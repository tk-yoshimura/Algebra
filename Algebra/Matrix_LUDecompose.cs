using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>LU分解</summary>
        public (Matrix lower_matrix, Matrix upper_matrix) LUDecompose() {
            if (!IsSquare(this)) {
                throw new InvalidOperationException("not square matrix");
            }

            int n = Size;

            Matrix m = Copy();
            Matrix l = Zero(n, n), u = Zero(n, n);

            //LU分解
            for (int i = 0; i < n; i++) {
                for (int j = i + 1; j < n; j++) {
                    ddouble mul = m.e[j, i] /= m.e[i, i];
                    m.e[j, i] = mul;

                    for (int k = i + 1; k < n; k++) {
                        m.e[j, k] -= m.e[i, k] * mul;
                    }
                }
            }

            //三角行列格納
            for (int i = 0; i < n; i++) {
                l.e[i, i] = 1;

                int j = 0;
                for (; j < i; j++) {
                    l.e[i, j] = m.e[i, j];
                }
                for (; j < n; j++) {
                    u.e[i, j] = m.e[i, j];
                }
            }

            return (l, u);
        }
    }
}
