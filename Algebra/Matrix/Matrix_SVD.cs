using DoubleDouble;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>特異値分解</summary>
        public static (Matrix u, Vector s, Matrix v) SVD(Matrix m) {
            if (m.Rows < 1 || m.Columns < 1) {
                throw new ArgumentException("empty matrix", nameof(m));
            }

            if (m.Rows >= m.Columns) {
                (Vector eigen_vals, Vector[] eigen_vecs) = EigenValueVectors(m.T * m);

                Matrix v = HConcat(eigen_vecs);
                Vector s = (x => x >= 0d ? ddouble.Sqrt(x) : 0d, eigen_vals);
                Matrix l = m * v;

                for (int i = 0; i < s.Dim; i++) {
                    l[.., i] /= s[i];
                }

                List<Vector> ls = [.. l.Verticals];
                GramSchmidtMethod(ls);

                Matrix u = HConcat(ls.ToArray());

                return (u, s, v);
            }
            else {
                (Vector eigen_vals, Vector[] eigen_vecs) = EigenValueVectors(m * m.T);

                Matrix u = HConcat(eigen_vecs);
                Vector s = (x => x >= 0d ? ddouble.Sqrt(x) : 0d, eigen_vals);
                Matrix r = u.T * m;

                for (int i = 0; i < s.Dim; i++) {
                    r[i, ..] /= s[i];
                }

                List<Vector> rs = [.. r.Horizontals];
                GramSchmidtMethod(rs);

                Matrix v = HConcat(rs.ToArray());

                return (u, s, v);
            }
        }

        private static void GramSchmidtMethod(List<Vector> vs) {
            int n = vs[0].Dim;

            for (int k = vs.Count - 1; k >= 0; k--) {
                if (Vector.IsFinite(vs[k])) {
                    break;
                }
                vs.RemoveAt(k);
            }

            if (vs.Count < 1) {
                vs.AddRange(Identity(n).Verticals);
                return;
            }

            List<Vector> init_vecs = [];
            for (int i = 0; i < n; i++) {
                Vector v = Vector.Zero(n);
                v[i] = 1;
                init_vecs.Add(v);
            }

            while (vs.Count < n) {
                Vector g = init_vecs.OrderBy(u => vs.Select(v => ddouble.Square(Vector.Dot(u, v))).Sum()).First();

                Vector v = g;

                for (int i = 0; i < vs.Count; i++) {
                    v -= vs[i] * Vector.Dot(g, vs[i]) / vs[i].SquareNorm;
                }

                v = v.Normal;

                vs.Add(v);
            }
        }
    }
}
