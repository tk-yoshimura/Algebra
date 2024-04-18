using Algebra;
using DoubleDouble;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void OperatorTest() {
            Matrix matrix1 = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Matrix matrix2 = new double[,] { { 7, 8, 9 }, { 1, 2, 3 } };
            Matrix matrix3 = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Vector vector1 = new(5, 4, 3);
            Vector vector2 = new(5, 4);

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }), +matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { -1, -2, -3 }, { -4, -5, -6 } }), -matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { 8, 10, 12 }, { 5, 7, 9 } }), matrix1 + matrix2);
            Assert.AreEqual(new Matrix(new double[,] { { 8, 10, 12 }, { 5, 7, 9 } }), matrix2 + matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { 3, 4, 5 }, { 6, 7, 8 } }), matrix1 + 2);
            Assert.AreEqual(new Matrix(new double[,] { { 3, 4, 5 }, { 6, 7, 8 } }), 2 + matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { -6, -6, -6 }, { 3, 3, 3 } }), matrix1 - matrix2);
            Assert.AreEqual(new Matrix(new double[,] { { 6, 6, 6 }, { -3, -3, -3 } }), matrix2 - matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { -1, 0, 1 }, { 2, 3, 4 } }), matrix1 - 2);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 0, -1 }, { -2, -3, -4 } }), 2 - matrix1);

            Assert.AreEqual(new Matrix(new double[,] { { 22, 28 }, { 49, 64 } }), matrix1 * matrix3);
            Assert.AreEqual(new Matrix(new double[,] { { 9, 12, 15 }, { 19, 26, 33 }, { 29, 40, 51 } }), matrix3 * matrix1);

            Assert.AreEqual(new Matrix(new double[,] { { 7, 16, 27 }, { 4, 10, 18 } }), Matrix.ElementwiseMul(matrix1, matrix2));
            Assert.AreEqual(new Matrix(new ddouble[,] { { 7, 4, 3 }, { 0.25, "0.4", 0.5 } }), Matrix.ElementwiseDiv(matrix2, matrix1));

            Assert.AreEqual(new Matrix(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }), matrix1 * 2);
            Assert.AreEqual(new Matrix(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }), 2 * matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { 0.5, 1, 1.5 }, { 2, 2.5, 3 } }), matrix1 / 2);
            Assert.AreEqual(new Matrix(new ddouble[,] { { 2, 1, (ddouble)2 / 3 }, { 0.5, "0.4", (ddouble)2 / 6 } }), 2 / matrix1);

            Assert.AreEqual(new Vector(22, 58), matrix1 * vector1);
            Assert.AreEqual(new Vector(32, 44), vector1 * matrix3);
            Assert.AreEqual(new Vector(21, 30, 39), vector2 * matrix1);
            Assert.AreEqual(new Vector(13, 31, 49), matrix3 * vector2);

            Assert.AreEqual(new Matrix(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }), matrix1 * 2);
            Assert.AreEqual(new Matrix(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }), 2 * matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { 0.5, 1, 1.5 }, { 2, 2.5, 3 } }), matrix1 / 2);
        }

        [TestMethod()]
        public void OperatorEqualTest() {
            Matrix matrix = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Assert.IsTrue(matrix == new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }));
            Assert.IsTrue(matrix != new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 7 } }));
            Assert.IsTrue(matrix != new Matrix(new double[,] { { 1, 2 }, { 4, 5 } }));
            Assert.IsTrue(matrix != new Matrix(new double[,] { { 1, 2, 3, 4 }, { 4, 5, 6, 7 } }));
            Assert.IsTrue(matrix != new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }));
            Assert.IsFalse(matrix == null);
            Assert.IsTrue(matrix != null);
            Assert.IsTrue(matrix != new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, double.NaN } }));
        }

        [TestMethod()]
        public void EqualsTest() {
            Matrix matrix = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Assert.IsTrue(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } })));
            Assert.IsFalse(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 7 } })));
            Assert.IsFalse(matrix.Equals(new Matrix(new double[,] { { 1, 2 }, { 4, 5 } })));
            Assert.IsFalse(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3, 4 }, { 4, 5, 6, 7 } })));
            Assert.IsFalse(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } })));
            Assert.IsFalse(matrix.Equals(null));
            Assert.IsFalse(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, double.NaN } })));
        }
    }
}