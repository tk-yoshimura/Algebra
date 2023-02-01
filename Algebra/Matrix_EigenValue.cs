using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>固有値計算</summary>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public ddouble[] CalculateEigenValues(int precision_level = 32) {
            Matrix m = Copy();
            for (int i = 0; i < precision_level; i++) {
                (Matrix q, Matrix r) = m.QRDecompose();
                m = r * q;
            }

            return m.Diagonals;
        }

        /// <summary>固有値・固有ベクトル</summary>
        /// <param name="eigen_values">固有値</param>
        /// <param name="eigen_vectors">固有ベクトル</param>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public (ddouble[] eigen_values, Vector[] eigen_vectors) CalculateEigenValueVectors(int precision_level = 32) {
            if (!IsSquare(this)) {
                throw new InvalidOperationException("not square matrix");
            }

            ddouble[] eigen_values = null;
            Vector[] eigen_vectors = new Vector[Size];

            const int vector_converge_times = 3;

            ddouble eigen_value;
            bool[] is_converged_vector = new bool[Size];
            Matrix m = Copy(), g;
            Vector x_init = Vector.Zero(Size), x;

            for (int i = 0; i < Size; i++) {
                eigen_vectors[i] = Vector.Invalid(Size);
                x_init.v[i] = 1d;
            }
            x_init /= x_init.Norm;

            for (int i = 0; i < precision_level; i++) {
                (Matrix q, Matrix r) = m.QRDecompose();
                m = r * q;

                eigen_values = m.Diagonals;

                bool is_all_converged = true;

                for (int j = 0; j < Size; j++) {
                    if (is_converged_vector[j]) {
                        continue;
                    }

                    is_all_converged = false;

                    eigen_value = eigen_values[j];

                    g = (this - eigen_value * Identity(Size)).Inverse;
                    if (!IsValid(g)) {
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
