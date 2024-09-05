using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>コレスキー分解</summary>
        public static Matrix Cholesky(Matrix m, bool enable_check_symmetric = true) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;
            
            if ((enable_check_symmetric && !IsSymmetric(m)) || !IsFinite(m)) {
                return Invalid(n);
            }

            if (IsZero(m)) {
                return Zero(n);
            }

            int exponent = (m.MaxExponent / 2) * 2;

            Matrix u = ScaleB(m, -exponent);

            ddouble[,] v = new ddouble[n, n];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < i; j++) {
                    ddouble v_ij = u[i, j];
                    for (int k = 0; k < j; k++) {
                        v_ij -= v[i, k] * v[j, k];
                    }
                    v[i, j] = v_ij / v[j, j]; 
                }

                ddouble v_ii = u[i, i];
                for (int k = 0; k < i; k++) {
                    v_ii -= v[i, k] * v[i, k];
                }
                v[i, i] = ddouble.Sqrt(v_ii);
            }

            Matrix l = ScaleB(new Matrix(v, cloning: false), exponent / 2);

            return l;
        }
    }
}
