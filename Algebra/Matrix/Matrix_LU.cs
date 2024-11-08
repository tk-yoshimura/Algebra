using DoubleDouble;
using System;
using System.Linq;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        private static (int[] pivot, int pivot_det, Matrix l, Matrix u) LUKernel(Matrix m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            int n = m.Size;

            int[] ps = (new int[n]).Select((_, idx) => idx).ToArray();
            int pivot_det = 1;

            if (!IsFinite(m)) {
                return (ps, 1, Invalid(n), Invalid(n));
            }
            if (IsZero(m)) {
                return (ps, 1, Invalid(n), Zero(n));
            }

            int exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            Matrix l = Zero(n), u = Zero(n);

            //LU分解
            for (int i = 0; i < n; i++) {
                ddouble pivot = ddouble.Abs(m.e[i, i]);
                int r = i;

                for (int j = i + 1; j < n; j++) {
                    if (ddouble.Abs(m.e[j, i]) > pivot) {
                        pivot = ddouble.Abs(m.e[j, i]);
                        r = j;
                    }
                }

                //ピボットが閾値以下ならばMは正則行列でない
                if (ddouble.ILogB(pivot) <= -100) {
                    return (ps, 0, Invalid(n), Zero(n));
                }

                if (r != i) {
                    for (int j = 0; j < n; j++) {
                        (m.e[r, j], m.e[i, j]) = (m.e[i, j], m.e[r, j]);
                    }

                    (ps[r], ps[i]) = (ps[i], ps[r]);

                    pivot_det = -pivot_det;
                }

                for (int j = i + 1; j < n; j++) {
                    ddouble mul = m.e[j, i] / m.e[i, i];
                    m.e[j, i] = mul;

                    for (int k = i + 1; k < n; k++) {
                        m.e[j, k] -= m.e[i, k] * mul;
                    }
                }
            }

            //三角行列格納
            for (int i = 0; i < n; i++) {
                l.e[i, i] = 1d;

                int j = 0;
                for (; j < i; j++) {
                    l.e[i, j] = m.e[i, j];
                }
                for (; j < n; j++) {
                    u.e[i, j] = m.e[i, j];
                }
            }

            u = ScaleB(u, exponent);

            return (ps, pivot_det, l, u);
        }

        /// <summary>LU分解</summary>
        public static (Matrix p, Matrix l, Matrix u) LU(Matrix m) {
            (int[] ps, int pivot_det, Matrix l, Matrix u) = LUKernel(m);

            int n = m.Size;

            if (pivot_det == 0) {
                return (Identity(n), l, u);
            }

            Matrix p = Zero(n, n);

            // ピボット行列
            for (int i = 0; i < n; i++) {
                p[i, ps[i]] = 1d;
            }

            return (p, l, u);
        }
    }
}
