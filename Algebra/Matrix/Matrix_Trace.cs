using DoubleDouble;
using System.Diagnostics;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>トレース</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Trace {
            get {
                (_, Matrix u) = LU(this);

                ddouble sum = 0d;
                foreach (var diagonal in u.Diagonals) {
                    sum += diagonal;
                }

                return sum;
            }
        }
    }
}
