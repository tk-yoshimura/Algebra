using DoubleDouble;
using System;
using System.Linq;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>固有値計算</summary>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public static ddouble[] EigenValues(Matrix m, int precision_level = 32) {
            ArgumentOutOfRangeException.ThrowIfLessThan(precision_level, 2, nameof(precision_level));
            if (!IsSquare(m) || m.Size <= 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            for (int iter = 0; iter < precision_level; iter++) {
                (Matrix q, Matrix r) = QR(m);
                m = r * q;
            }

            return m.Diagonals;
        }

        /// <summary>固有値・固有ベクトル</summary>
        /// <param name="eigen_values">固有値</param>
        /// <param name="eigen_vectors">固有ベクトル</param>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public static (ddouble[] eigen_values, Vector[] eigen_vectors) EigenValueVectors(Matrix m, int precision_level = 32) {
            ArgumentOutOfRangeException.ThrowIfLessThan(precision_level, 2, nameof(precision_level));
            if (!IsSquare(m) || m.Size <= 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;
            bool[] is_convergenced = new bool[n];
            ddouble[] eigen_values = Vector.Fill(n, 1);
            Vector[] eigen_vectors = Identity(n).Horizontals;

            Matrix d = m, identity = Identity(n);

            Matrix[] gs_prev = new Matrix[n];

            for (int iter_qr = 0; iter_qr < precision_level; iter_qr++) {
                (Matrix q, Matrix r) = QR(d);
                d = r * q;

                eigen_values = d.Diagonals;

                for (int i = 0; i < n; i++) {
                    if (is_convergenced[i]) {
                        continue;
                    }

                    if (iter_qr < precision_level - 1) {
                        Matrix h = m - eigen_values[i] * identity;
                        Matrix g = h.Inverse;
                        if (IsFinite(g) && g.Norm < ddouble.Ldexp(h.Norm, 100)) {
                            gs_prev[i] = g;
                            continue;
                        }

                        if (gs_prev[i] is null) {
                            is_convergenced[i] = true;
                            continue;
                        }
                    }

                    Matrix gp = ScaleB(gs_prev[i], -gs_prev[i].MaxExponent);

                    ddouble norm, norm_prev = ddouble.NaN;
                    Vector x = Vector.Fill(n, 0.125), x_prev = x;
                    x[i] = 1d;

                    for (int iter_vector = 0; iter_vector < precision_level; iter_vector++) {
                        x = (gp * x).Normal;

                        if (ddouble.IsNegative(Vector.Dot(x, x_prev))) {
                            x = -x;
                        }

                        norm = (x - x_prev).Norm;

                        if (norm < 1e-30 || (norm < 1e-28 && norm >= norm_prev)) {
                            break;
                        }

                        x_prev = x;
                        norm_prev = norm;
                    }

                    eigen_vectors[i] = x;
                    is_convergenced[i] = true;
                }

                if (is_convergenced.All(b => b)) {
                    break;
                }
            }

            return (eigen_values, eigen_vectors);
        }
    }
}
