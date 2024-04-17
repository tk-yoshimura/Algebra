using DoubleDouble;
using System;
using System.Linq;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        private static (int[] pivot, int pivot_det, Matrix l, Matrix u) LUKernel(Matrix m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("invalid size", nameof(m));
            }

            int n = m.Size;

            int[] ps = (new int[n]).Select((_, idx) => idx).ToArray();
            int pivot_det = 1;

            if (!IsFinite(m)) {
                return (ps, 1, Invalid(n, n), Invalid(n, n));
            }
            if (IsZero(m)) {
                return (ps, 1, Zero(n, n), Zero(n, n));
            }

            int exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            Matrix l = Zero(n, n), u = Zero(n, n);

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
            (int[] ps, _, Matrix l, Matrix u) = LUKernel(m);

            int n = m.Size;

            Matrix p = Zero(n, n);

            // ピボット行列
            for (int i = 0; i < n; i++) {
                p[ps[i], i] = 1d;
            }

            return (p, l, u);
        }
    }
}
