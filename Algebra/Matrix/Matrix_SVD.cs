using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>特異値分解</summary>
        public static (Matrix u, Vector s, Matrix v) SVD(Matrix x) {
            if (!IsSquare(x)) {
                throw new ArgumentException("invalid size", nameof(x));
            }

            int exponent = x.MaxExponent;
            int m = x.Rows, n = x.Columns;

            Matrix u = ScaleB(x, -exponent), v = Identity(n);
            Vector s = Vector.Zero(n);

            static (ddouble c, ddouble s) jacobi_coef(ddouble dot_ii, ddouble dot_ij, ddouble dot_jj) {
                if (ddouble.Abs(dot_ij) > 1e-28d) {
                    ddouble tau = (dot_jj - dot_ii) / ddouble.Ldexp(dot_ij, 1);
                    ddouble t = (tau >= 0d)
                        ? (+1d / (ddouble.Hypot(1d, tau) + tau))
                        : (-1d / (ddouble.Hypot(1d, tau) - tau));

                    ddouble c = 1d / ddouble.Hypot(1d, t);
                    ddouble s = t * c;

                    return (c, s);
                }
                else {
                    return (1d, 0d);
                }
            }

            for (int iter = 0; iter < 256; iter++) {
                bool convergenced = true;

                for (int i = 0; i < n - 1; i++) { 
                    for (int j = i + 1; j < n; j++) {
                        Vector ui = u[.., i], uj = u[.., j];

                        ddouble dot_ii = ui.SquareNorm, dot_jj = uj.SquareNorm;
                        ddouble dot_ij = Vector.Dot(ui, uj);

                        (ddouble cos, ddouble sin) = jacobi_coef(dot_ii, dot_ij, dot_jj);

                        if (ddouble.Abs(sin) < 1e-28d) {
                            continue;
                        }

                        u[.., i] = cos * ui - sin * uj;
                        u[.., j] = sin * ui + cos * uj;

                        Vector vi = v[.., i], vj = v[.., j];
                        v[.., i] = cos * vi - sin * vj;
                        v[.., j] = sin * vi + cos * vj;

                        convergenced = false;
                    }
                }

                if (convergenced) {
                    break;
                }
            }

            for (int i = 0; i < n; i++) {
                ddouble sigma = u[.., i].Norm;

                s[i] = sigma;

                u[.., i] /= sigma; 
            }

            s = Vector.ScaleB(s, exponent);

            return (u, s, v);
        }
    }
}
