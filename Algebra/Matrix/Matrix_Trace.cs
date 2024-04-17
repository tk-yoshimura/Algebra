using DoubleDouble;
using System.Diagnostics;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>トレース</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Trace {
            get {
                ddouble sum = 0d;
                foreach (var diagonal in Diagonals) {
                    sum += diagonal;
                }

                return sum;
            }
        }
    }
}
