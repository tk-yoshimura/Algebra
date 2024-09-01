using Algebra;
using DoubleDouble;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void SVDDecomposeTest() {
            foreach (Matrix matrix in MatrixTestCases.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix);

                Matrix matrix2 = u * Matrix.FromDiagonals(s) * v.T;

                Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
                Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
                Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
            }
        }

        [TestMethod()]
        public void SVDDecomposeLargeTest() {
            foreach (Matrix matrix in MatrixTestCases.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix);

                Matrix matrix2 = u * Matrix.FromDiagonals(s) * v.T;

                Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
                Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
                Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
            }
        }

        [TestMethod()]
        public void SVDDecomposeLargeRectTest() {
            foreach (Matrix matrix in MatrixTestCases.LargeMatrixs) {
                Matrix matrix_rect = matrix[..^3, ..];

                Console.WriteLine($"test: {matrix_rect}");

                (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix_rect);

                Matrix sm = Matrix.HConcat(Matrix.FromDiagonals(s), Matrix.Zero(matrix.Size - 3, 3));

                Matrix matrix2 = u * sm * v.T;

                Assert.IsTrue((matrix_rect - matrix2).Norm < 1e-25);
                Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
                Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
            }

            foreach (Matrix matrix in MatrixTestCases.LargeMatrixs) {
                Matrix matrix_rect = matrix[.., ..^3];

                Console.WriteLine($"test: {matrix_rect}");

                (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix_rect);

                Matrix sm = Matrix.VConcat(Matrix.FromDiagonals(s), Matrix.Zero(3, matrix.Size - 3));

                Matrix matrix2 = u * sm * v.T;

                Assert.IsTrue((matrix_rect - matrix2).Norm < 1e-25);
                Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
                Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
            }
        }

        [TestMethod()]
        public void SVDDecompose4x4Test() {
            Matrix matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 }, { 8, 13, 7, 2 } };

            (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix);

            Matrix matrix2 = u * Matrix.FromDiagonals(s) * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecompose4x3Test() {
            Matrix matrix = new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 }, { 8, 13, 7 } };

            (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix);

            Matrix sm = Matrix.VConcat(Matrix.FromDiagonals(s), Matrix.Zero(1, 3));

            Matrix matrix2 = u * sm * v.T;

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecompose3x4Test() {
            Matrix matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 } };

            (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix);

            Matrix sm = Matrix.HConcat(Matrix.FromDiagonals(s), Matrix.Zero(3, 1));

            Matrix matrix2 = u * sm * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecomposeDet0Test() {
            Matrix matrix = new double[,] { { 12, -51, 4 }, { -4, 24, -41 }, { -8, 48, -82 }, { -12, 72, -123 } };

            (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix);

            Matrix sm = Matrix.VConcat(Matrix.FromDiagonals(s), Matrix.Zero(1, 3));

            Matrix matrix2 = u * sm * v.T;

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecomposeEyeTest() {
            Matrix matrix = Matrix.Identity(4);

            (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix);

            Matrix matrix2 = u * Matrix.FromDiagonals(s) * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
        }

        [TestMethod()]
        public void SVDDecomposeZeroVectorTest() {
            Matrix matrix = Matrix.Identity(4);

            matrix[0, 0] = 0;

            (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix);

            Matrix matrix2 = u * Matrix.FromDiagonals(s) * v.T;

            Console.WriteLine(matrix2);

            Assert.IsTrue((matrix - matrix2).Norm < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(u.Det) - 1d) < 1e-25);
            Assert.IsTrue(ddouble.Abs(ddouble.Abs(v.Det) - 1d) < 1e-25);
        }
    }
}