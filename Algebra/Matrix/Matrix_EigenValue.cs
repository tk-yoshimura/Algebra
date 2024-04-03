using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>固有値計算</summary>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public static ddouble[] EigenValues(Matrix m, int precision_level = 32) {
            for (int i = 0; i < precision_level; i++) {
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
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            const int vector_converge_times = 3;

            ddouble[] eigen_values = null;
            int size = m.Size;
            Vector[] eigen_vectors = Identity(size).Horizontals;

            ddouble eigen_value;
            bool[] is_converged_vector = new bool[size];
            Matrix d = m, identity = Identity(size);
            Vector x_init = Vector.Fill(size, 1).Normal, x;

            for (int i = 0; i < precision_level; i++) {
                (Matrix q, Matrix r) = QR(d);
                d = r * q;

                eigen_values = d.Diagonals;

                bool is_all_converged = true;

                for (int j = 0; j < size; j++) {
                    if (is_converged_vector[j]) {
                        continue;
                    }

                    is_all_converged = false;

                    eigen_value = eigen_values[j];

                    Matrix h = m - eigen_value * identity;
                    if (h.Norm <= m.Norm * 1e-28) {
                        is_converged_vector[j] = true;
                        break;
                    }

                    Matrix g = h.Inverse;
                    if (!IsFinite(g)) {
                        is_converged_vector[j] = true;
                        break;
                    }

                    x = x_init;

                    for (int k = 0; k < vector_converge_times; k++) {
                        x = (g * x).Normal;
                    }

                    eigen_vectors[j] = x;
                }

                if (is_all_converged) {
                    break;
                }
            }

            return (eigen_values, eigen_vectors);
        }
    }
}
