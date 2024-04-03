using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>QR分解</summary>
        public static (Matrix q, Matrix r) QR(Matrix m) {
            if (!IsSquare(m)) {
                throw new ArgumentException("invalid size", nameof(m));
            }

            int size = m.Size;

            int exponent = m.MaxExponent;
            m = ScaleB(m, -exponent);

            Matrix r = m, q = Identity(size);
            Vector u = Vector.Zero(size);

            for (int k = 0; k < size - 1; k++) {
                ddouble vsum = 0d;
                for (int i = k; i < size; i++) {
                    vsum += ddouble.Square(r.e[i, k]);
                }
                ddouble vnorm = ddouble.Sqrt(vsum);

                if (vnorm == 0d) {
                    continue;
                }

                ddouble x = r.e[k, k];
                u.v[k] = (x >= 0) ? (x + vnorm) : (x - vnorm);
                ddouble usum = ddouble.Square(u.v[k]);

                for (int i = k + 1; i < size; i++) {
                    u.v[i] = r.e[i, k];
                    usum += ddouble.Square(u.v[i]);
                }
                ddouble c = 2d / usum;

                Matrix h = Identity(size);
                for (int i = k; i < size; i++) {
                    for (int j = k; j < size; j++) {
                        h.e[i, j] -= c * u[i] * u[j];
                    }
                }

                r = h * r;
                q *= h;
            }

            for (int i = 0; i < size; i++) {
                if (ddouble.IsNegative(r.e[i, i])) {
                    q[.., i] = -q[.., i];
                    r[i, ..] = -r[i, ..];
                }

                for (int k = 0; k < i; k++) {
                    r.e[i, k] = 0d;
                }

                for (int k = i + 1; k < size; k++) {
                    if (ddouble.IsMinusZero(r.e[i, k])) {
                        r.e[i, k] = 0d;
                    }
                }
            }

            r = ScaleB(r, exponent);

            return (q, r);
        }
    }
}
