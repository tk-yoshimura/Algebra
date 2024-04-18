using Algebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void QRDecomposeTest() {
            foreach (Matrix matrix in MatrixTestCases.RegularMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix q, Matrix r) = Matrix.QR(matrix);

                Assert.IsTrue((matrix - q * r).Norm < 1e-25);
                Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void QRDecomposeLargeTest() {
            foreach (Matrix matrix in MatrixTestCases.LargeMatrixs) {
                Console.WriteLine($"test: {matrix}");

                (Matrix q, Matrix r) = Matrix.QR(matrix);

                Assert.IsTrue((matrix - q * r).Norm < 1e-25);
                Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void QRDecompose2x2Test() {
            Matrix matrix = new double[,] { { 1, 2 }, { 3, 4 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.AreEqual(0d, r[1, 0]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecompose3x3Test() {
            Matrix matrix = new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.AreEqual(new Matrix(new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } }), matrix);

            Assert.AreEqual(0d, r[1, 0]);
            Assert.AreEqual(0d, r[2, 0]);
            Assert.AreEqual(0d, r[2, 1]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecompose4x4Test() {
            Matrix matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 }, { 8, 13, 7, 2 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.AreEqual(0d, r[1, 0]);
            Assert.AreEqual(0d, r[2, 0]);
            Assert.AreEqual(0d, r[3, 0]);
            Assert.AreEqual(0d, r[2, 1]);
            Assert.AreEqual(0d, r[3, 1]);
            Assert.AreEqual(0d, r[3, 2]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeEyeTest() {
            Matrix matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeEyeEpsTest() {
            Matrix matrix = new double[,] { { 1, 1e-30, 0 }, { 2e-30, 1, 1e-30 }, { 0, -1e-30, 1 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.IsTrue((matrix - q * r).Norm < 1e-25);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }
    }
}