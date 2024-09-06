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

        /// <summary>正定値対称行列に対する逆行列</summary>
        public static Matrix InversePositiveSymmetric(Matrix m, bool enable_check_symmetric = true) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return Invalid(n, n);
            }

            Matrix l = Cholesky(m, enable_check_symmetric);

            if (!IsFinite(l)) {
                return Invalid(n);
            }

            Matrix v = Identity(n);

            for (int i = 0; i < n; i++) {
                ddouble inv_mii = 1d / l.e[i, i];
                for (int j = 0; j < n; j++) {
                    v.e[i, j] *= inv_mii;
                }

                for (int j = i + 1; j < n; j++) {
                    ddouble mul = l.e[j, i];
                    for (int k = 0; k < n; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            ddouble[,] ret = new ddouble[n, n];
            v = v.T;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j <= i; j++) {
                    ddouble s = 0d;

                    for (int k = i; k < n; k++) {
                        s += v.e[i, k] * v.e[j, k];
                    }

                    ret[i, j] = ret[j, i] = s;
                }
            }

            Matrix w = new(ret, cloning: false);

            return w;
        }

        /// <summary>正定値対称行列に対する連立方程式の解</summary>
        public static Vector SolvePositiveSymmetric(Matrix m, Vector v, bool enable_check_symmetric = true) {
            if (!IsSquare(m) || m.Size != v.Dim) {
                throw new ArgumentException("invalid size", $"{nameof(m)}, {nameof(v)}");
            }

            int n = m.Size;

            if (!IsFinite(m)) {
                return Vector.Invalid(n);
            }

            Matrix l = Cholesky(m, enable_check_symmetric);

            if (!IsFinite(l)) {
                return Vector.Invalid(n);
            }

            v = v.Copy();

            // 前進消去
            for (int i = 0; i < n; i++) {
                ddouble inv_mii = 1d / l.e[i, i];
                v[i] *= inv_mii;

                for (int j = i + 1; j < n; j++) {
                    ddouble mul = l.e[j, i];
                    v[j] -= v[i] * mul;
                }
            }

            // 後退代入
            for (int i = n - 1; i >= 0; i--) {
                ddouble inv_mii = 1d / l.e[i, i];
                v[i] *= inv_mii;

                for (int j = i - 1; j >= 0; j--) {
                    ddouble mul = l.e[i, j];
                    v[j] -= v[i] * mul;
                }
            }

            return v;
        }
    }
}
