using DoubleDouble;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>トレース</summary>
        public ddouble Trace {
            get {
                (_, Matrix upper) = LUDecomposition();

                ddouble sum = 0;
                foreach (var diagonal in upper.Diagonals) {
                    sum += diagonal;
                }

                return sum;
            }
        }
    }
}
