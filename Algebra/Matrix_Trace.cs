namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>トレース</summary>
        public double Trace {
            get {
                LUDecomposition(out _, out Matrix upper);

                double sum = 0;
                foreach(var diagonal in upper.Diagonals) {
                    sum += diagonal;
                }

                return sum;
            }
        }
    }
}
