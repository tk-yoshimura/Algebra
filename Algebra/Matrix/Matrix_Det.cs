using DoubleDouble;
using System.Diagnostics;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>行列式</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Det {
            get {
                (_, int pivot_det, _, Matrix u) = LUKernel(this);

                ddouble prod = pivot_det;
                foreach (var diagonal in u.Diagonals) {
                    prod *= diagonal;
                }

                return prod;
            }
        }
    }
}
