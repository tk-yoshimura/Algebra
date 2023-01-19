using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>ガウスの消去法</summary>
        public static Matrix GaussianEliminate(Matrix m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("invalid size", $"{nameof(m)}");
            }

            int exponent = m.MaxExponent;
            int n = m.Rows;

            Matrix v = Identity(m.Rows), u = ScaleB(m, -exponent);

            for (int i = 0; i < n; i++) {
                ddouble pivot = ddouble.Abs(u.e[i, i]);
                int p = i;

                //ピボット選択
                for (int j = i + 1; j < n; j++) {
                    if (ddouble.Abs(u.e[j, i]) > pivot) {
                        pivot = ddouble.Abs(u.e[j, i]);
                        p = j;
                    }
                }

                //ピボットが閾値以下ならばMは正則行列でないので逆行列は存在しない
                if (Math.ILogB((double)pivot) <= -100) {
                    return Invalid(v.Rows, v.Columns);
                }

                //行入れ替え
                if (p != i) {
                    for (int j = 0; j < n; j++) {
                        (u.e[p, j], u.e[i, j]) = (u.e[i, j], u.e[p, j]);
                    }

                    for (int j = 0; j < v.Columns; j++) {
                        (v.e[p, j], v.e[i, j]) = (v.e[i, j], v.e[p, j]);
                    }
                }

                // 前進消去
                ddouble inv_mii = 1d / u.e[i, i];
                u.e[i, i] = 1;
                for (int j = i + 1; j < n; j++) {
                    u.e[i, j] *= inv_mii;
                }
                for (int j = 0; j < v.Columns; j++) {
                    v.e[i, j] *= inv_mii;
                }

                for (int j = i + 1; j < n; j++) {
                    ddouble mul = u.e[j, i];
                    u.e[j, i] = 0;
                    for (int k = i + 1; k < n; k++) {
                        u.e[j, k] -= u.e[i, k] * mul;
                    }
                    for (int k = 0; k < v.Columns; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            // 後退代入
            for (int i = n - 1; i >= 0; i--) {
                for (int j = i - 1; j >= 0; j--) {
                    ddouble mul = u.e[j, i];
                    for (int k = i; k < n; k++) {
                        u.e[j, k] = 0;
                    }
                    for (int k = 0; k < v.Columns; k++) {
                        v.e[j, k] -= v.e[i, k] * mul;
                    }
                }
            }

            v = ScaleB(v, -exponent);
            return v;
        }
    }
}
