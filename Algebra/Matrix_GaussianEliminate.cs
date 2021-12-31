using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>ガウスの消去法</summary>
        public static void GaussianEliminate(Matrix m, ref Matrix v) {
            if (!IsSquare(m) || m.Rows != v.Rows) {
                throw new ArgumentException("invalid size", $"{m},{v}");
            }

            int i, j, k, p, n = m.Rows;
            double pivot, inv_mii, mul, swap;

            for (i = 0; i < n; i++) {
                pivot = Math.Abs(m[i, i]);
                p = i;

                //ピボット選択
                for (j = i + 1; j < n; j++) {
                    if (Math.Abs(m[j, i]) > pivot) {
                        pivot = Math.Abs(m[j, i]);
                        p = j;
                    }
                }

                //ピボットが閾値以下ならばMは正則行列でないので逆行列は存在しない
                if (pivot < 1.0e-12) {
                    v = Invalid(v.Rows, v.Columns);
                    return;
                }

                //行入れ替え
                if (p != i) {
                    for (j = 0; j < n; j++) {
                        swap = m[i, j];
                        m[i, j] = m[p, j];
                        m[p, j] = swap;
                    }

                    for (j = 0; j < v.Columns; j++) {
                        swap = v[i, j];
                        v[i, j] = v[p, j];
                        v[p, j] = swap;
                    }
                }

                // 前進消去
                inv_mii = 1 / m[i, i];
                m[i, i] = 1;
                for (j = i + 1; j < n; j++) {
                    m[i, j] *= inv_mii;
                }
                for (j = 0; j < v.Columns; j++) {
                    v[i, j] *= inv_mii;
                }

                for (j = i + 1; j < n; j++) {
                    mul = m[j, i];
                    m[j, i] = 0;
                    for (k = i + 1; k < n; k++) {
                        m[j, k] -= m[i, k] * mul;
                    }
                    for (k = 0; k < v.Columns; k++) {
                        v[j, k] -= v[i, k] * mul;
                    }
                }
            }

            // 後退代入
            for (i = n - 1; i >= 0; i--) {
                for (j = i - 1; j >= 0; j--) {
                    mul = m[j, i];
                    for (k = i; k < n; k++) {
                        m[j, k] = 0;
                    }
                    for (k = 0; k < v.Columns; k++) {
                        v[j, k] -= v[i, k] * mul;
                    }
                }
            }
        }
    }
}
