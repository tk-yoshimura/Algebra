using DoubleDouble;
using System.Diagnostics;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>トレース</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
