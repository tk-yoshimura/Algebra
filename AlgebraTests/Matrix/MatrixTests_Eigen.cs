using Algebra;
using DoubleDouble;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void EigenValuesTest() {
            Matrix matrix = new double[,] { { 1, 2 }, { 4, 5 } };
            ddouble[] eigen_values = Matrix.EigenValues(matrix);

            Assert.IsTrue(ddouble.Abs(eigen_values[0] - (3 + 2 * ddouble.Sqrt(3))) < 1e-29);
            Assert.IsTrue(ddouble.Abs(eigen_values[1] - (3 - 2 * ddouble.Sqrt(3))) < 1e-29);
        }

        [TestMethod()]
        public void EigenValuesEyeTest() {
            Matrix matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
            ddouble[] eigen_values = Matrix.EigenValues(matrix);

            Assert.IsTrue(ddouble.Abs(eigen_values[0] - 1) < 1e-29);
            Assert.IsTrue(ddouble.Abs(eigen_values[1] - 1) < 1e-29);
        }

        [TestMethod()]
        public void EigenValuesEyeEpsTest() {
            Matrix matrix = new double[,] { { 1, 1e-30, 0 }, { 0, 1, 2e-30 }, { -1e-30, 1, 1e-30 } };
            ddouble[] eigen_values = Matrix.EigenValues(matrix);

            Assert.IsTrue(ddouble.Abs(eigen_values[0] - 1) < 1e-29);
            Assert.IsTrue(ddouble.Abs(eigen_values[1] - 1) < 1e-29);
        }

        [TestMethod()]
        public void EigenVectorTest() {
            foreach (Matrix matrix in MatrixTestCases.PositiveMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix matrix_scaled = Matrix.ScaleB(matrix, -matrix.MaxExponent);

                (ddouble[] eigen_values, Vector[] eigen_vectors) = Matrix.EigenValueVectors(matrix_scaled);
                Vector eigen_values_expected = Matrix.EigenValues(matrix_scaled);

                Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-25);

                for (int i = 0; i < matrix_scaled.Size; i++) {
                    ddouble eigen_value = eigen_values[i];
                    Vector eigen_vector = eigen_vectors[i];

                    Assert.IsTrue((matrix_scaled * eigen_vector - eigen_value * eigen_vector).Norm < 1e-1);
                }
            }
        }

        [TestMethod()]
        public void EigenVectorEyeTest() {
            Matrix matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            (ddouble[] eigen_values, Vector[] eigen_vectors) = Matrix.EigenValueVectors(matrix);

            Assert.AreEqual(new Matrix(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } }), matrix);

            for (int i = 0; i < matrix.Size; i++) {
                ddouble eigen_value = eigen_values[i];
                Vector eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-15);
            }
        }

        [TestMethod()]
        public void EigenVectorEyeEpsTest() {
            Matrix matrix = new double[,] { { 1, 1e-30, 0 }, { 0, 1, 2e-30 }, { -1e-30, 1e-30, 1 } };

            (ddouble[] eigen_values, Vector[] eigen_vectors) = Matrix.EigenValueVectors(matrix);

            Assert.AreEqual(new double[,] { { 1, 1e-30, 0 }, { 0, 1, 2e-30 }, { -1e-30, 1e-30, 1 } }, matrix);

            for (int i = 0; i < matrix.Size; i++) {
                ddouble eigen_value = eigen_values[i];
                Vector eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-15);
            }
        }
    }
}