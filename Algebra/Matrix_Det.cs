namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>行列式</summary>
        public double Det {
            get {
                LUDecomposition(out _, out Matrix upper);

                double prod = 1;
                foreach(var diagonal in upper.Diagonals) {
                    prod *= diagonal;
                }

                return prod;
            }
        }
    }
}
