using DoubleDouble;
using System;
using System.Diagnostics;
using System.Linq;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>特異値分解</summary>
        public static (Matrix u, Vector s, Matrix v) SVD(Matrix x) {
            if (x.Rows < x.Columns) {
                (Matrix ut, Vector st, Matrix vt) = SVD(x.T);

                return (vt, st, ut);
            }

            int exponent = x.MaxExponent;
            int m = x.Rows, n = x.Columns;

            Debug.Assert(m >= n);

            Vector[] us = ScaleB(x, -exponent).Verticals, vs = Identity(n).Verticals;
            ddouble[] sqnorms = us.Select(u => u.SquareNorm).ToArray();

            static (ddouble c, ddouble s) jacobi_coef(ddouble dot_ii, ddouble dot_ij, ddouble dot_jj) {
                (_, (dot_ii, dot_ij, dot_jj)) = ddouble.AdjustScale(0, (dot_ii, dot_ij, dot_jj));

                ddouble v = (dot_jj - dot_ii) / dot_ij;

                if (ddouble.Abs(v) < 1e6d) {
                    ddouble tau = ddouble.Ldexp(v, -1);
                    ddouble t = (tau >= 0d)
                        ? (+1d / (ddouble.Hypot(1d, tau) + tau))
                        : (-1d / (ddouble.Hypot(1d, tau) - tau));

                    ddouble c = 1d / ddouble.Hypot(1d, t);
                    ddouble s = t * c;

                    return (c, s);
                }
                else {
                    ddouble u = 1d / v, u2 = ddouble.Square(u);

                    ddouble c = 1d + u2 * (-0.5d + u2 * (1.375d + u2 * -4.3125d));
                    ddouble s = u * (1d + u2 * (-1.5d + u2 * (3.875d + u2 * -11.6875d)));

                    return (c, s);
                }
            }

            for (int iter = 0; iter < 256; iter++) {
                bool convergenced = true;

                for (int i = 0; i < n - 1; i++) { 
                    for (int j = i + 1; j < n; j++) {
                        Vector ui = us[i], uj = us[j];

                        ddouble dot_ii = sqnorms[i], dot_jj = sqnorms[j], dot_ij = Vector.Dot(ui, uj);

                        (ddouble cos, ddouble sin) = jacobi_coef(dot_ii, dot_ij, dot_jj);

                        if (ddouble.Abs(sin) < 1e-28d) {
                            continue;
                        }

                        us[i] = cos * ui - sin * uj;
                        us[j] = sin * ui + cos * uj;

                        sqnorms[i] = us[i].SquareNorm;
                        sqnorms[j] = us[j].SquareNorm;

                        Vector vi = vs[i], vj = vs[j];
                        vs[i] = cos * vi - sin * vj;
                        vs[j] = sin * vi + cos * vj;

                        convergenced = false;
                    }
                }

                if (convergenced) {
                    break;
                }
            }
                        
            ddouble[] sigmas = new ddouble[n];

            for (int i = 0; i < n; i++) {
                ddouble sigma = us[i].Norm;

                sigmas[i] = sigma;

                us[i] /= sigma; 
            }

            int[] order = sigmas.Select((d, idx) => (d, idx)).OrderByDescending(item => ddouble.Abs(item.d)).Select(item => item.idx).ToArray();

            Vector s = Vector.ScaleB(order.Select(idx => sigmas[idx]).ToArray(), exponent);
            vs = order.Select(idx => vs[idx]).ToArray();
            us = order.Select(idx => us[idx]).ToArray();

            Matrix u = HConcat(us), v = HConcat(vs);

            return (u, s, v);
        }
    }
}
