using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>LU分解</summary>
        public void LUDecomposition(out Matrix lower_matrix, out Matrix upper_matrix) {
            if(!IsSquare(this)) {
                throw new InvalidOperationException();
            }
            
            Matrix m = Copy();
            lower_matrix = Matrix.Zero(Size, Size);
            upper_matrix = Matrix.Zero(Size, Size);

            int i, j, k, n = Size;

            //LU分解
            for(i = 0; i < n; i++) {
                for(j = i + 1; j < n; j++) {
                    double mul = m[j, i] /= m[i, i];
                    m[j, i] = mul;

                    for(k = i + 1; k < n; k++) {
                        m[j, k] -= m[i, k] * mul;
                    }
                }
            }

            //三角行列格納
            for(i = 0; i < n; i++) {
                lower_matrix[i, i] = 1;
                for(j = 0; j < i; j++) {
                    lower_matrix[i, j] = m[i, j];
                }
                for(; j < n; j++) {
                    upper_matrix[i, j] = m[i, j];
                }
            }
        }
    }
}
