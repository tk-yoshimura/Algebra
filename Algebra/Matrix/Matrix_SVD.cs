using DoubleDouble;
using System.Diagnostics;
using System.Linq;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>特異値分解</summary>
        public static (Matrix u, Vector s, Matrix v) SVD(Matrix m, int max_iter = 256) {
            const double eps = 1e-28d;

            if (m.Rows < m.Columns) {
                (Matrix ut, Vector st, Matrix vt) = SVD(m.T);

                return (vt, st, ut);
            }

            int exponent = m.MaxExponent;
            int row = m.Rows, col = m.Columns;

            Debug.Assert(row >= col);

            if (!IsFinite(m)) {
                return (Invalid(row, row), Vector.Invalid(col), Invalid(col, col));
            }
            if (IsZero(m)) {
                return (Identity(row), Vector.Zero(col), Identity(col));
            }

            Vector[] us = ScaleB(m, -exponent).Verticals, vs = Identity(col).Verticals;
            ddouble[] sqnorms = us.Select(u => u.SquareNorm).ToArray();

            // jacobi rotate cos, sin
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
                else if (ddouble.Abs(v) < 1e32d) {
                    ddouble u = 1d / v, u2 = ddouble.Square(u);

                    ddouble c = 1d + u2 * (-0.5d + u2 * (1.375d + u2 * -4.3125d));
                    ddouble s = u * (1d + u2 * (-1.5d + u2 * (3.875d + u2 * -11.6875d)));

                    return (c, s);
                }
                else {
                    return (1d, 0d);
                }
            }

            // one-side jacobi method
            for (int iter = 0; iter < max_iter; iter++) {
                bool convergenced = true;

                for (int i = 0; i < col - 1; i++) {
                    for (int j = i + 1; j < col; j++) {
                        Vector ui = us[i], uj = us[j];

                        ddouble dot_ii = sqnorms[i], dot_jj = sqnorms[j], dot_ij = Vector.Dot(ui, uj);

                        (ddouble cos, ddouble sin) = jacobi_coef(dot_ii, dot_ij, dot_jj);

                        if (ddouble.Abs(sin) < eps) {
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

            // determine eigen value
            ddouble[] sigmas = new ddouble[col];

            for (int i = 0; i < col; i++) {
                ddouble sigma = us[i].Norm;

                sigmas[i] = sigma;

                us[i] /= sigma;
            }

            // reorder by eigen value
            int[] order = sigmas.Select((d, idx) => (d, idx)).OrderByDescending(item => ddouble.Abs(item.d)).Select(item => item.idx).ToArray();

            sigmas = order.Select(idx => sigmas[idx]).ToArray();
            us = order.Select(idx => us[idx]).ToArray();
            vs = order.Select(idx => vs[idx]).ToArray();

            // truncate near zero eigen
            for (int i = 0; i < sigmas.Length; i++) {
                if (ddouble.Abs(sigmas[i]) < eps) {
                    sigmas[i] = 0d;
                    us = us[..i];
                    break;
                }
            }

            // generate ortho vector
            for (int i = us.Length; i < row; i++) {
                Vector u_new = Vector.Zero(row);
                u_new[i] = 1d;

                for (int k = 0; k < us.Length; k++) {
                    u_new -= us[k] * us[k][i];
                }

                u_new = u_new.Normal;

                us = [.. us, u_new];
            }

            Matrix u = HConcat(us), v = HConcat(vs);
            Vector s = Vector.ScaleB(sigmas, exponent);

            return (u, s, v);
        }
    }
}
