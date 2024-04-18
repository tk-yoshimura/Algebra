using Algebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void FuncTest() {
            Matrix matrix1 = new double[,] { { 1, 2, 4 }, { 8, 16, 32 } };
            Matrix matrix2 = new double[,] { { 2, 3, 5 }, { 9, 17, 33 } };
            Matrix matrix3 = new double[,] { { 3, 4, 6 }, { 10, 18, 34 } };
            Matrix matrix4 = new double[,] { { 4, 5, 7 }, { 11, 19, 35 } };
            Matrix matrix5 = new double[,] { { 5, 6, 8 }, { 12, 20, 36 }, { 2, 1, 0 } };

            Assert.AreEqual(new Matrix(new double[,] { { 2, 4, 8 }, { 16, 32, 64 } }), Matrix.Func(v => 2 * v, matrix1));
            Assert.AreEqual(new Matrix(new double[,] { { 5, 8, 14 }, { 26, 50, 98 } }), Matrix.Func((v1, v2) => v1 + 2 * v2, matrix1, matrix2));
            Assert.AreEqual(new Matrix(new double[,] { { 17, 24, 38 }, { 66, 122, 234 } }), Matrix.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, matrix1, matrix2, matrix3));
            Assert.AreEqual(new Matrix(new double[,] { { 49, 64, 94 }, { 154, 274, 514 } }), Matrix.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix1, matrix2, matrix3, matrix4));
            Assert.AreEqual(new Matrix(new double[,] { { 49, 64, 94 }, { 154, 274, 514 } }), Matrix.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix1, matrix2, matrix3, matrix4));

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Func((v1, v2) => v1 + 2 * v2, matrix1, matrix5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Func((v1, v2) => v1 + 2 * v2, matrix5, matrix1);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, matrix1, matrix2, matrix5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, matrix1, matrix5, matrix2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, matrix5, matrix1, matrix2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix1, matrix2, matrix3, matrix5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix1, matrix2, matrix5, matrix3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix1, matrix5, matrix2, matrix3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, matrix5, matrix1, matrix2, matrix3);
            });
        }

        [TestMethod()]
        public void MapTest() {
            Vector vector1 = new(1, 2, 4, 8);
            Vector vector2 = new(2, 3, 5);

            Matrix m = ((v1, v2) => v1 + v2, vector1, vector2);

            Assert.AreEqual(new Matrix(new double[,] { { 3, 4, 6 }, { 4, 5, 7 }, { 6, 7, 9 }, { 10, 11, 13 } }), m);
        }
    }
}