using Algebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void LUDecomposeTest() {
            foreach (Matrix matrix in MatrixTestCases.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix pivot, Matrix lower, Matrix upper) = Matrix.LU(matrix);

                foreach (var diagonal in lower.Diagonals) {
                    Assert.AreEqual(1, diagonal);
                }

                Assert.IsTrue((matrix - pivot * lower * upper).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void LUDecomposeLargeTest() {
            foreach (Matrix matrix in MatrixTestCases.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix pivot, Matrix lower, Matrix upper) = Matrix.LU(matrix);

                foreach (var diagonal in lower.Diagonals) {
                    Assert.AreEqual(1, diagonal);
                }

                Assert.IsTrue((matrix - pivot * lower * upper).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void LUDecomposeSingularTest() {
            foreach (Matrix matrix in MatrixTestCases.SingularMatrixs) {

                Console.WriteLine($"test: {matrix}");

                (Matrix pivot, Matrix lower, Matrix upper) = Matrix.LU(matrix);

                Assert.IsTrue(Matrix.IsFinite(pivot));
                Assert.IsTrue(!Matrix.IsValid(lower));
                Assert.IsTrue(Matrix.IsZero(upper));
                Assert.AreEqual(0d, matrix.Det);
            }
        }
    }
}