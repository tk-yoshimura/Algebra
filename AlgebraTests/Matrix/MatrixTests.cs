using Algebra;
using DoubleDouble;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    [TestClass()]
    public partial class MatrixTests {
        [TestMethod()]
        public void MatrixTest() {
            Matrix matrix1 = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Matrix matrix2 = Matrix.Zero(3, 2);

            Assert.AreEqual(1, matrix1[0, 0]);
            Assert.AreEqual(2, matrix1[0, 1]);
            Assert.AreEqual(3, matrix1[0, 2]);
            Assert.AreEqual(4, matrix1[1, 0]);
            Assert.AreEqual(5, matrix1[1, 1]);
            Assert.AreEqual(6, matrix1[1, 2]);

            Assert.AreEqual(1, matrix1[^2, ^3]);
            Assert.AreEqual(2, matrix1[^2, ^2]);
            Assert.AreEqual(3, matrix1[^2, ^1]);
            Assert.AreEqual(4, matrix1[^1, ^3]);
            Assert.AreEqual(5, matrix1[^1, ^2]);
            Assert.AreEqual(6, matrix1[^1, ^1]);

            matrix1[0, 0] = 4;
            matrix1[0, 1] = 5;
            matrix1[^2, 2] = 6;
            matrix1[1, ^3] = 1;
            matrix1[^1, 1] = 2;
            matrix1[1, ^1] = 3;

            Assert.AreEqual(4, matrix1[0, 0]);
            Assert.AreEqual(5, matrix1[0, 1]);
            Assert.AreEqual(6, matrix1[^2, 2]);
            Assert.AreEqual(1, matrix1[1, ^3]);
            Assert.AreEqual(2, matrix1[^1, 1]);
            Assert.AreEqual(3, matrix1[1, ^1]);

            Assert.AreEqual(2, matrix1.Rows);
            Assert.AreEqual(3, matrix1.Columns);

            Assert.AreEqual(0, matrix2[0, 0]);
            Assert.AreEqual(0, matrix2[0, 1]);
            Assert.AreEqual(0, matrix2[1, 0]);
            Assert.AreEqual(0, matrix2[1, 1]);
            Assert.AreEqual(0, matrix2[2, 0]);
            Assert.AreEqual(0, matrix2[2, 1]);

            Assert.AreEqual(3, matrix2.Rows);
            Assert.AreEqual(2, matrix2.Columns);

            Assert.AreEqual(2, Matrix.Zero(2, 2).Size);

            Assert.ThrowsException<ArithmeticException>(() => {
                int n = Matrix.Zero(2, 3).Size;
            });

            string str = string.Empty;
            foreach ((int row_index, int col_index, ddouble val) in matrix1) {
                str += $"({row_index},{col_index},{val}),";
            }

            Assert.AreEqual("(0,0,4),(0,1,5),(0,2,6),(1,0,1),(1,1,2),(1,2,3),", str);
        }

        [TestMethod()]
        public void NormTest() {
            Matrix matrix1 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matrix matrix2 = Matrix.Zero(2, 3);

            Assert.AreEqual(ddouble.Sqrt(30), matrix1.Norm);
            Assert.AreEqual(0, matrix2.Norm);
            Assert.AreEqual(30, matrix1.SquareNorm);
        }

        [TestMethod()]
        public void SumTest() {
            Matrix matrix = new double[,] { { 1, 2 }, { 3, 4 } };

            Assert.AreEqual(10, matrix.Sum);
        }

        [TestMethod()]
        public void TransposeTest() {
            Matrix matrix1 = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Matrix matrix2 = matrix1.T;
            Matrix matrix3 = matrix2.T;

            Assert.AreEqual(new Matrix(new double[,] { { 1, 4 }, { 2, 5 }, { 3, 6 } }), matrix2);
            Assert.AreEqual(matrix1, matrix3);
        }

        [TestMethod()]
        public void HorizontalTest() {
            Vector vector = new(1, 2, 3);

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3 } }), vector.Horizontal);

            Matrix matrix = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Assert.AreEqual(new Vector(1, 2, 3), matrix.Horizontal(0));
            Assert.AreEqual(new Vector(4, 5, 6), matrix.Horizontal(1));
        }

        [TestMethod()]
        public void VerticalTest() {
            Vector vector = new(1, 2, 3);

            Assert.AreEqual(new Matrix(new double[,] { { 1 }, { 2 }, { 3 } }), vector.Vertical);

            Matrix matrix = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Assert.AreEqual(new Vector(1, 4), matrix.Vertical(0));
            Assert.AreEqual(new Vector(2, 5), matrix.Vertical(1));
            Assert.AreEqual(new Vector(3, 6), matrix.Vertical(2));
        }

        [TestMethod()]
        public void ZeroTest() {
            Matrix matrix = Matrix.Zero(2, 2);

            Assert.AreEqual(0, matrix[0, 0]);
            Assert.AreEqual(0, matrix[1, 0]);
            Assert.AreEqual(0, matrix[0, 1]);
            Assert.AreEqual(0, matrix[1, 1]);
        }

        [TestMethod()]
        public void FillTest() {
            Matrix matrix = Matrix.Fill(2, 2, value: 7);

            Assert.AreEqual(7, matrix[0, 0]);
            Assert.AreEqual(7, matrix[1, 0]);
            Assert.AreEqual(7, matrix[0, 1]);
            Assert.AreEqual(7, matrix[1, 1]);
        }

        [TestMethod()]
        public void IdentityTest() {
            Matrix matrix = Matrix.Identity(2);

            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(0, matrix[1, 0]);
            Assert.AreEqual(0, matrix[0, 1]);
            Assert.AreEqual(1, matrix[1, 1]);
        }

        [TestMethod()]
        public void InvalidTest() {
            Matrix matrix = Matrix.Invalid(2, 1);

            Assert.IsTrue(ddouble.IsNaN(matrix[0, 0]));
            Assert.IsTrue(ddouble.IsNaN(matrix[1, 0]));
        }

        [TestMethod()]
        public void IsEqualSizeTest() {
            Matrix matrix1 = Matrix.Zero(2, 2);
            Matrix matrix2 = Matrix.Zero(2, 3);

            Assert.AreEqual(matrix1.Shape, matrix1.Shape);
            Assert.AreNotEqual(matrix1.Shape, matrix2.Shape);
        }

        [TestMethod()]
        public void IsSquareTest() {
            Matrix matrix1 = Matrix.Zero(2, 2);
            Matrix matrix2 = Matrix.Zero(2, 3);

            Assert.IsTrue(Matrix.IsSquare(matrix1));
            Assert.IsFalse(Matrix.IsSquare(matrix2));
        }

        [TestMethod()]
        public void IsDiagonalTest() {
            Matrix matrix1 = new double[,] { { 1, 0 }, { 0, 2 } };
            Matrix matrix2 = new double[,] { { 1, 1 }, { 0, 2 } };
            Matrix matrix3 = new double[,] { { 1, 0, 0 }, { 0, 2, 0 } };
            Matrix matrix4 = new double[,] { { 1, 0 }, { 0, 2 }, { 0, 0 } };

            Assert.IsTrue(Matrix.IsDiagonal(matrix1));
            Assert.IsFalse(Matrix.IsDiagonal(matrix2));
            Assert.IsFalse(Matrix.IsDiagonal(matrix3));
            Assert.IsFalse(Matrix.IsDiagonal(matrix4));
        }

        [TestMethod()]
        public void IsZeroTest() {
            Matrix matrix = Matrix.Zero(2, 3);

            Assert.IsTrue(Matrix.IsZero(matrix));

            matrix[0, 0] = 1;

            Assert.IsFalse(Matrix.IsZero(matrix));
        }

        [TestMethod()]
        public void IsIdentityTest() {
            Matrix matrix = Matrix.Identity(2);

            Assert.IsTrue(Matrix.IsIdentity(matrix));

            matrix[0, 1] = 1;

            Assert.IsFalse(Matrix.IsIdentity(matrix));
        }

        [TestMethod()]
        public void IsSymmetricTest() {
            Matrix matrix1 = new double[,] { { 1, 2 }, { 2, 3 } };
            Matrix matrix2 = new double[,] { { 1, 2 }, { 3, 3 } };

            Assert.IsTrue(Matrix.IsSymmetric(matrix1));
            Assert.IsFalse(Matrix.IsSymmetric(matrix2));
        }

        [TestMethod()]
        public void IsValidTest() {
            Assert.IsTrue(Matrix.IsValid(Matrix.Zero(3, 2)));
            Assert.IsFalse(Matrix.IsValid(Matrix.Invalid(2, 3)));
        }

        [TestMethod()]
        public void IsRegularTest() {
            Matrix matrix1 = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Matrix matrix2 = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix3 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matrix matrix4 = new double[,] { { 0, 1 }, { 0, 0 } };

            Assert.IsTrue(Matrix.IsRegular(matrix1));
            Assert.IsTrue(Matrix.IsRegular(matrix2));
            Assert.IsTrue(Matrix.IsRegular(matrix3));
            Assert.IsFalse(Matrix.IsRegular(matrix4));
            Assert.IsFalse(Matrix.IsRegular(Matrix.Zero(2, 2)));
            Assert.IsTrue(Matrix.IsRegular(Matrix.Identity(2)));
        }

        [TestMethod()]
        public void DiagonalsTest() {
            Matrix matrix = new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            ddouble[] diagonals = matrix.Diagonals;

            Assert.AreEqual(1, diagonals[0]);
            Assert.AreEqual(5, diagonals[1]);
            Assert.AreEqual(9, diagonals[2]);
        }

        [TestMethod()]
        public void DetTest() {
            Matrix matrix1 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matrix matrix2 = new double[,] { { 1, 2 }, { 2, 4 } };

            Assert.AreEqual(-2, matrix1.Det);
            Assert.AreEqual(0, matrix2.Det);
        }

        [TestMethod()]
        public void TraceTest() {
            Matrix matrix1 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matrix matrix2 = new double[,] { { 1, 2 }, { 2, 4 } };

            Assert.AreEqual(5, matrix1.Trace);
            Assert.AreEqual(5, matrix2.Trace);
        }

        [TestMethod()]
        public void CopyTest() {
            Matrix matrix1 = Matrix.Zero(2, 2);
            Matrix matrix2 = matrix1.Copy();

            matrix2[1, 1] = 1;

            Assert.AreEqual(0, matrix1[1, 1]);
            Assert.AreEqual(1, matrix2[1, 1]);
        }

        [TestMethod()]
        public void ToStringTest() {
            Matrix matrix1 = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Matrix matrix2 = new double[,] { { 1, 2, 3 } };
            Matrix matrix3 = new double[,] { { 1 }, { 2 }, { 3 } };
            Matrix matrix4 = Matrix.Invalid(2, 2);

            Assert.AreEqual("[ [ 1, 2, 3 ], [ 4, 5, 6 ] ]", matrix1.ToString());
            Assert.AreEqual("[ [ 1, 2, 3 ] ]", matrix2.ToString());
            Assert.AreEqual("[ [ 1 ], [ 2 ], [ 3 ] ]", matrix3.ToString());
            Assert.AreEqual("invalid", matrix4.ToString());

            Assert.AreEqual("[ [ 1.0000e0, 2.0000e0, 3.0000e0 ], [ 4.0000e0, 5.0000e0, 6.0000e0 ] ]", matrix1.ToString("e4"));
            Assert.AreEqual("[ [ 1.0000e0, 2.0000e0, 3.0000e0 ], [ 4.0000e0, 5.0000e0, 6.0000e0 ] ]", $"{matrix1:e4}");
        }

        [TestMethod()]
        public void SampleTest() {
            // solve for v: Av=x
            Matrix a = new double[,] { { 1, 2 }, { 3, 4 } };
            Vector x = (4, 3);

            Vector v = Matrix.Solve(a, x);

            Console.WriteLine(v);
        }
    }
}