using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>QR分解</summary>
        public void QRDecomposition(out Matrix orthogonal_matrix, out Matrix triangular_matrix) {
            if(!IsSquare(this)) {
                throw new InvalidOperationException();
            }

            orthogonal_matrix = new Matrix(Size, Size);
            triangular_matrix = new Matrix(Size, Size);

            int i, j, n = Size;

            Vector[] e = new Vector[n], u = new Vector[n];
            for(i = 0; i < Size; i++) {
                e[i] = Vector.Zero(n);
            }

            for(i = 0; i < n; i++) {
                Vector ai = Vertical(i);

                u[i] = ai.Copy();

                for(j = 0; j < i; j++) {
                    u[i] -= u[j] * Vector.InnerProduct(ai, u[j]) / u[j].SquareNorm;
                }

                e[i] = u[i].Normal;

                for(j = 0; j <= i; j++) {
                    triangular_matrix[j, i] = Vector.InnerProduct(ai, e[j]);
                }

                for(j = 0; j < n; j++) {
                    orthogonal_matrix[j, i] = e[i][j];
                }
            }
        }
    }
}
