using DoubleDouble;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>SVD分解</summary>
        public static (Matrix u, Vector s, Matrix v) SVD(Matrix m) {
            Matrix mt = m.T, r = mt * m;

            (ddouble[] values, Vector[] vectors) = EigenValueVectors(r);
        
            Vector s = (v => ddouble.Sqrt(ddouble.Max(0, v)), values);
            Matrix v = VConcat(vectors);
            Matrix u = m * Invert(FromDiagonals(s) * v);

            return (u, s, v);
        }
    }
}
