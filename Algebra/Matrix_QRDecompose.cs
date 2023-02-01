using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>QR分解</summary>
        public (Matrix orthogonal_matrix, Matrix triangular_matrix) QRDecompose() {
            if (!IsSquare(this)) {
                throw new InvalidOperationException("not square matrix");
            }

            int n = Size;

            Matrix q = new(n, n), r = new(n, n);

            Vector[] e = new Vector[n], u = new Vector[n];
            for (int i = 0; i < n; i++) {
                e[i] = Vector.Zero(n);
            }

            for (int i = 0; i < n; i++) {
                Vector ai = Vertical(i);

                u[i] = ai.Copy();

                for (int j = 0; j < i; j++) {
                    u[i] -= u[j] * Vector.Dot(ai, u[j]) / u[j].SquareNorm;
                }

                e[i] = u[i].Normal;

                for (int j = 0; j <= i; j++) {
                    r.e[j, i] = Vector.Dot(ai, e[j]);
                }

                for (int j = 0; j < n; j++) {
                    q.e[j, i] = e[i].v[j];
                }
            }

            return (q, r);
        }
    }
}
