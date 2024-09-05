using Algebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void CholeskyTest() {
            foreach (Matrix matrix in MatrixTestCases.PositiveMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix l = Matrix.Cholesky(matrix);
                Matrix v = l * l.T;

                Assert.IsTrue((matrix - v).Norm < 1e-28);
            }
        }
    }
}