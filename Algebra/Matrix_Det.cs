using DoubleDouble;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>行列式</summary>
        public ddouble Det {
            get {
                (_, Matrix upper) = LUDecomposition();

                ddouble prod = 1d;
                foreach (var diagonal in upper.Diagonals) {
                    prod *= diagonal;
                }

                return prod;
            }
        }
    }
}
