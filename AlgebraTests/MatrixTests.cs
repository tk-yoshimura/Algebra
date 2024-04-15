using DoubleDouble;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algebra.Tests {
    [TestClass()]
    public class MatrixTests {
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
        public void RangeIndexerGetterTest() {
            Matrix matrix = new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } };

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix[.., ..]);

            Assert.AreEqual(new Matrix(new double[,] { { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix[1.., ..]);
            Assert.AreEqual(new Matrix(new double[,] { { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix[2.., ..]);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 } }), matrix[..^1, ..]);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 } }), matrix[..3, ..]);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 } }), matrix[..^2, ..]);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 } }), matrix[..2, ..]);

            Assert.AreEqual(new Matrix(new double[,] { { 11, 12, 13, 14, 15 } }), matrix[2..3, ..]);
            Assert.AreEqual(new Matrix(new double[,] { { 11, 12, 13, 14, 15 } }), matrix[2..^1, ..]);

            Assert.AreEqual(new Matrix(new double[,] { { 2, 3, 4, 5 }, { 7, 8, 9, 10 }, { 12, 13, 14, 15 }, { 17, 18, 19, 20 } }), matrix[.., 1..]);
            Assert.AreEqual(new Matrix(new double[,] { { 3, 4, 5 }, { 8, 9, 10 }, { 13, 14, 15 }, { 18, 19, 20 } }), matrix[.., 2..]);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4 }, { 6, 7, 8, 9 }, { 11, 12, 13, 14 }, { 16, 17, 18, 19 } }), matrix[.., ..^1]);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4 }, { 6, 7, 8, 9 }, { 11, 12, 13, 14 }, { 16, 17, 18, 19 } }), matrix[.., ..4]);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3 }, { 6, 7, 8 }, { 11, 12, 13 }, { 16, 17, 18 } }), matrix[.., ..^2]);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3 }, { 6, 7, 8 }, { 11, 12, 13 }, { 16, 17, 18 } }), matrix[.., ..3]);

            Assert.AreEqual(new Matrix(new double[,] { { 2, 3, 4 }, { 7, 8, 9 }, { 12, 13, 14 }, { 17, 18, 19 } }), matrix[.., 1..4]);
            Assert.AreEqual(new Matrix(new double[,] { { 2, 3, 4 }, { 7, 8, 9 }, { 12, 13, 14 }, { 17, 18, 19 } }), matrix[.., 1..^1]);

            Assert.AreEqual(new Matrix(new double[,] { { 7, 8, 9 }, { 12, 13, 14 } }), matrix[1..^1, 1..^1]);

            Assert.AreEqual(new Vector(6, 7, 8, 9, 10), matrix[1, ..]);
            Assert.AreEqual(new Vector(7, 8, 9, 10), matrix[1, 1..]);
            Assert.AreEqual(new Vector(6, 7, 8, 9), matrix[1, ..^1]);
            Assert.AreEqual(new Vector(7, 8, 9), matrix[1, 1..^1]);

            Assert.AreEqual(new Vector(6, 7, 8, 9, 10), matrix[^3, ..]);
            Assert.AreEqual(new Vector(7, 8, 9, 10), matrix[^3, 1..]);
            Assert.AreEqual(new Vector(6, 7, 8, 9), matrix[^3, ..^1]);
            Assert.AreEqual(new Vector(7, 8, 9), matrix[^3, 1..^1]);

            Assert.AreEqual(new Vector(3, 8, 13, 18), matrix[.., 2]);
            Assert.AreEqual(new Vector(8, 13, 18), matrix[1.., 2]);
            Assert.AreEqual(new Vector(3, 8, 13), matrix[..^1, 2]);
            Assert.AreEqual(new Vector(8, 13), matrix[1..^1, 2]);

            Assert.AreEqual(new Vector(3, 8, 13, 18), matrix[.., ^3]);
            Assert.AreEqual(new Vector(8, 13, 18), matrix[1.., ^3]);
            Assert.AreEqual(new Vector(3, 8, 13), matrix[..^1, ^3]);
            Assert.AreEqual(new Vector(8, 13), matrix[1..^1, ^3]);
        }

        [TestMethod()]
        public void RangeIndexerSetterTest() {
            Matrix matrix_src = new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } };
            Matrix matrix_dst;

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..] = matrix_src;
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[1.., ..] = matrix_src[1.., ..];
            Assert.AreEqual(new Matrix(new double[,] { { 0, 0, 0, 0, 0 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[2.., ..] = matrix_src[2.., ..];
            Assert.AreEqual(new Matrix(new double[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[..^1, ..] = matrix_src[..^1, ..];
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[..3, ..] = matrix_src[..3, ..];
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[..^2, ..] = matrix_src[..^2, ..];
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[..2, ..] = matrix_src[..2, ..];
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[2..3, ..] = matrix_src[2..3, ..];
            Assert.AreEqual(new Matrix(new double[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 11, 12, 13, 14, 15 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[2..^1, ..] = matrix_src[2..^1, ..];
            Assert.AreEqual(new Matrix(new double[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 11, 12, 13, 14, 15 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., 1..] = matrix_src[.., 1..];
            Assert.AreEqual(new Matrix(new double[,] { { 0, 2, 3, 4, 5 }, { 0, 7, 8, 9, 10 }, { 0, 12, 13, 14, 15 }, { 0, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., 2..] = matrix_src[.., 2..];
            Assert.AreEqual(new Matrix(new double[,] { { 0, 0, 3, 4, 5 }, { 0, 0, 8, 9, 10 }, { 0, 0, 13, 14, 15 }, { 0, 0, 18, 19, 20 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..^1] = matrix_src[.., ..^1];
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 0 }, { 6, 7, 8, 9, 0 }, { 11, 12, 13, 14, 0 }, { 16, 17, 18, 19, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..4] = matrix_src[.., ..4];
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 0 }, { 6, 7, 8, 9, 0 }, { 11, 12, 13, 14, 0 }, { 16, 17, 18, 19, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..^2] = matrix_src[.., ..^2];
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 0, 0 }, { 6, 7, 8, 0, 0 }, { 11, 12, 13, 0, 0 }, { 16, 17, 18, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., ..3] = matrix_src[.., ..3];
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 0, 0 }, { 6, 7, 8, 0, 0 }, { 11, 12, 13, 0, 0 }, { 16, 17, 18, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., 1..4] = matrix_src[.., 1..4];
            Assert.AreEqual(new Matrix(new double[,] { { 0, 2, 3, 4, 0 }, { 0, 7, 8, 9, 0 }, { 0, 12, 13, 14, 0 }, { 0, 17, 18, 19, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[.., 1..^1] = matrix_src[.., 1..^1];
            Assert.AreEqual(new Matrix(new double[,] { { 0, 2, 3, 4, 0 }, { 0, 7, 8, 9, 0 }, { 0, 12, 13, 14, 0 }, { 0, 17, 18, 19, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[1..^1, 1..^1] = matrix_src[1..^1, 1..^1];
            Assert.AreEqual(new Matrix(new double[,] { { 0, 0, 0, 0, 0 }, { 0, 7, 8, 9, 0 }, { 0, 12, 13, 14, 0 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
            matrix_dst[0..^2, 0..^2] = matrix_src[1..^1, 1..^1];
            Assert.AreEqual(new Matrix(new double[,] { { 7, 8, 9, 0, 0 }, { 12, 13, 14, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } }), matrix_dst);

            matrix_dst = matrix_src.Copy();
            matrix_dst[1, ..] = new Vector(-1, -2, -3, -4, -5);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { -1, -2, -3, -4, -5 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = matrix_src.Copy();
            matrix_dst[^2, ..] = new Vector(-1, -2, -3, -4, -5);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { -1, -2, -3, -4, -5 }, { 16, 17, 18, 19, 20 } }), matrix_dst);

            matrix_dst = matrix_src.Copy();
            matrix_dst[.., 1] = new Vector(-1, -2, -3, -4);
            Assert.AreEqual(new Matrix(new double[,] { { 1, -1, 3, 4, 5 }, { 6, -2, 8, 9, 10 }, { 11, -3, 13, 14, 15 }, { 16, -4, 18, 19, 20 } }), matrix_dst);

            matrix_dst = matrix_src.Copy();
            matrix_dst[.., ^2] = new Vector(-1, -2, -3, -4);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3, -1, 5 }, { 6, 7, 8, -2, 10 }, { 11, 12, 13, -3, 15 }, { 16, 17, 18, -4, 20 } }), matrix_dst);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
                matrix_dst[0..^2, 0..^2] = matrix_src[1..^2, 1..^1];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
                matrix_dst[0..^2, 0..^2] = matrix_src[1.., 1..^1];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
                matrix_dst[0..^2, 0..^2] = matrix_src[1..^1, 1..^2];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                matrix_dst = Matrix.Zero(matrix_src.Rows, matrix_src.Columns);
                matrix_dst[0..^2, 0..^2] = matrix_src[1..^1, 1..];
            });
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
        public void InverseTest() {
            Matrix matrix1 = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Matrix matrix2 = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix3 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matrix matrix4 = new double[,] { { 0, 1 }, { 0, 0 } };
            Matrix matrix5 = new double[,] { { 2, 1, 1, 2 }, { 4, 2, 3, 1 }, { -2, -2, 0, -1 }, { 1, 1, 2, 6 } };
            Matrix matrix6 = new double[,] {
                { 1, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 2, 1, 1, 0 },
                { 0, 0, 0, 1, 4, 2, 1, 1 },
                { 0, 0, 0, 0, 5, 4, 2, 1 },
                { 0, 0, 0, 0, 7, 5, 4, 2 },
                { 0, 0, 0, 0, 6, 7, 5, 4 },
                { 0, 0, 0, 0, 8, 6, 7, 5 },
            };

            Assert.AreEqual(matrix1.Rows, matrix1.T.Columns);
            Assert.AreEqual(matrix1.Columns, matrix1.T.Rows);

            Assert.AreEqual(matrix2.Rows, matrix2.T.Columns);
            Assert.AreEqual(matrix2.Columns, matrix2.T.Rows);

            Assert.AreEqual(matrix3.Rows, matrix3.T.Columns);
            Assert.AreEqual(matrix3.Columns, matrix3.T.Rows);

            Assert.IsTrue((matrix1 * matrix1.Inverse * matrix1 - matrix1).Norm < 1e-28);
            Assert.IsTrue((matrix2 * matrix2.Inverse * matrix2 - matrix2).Norm < 1e-28);
            Assert.IsTrue((matrix3 * matrix3.Inverse * matrix3 - matrix3).Norm < 1e-28);

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }), matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } }), matrix2);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }), matrix3);

            Assert.IsTrue((matrix1.Inverse * matrix1 * matrix1.Inverse - matrix1.Inverse).Norm < 1e-28);
            Assert.IsTrue((matrix2.Inverse * matrix2 * matrix2.Inverse - matrix2.Inverse).Norm < 1e-28);
            Assert.IsTrue((matrix3.Inverse * matrix3 * matrix3.Inverse - matrix3.Inverse).Norm < 1e-28);

            Assert.IsTrue(((matrix1 * matrix1.Inverse).T - matrix1 * matrix1.Inverse).Norm < 1e-28);
            Assert.IsTrue(((matrix2 * matrix2.Inverse).T - matrix2 * matrix2.Inverse).Norm < 1e-28);
            Assert.IsTrue(((matrix3 * matrix3.Inverse).T - matrix3 * matrix3.Inverse).Norm < 1e-28);

            Assert.IsTrue(((matrix1.Inverse * matrix1).T - matrix1.Inverse * matrix1).Norm < 1e-28);
            Assert.IsTrue(((matrix2.Inverse * matrix2).T - matrix2.Inverse * matrix2).Norm < 1e-28);
            Assert.IsTrue(((matrix3.Inverse * matrix3).T - matrix3.Inverse * matrix3).Norm < 1e-28);

            Assert.IsFalse(Matrix.IsValid(matrix4.Inverse));
            Assert.IsFalse(Matrix.IsValid(Matrix.Zero(2, 2).Inverse));
            Assert.IsTrue(Matrix.IsIdentity(Matrix.Identity(2).Inverse));

            Assert.IsTrue((matrix5.Inverse.Inverse - matrix5).Norm < 1e-28);
            Assert.IsTrue((matrix6.Inverse.Inverse - matrix6).Norm < 1e-28);

            Assert.IsTrue(((matrix5 * 1e+100).Inverse.Inverse - (matrix5 * 1e+100)).Norm < 1e+72);
            Assert.IsTrue(((matrix6 * 1e+100).Inverse.Inverse - (matrix6 * 1e+100)).Norm < 1e+72);

            Assert.IsTrue(((matrix5 * 1e-100).Inverse.Inverse - (matrix5 * 1e-100)).Norm < 1e-128);
            Assert.IsTrue(((matrix6 * 1e-100).Inverse.Inverse - (matrix6 * 1e-100)).Norm < 1e-128);
        }

        [TestMethod()]
        public void SolveTest() {
            Matrix matrix1 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matrix matrix2 = new double[,] { { 0, 1 }, { 0, 0 } };
            Matrix matrix3 = new double[,] { { 1, 2, -3 }, { 2, -1, 3 }, { -3, 2, 1 } };
            Matrix matrix4 = new double[,] { { 2, 1, 1, 2 }, { 4, 2, 3, 1 }, { -2, -2, 0, -1 }, { 1, 1, 2, 6 } };
            Matrix matrix5 = new double[,] {
                { 1, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 2, 1, 1, 0 },
                { 0, 0, 0, 1, 4, 2, 1, 1 },
                { 0, 0, 0, 0, 5, 4, 2, 1 },
                { 0, 0, 0, 0, 7, 5, 4, 2 },
                { 0, 0, 0, 0, 6, 7, 5, 4 },
                { 0, 0, 0, 0, 8, 6, 7, 5 },
            };

            Vector vector1 = new(4, 3);
            Vector vector2 = new(3, 1);
            Vector vector3 = new(5, 4, 1);
            Vector vector4 = new(1, -2, 3, 2);
            Vector vector5 = new(5, -3, 1, -2, 6, 2, 4, 2);

            Assert.IsTrue((matrix1.Inverse * vector1 - Matrix.Solve(matrix1, vector1)).Norm < 1e-28, $"{Matrix.Solve(matrix1, vector1)}");
            Assert.IsTrue((matrix3.Inverse * vector3 - Matrix.Solve(matrix3, vector3)).Norm < 1e-28);
            Assert.IsFalse(Vector.IsValid(Matrix.Solve(matrix2, vector2)));
            Assert.IsTrue((matrix4.Inverse * vector4 - Matrix.Solve(matrix4, vector4)).Norm < 1e-28);
            Assert.IsTrue((matrix5.Inverse * vector5 - Matrix.Solve(matrix5, vector5)).Norm < 1e-28);

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }), matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { 0, 1 }, { 0, 0 } }), matrix2);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, -3 }, { 2, -1, 3 }, { -3, 2, 1 } }), matrix3);

            Assert.AreEqual(new Vector(4, 3), vector1);
            Assert.AreEqual(new Vector(3, 1), vector2);
            Assert.AreEqual(new Vector(5, 4, 1), vector3);

            Assert.IsTrue(((matrix4 * 1e+100).Inverse * vector4 - Matrix.Solve(matrix4 * 1e+100, vector4)).Norm < 1e-128);
            Assert.IsTrue(((matrix5 * 1e+100).Inverse * vector5 - Matrix.Solve(matrix5 * 1e+100, vector5)).Norm < 1e-128);

            Assert.IsTrue(((matrix4 * 1e-100).Inverse * vector4 - Matrix.Solve(matrix4 * 1e-100, vector4)).Norm < 1e+72);
            Assert.IsTrue(((matrix5 * 1e-100).Inverse * vector5 - Matrix.Solve(matrix5 * 1e-100, vector5)).Norm < 1e+72);
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
        public void LUDecomposeTest() {
            Matrix matrix = new double[,] { { 2, 3, 1, 2 }, { 4, 1, 3, -2 }, { 2, 2, -3, 1 }, { 1, -3, 2, 4 } };

            (Matrix lower, Matrix upper) = Matrix.LU(matrix);

            Assert.AreEqual(new Matrix(new double[,] { { 2, 3, 1, 2 }, { 4, 1, 3, -2 }, { 2, 2, -3, 1 }, { 1, -3, 2, 4 } }), matrix);

            foreach (var diagonal in lower.Diagonals) {
                Assert.AreEqual(1, diagonal);
            }

            Assert.IsTrue((matrix - lower * upper).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeTest() {
            Matrix matrix = new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.AreEqual(new Matrix(new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } }), matrix);

            Assert.AreEqual(0d, r[1, 0]);
            Assert.AreEqual(0d, r[2, 0]);
            Assert.AreEqual(0d, r[2, 1]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-12);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeTest2() {
            Matrix matrix = new double[,] { { 1, 2 }, { 3, 4 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.AreEqual(0d, r[1, 0]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-12);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeTest3() {
            Matrix matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 }, { 8, 13, 7, 2 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.AreEqual(0d, r[1, 0]);
            Assert.AreEqual(0d, r[2, 0]);
            Assert.AreEqual(0d, r[3, 0]);
            Assert.AreEqual(0d, r[2, 1]);
            Assert.AreEqual(0d, r[3, 1]);
            Assert.AreEqual(0d, r[3, 2]);

            Assert.IsTrue((matrix - q * r).Norm < 1e-12);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeEyeTest() {
            Matrix matrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.IsTrue((matrix - q * r).Norm < 1e-12);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void QRDecomposeEyeEpsTest() {
            Matrix matrix = new double[,] { { 1, 1e-30, 0 }, { 2e-30, 1, 1e-30 }, { 0, -1e-30, 1 } };

            (Matrix q, Matrix r) = Matrix.QR(matrix);

            Assert.IsTrue((matrix - q * r).Norm < 1e-12);
            Assert.IsTrue((q * q.T - Matrix.Identity(matrix.Size)).Norm < 1e-31);
        }

        [TestMethod()]
        public void SVDDecomposeTest() {
            Matrix matrix = new double[,] { { 12, -51, 4, 6 }, { 6, 167, -68, 3 }, { -4, 24, -41, 12 }, { 8, 13, 7, 2 } };

            (Matrix u, Vector s, Matrix v) = Matrix.SVD(matrix);

            Matrix m2 = u * Matrix.FromDiagonals(s) * v.T;

            Console.WriteLine(m2);
        }

        [TestMethod()]
        public void EigenValuesTest() {
            Matrix matrix = new double[,] { { 1, 2 }, { 4, 5 } };
            ddouble[] eigen_values = Matrix.EigenValues(matrix);

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2 }, { 4, 5 } }), matrix);

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
            Matrix matrix = new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 8, 7, 9 } };

            (ddouble[] eigen_values, Vector[] eigen_vectors) = Matrix.EigenValueVectors(matrix);
            Vector eigen_values_expected = Matrix.EigenValues(matrix);

            Assert.IsTrue((eigen_values - eigen_values_expected).Norm < 1e-30);

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 8, 7, 9 } }), matrix);

            for (int i = 0; i < matrix.Size; i++) {
                ddouble eigen_value = eigen_values[i];
                Vector eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-15);
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

            Assert.AreEqual(-1, matrix1.Trace);
            Assert.AreEqual(1, matrix2.Trace);
        }

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

        [TestMethod()]
        public void ConcatTest() {
            Matrix matrix3x2 = Matrix.Fill(3, 2, value: 2);
            Matrix matrix3x4 = Matrix.Fill(3, 4, value: 3);
            Matrix matrix3x6 = Matrix.Fill(3, 6, value: 4);
            Matrix matrix5x2 = Matrix.Fill(5, 2, value: 5);
            Matrix matrix5x4 = Matrix.Fill(5, 4, value: 6);
            Matrix matrix5x6 = Matrix.Fill(5, 6, value: 7);
            Matrix matrix7x2 = Matrix.Fill(7, 2, value: 8);
            Matrix matrix7x4 = Matrix.Fill(7, 4, value: 9);
            Matrix matrix7x6 = Matrix.Fill(7, 6, value: 10);

            Vector vector3x1 = Vector.Fill(3, value: -1);
            Vector vector5x1 = Vector.Fill(5, value: -2);
            Vector vector7x1 = Vector.Fill(7, value: -3);

            Matrix matrix1 = Matrix.Concat(new object[,] { { matrix3x2 }, { matrix5x2 }, { matrix7x2 } });
            Assert.AreEqual(matrix3x2, matrix1[..3, ..]);
            Assert.AreEqual(matrix5x2, matrix1[3..8, ..]);
            Assert.AreEqual(matrix7x2, matrix1[8.., ..]);

            Matrix matrix2 = Matrix.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix2[.., ..2]);
            Assert.AreEqual(matrix3x4, matrix2[.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix2[.., 6..]);

            Matrix matrix3 = Matrix.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix3[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix3[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix3[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix3[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix3[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix3[8.., 2..6]);
            Assert.AreEqual(matrix3x6, matrix3[..3, 6..]);
            Assert.AreEqual(matrix5x6, matrix3[3..8, 6..]);
            Assert.AreEqual(matrix7x6, matrix3[8.., 6..]);

            Matrix matrix4 = Matrix.Concat(new object[,] { { matrix3x2, vector3x1, matrix3x6 } });
            Assert.AreEqual(matrix3x2, matrix4[.., ..2]);
            Assert.AreEqual(vector3x1, matrix4[.., 2]);
            Assert.AreEqual(matrix3x6, matrix4[.., 3..]);

            Matrix matrix5 = Matrix.Concat(new object[,] { { vector3x1, matrix3x2, matrix3x6 } });
            Assert.AreEqual(vector3x1, matrix5[.., 0]);
            Assert.AreEqual(matrix3x2, matrix5[.., 1..3]);
            Assert.AreEqual(matrix3x6, matrix5[.., 3..]);

            Matrix matrix6 = Matrix.Concat(new object[,] { { matrix3x2, matrix3x6, vector3x1 } });
            Assert.AreEqual(matrix3x2, matrix6[.., 0..2]);
            Assert.AreEqual(matrix3x6, matrix6[.., 2..8]);
            Assert.AreEqual(vector3x1, matrix6[.., 8]);

            Matrix matrix7 = Matrix.Concat(new object[,] { { matrix3x2, matrix3x4, vector3x1, matrix3x6 }, { matrix5x2, matrix5x4, vector5x1, matrix5x6 }, { matrix7x2, matrix7x4, vector7x1, matrix7x6 } });
            Assert.AreEqual(matrix3x2, matrix7[..3, ..2]);
            Assert.AreEqual(matrix5x2, matrix7[3..8, ..2]);
            Assert.AreEqual(matrix7x2, matrix7[8.., ..2]);
            Assert.AreEqual(matrix3x4, matrix7[..3, 2..6]);
            Assert.AreEqual(matrix5x4, matrix7[3..8, 2..6]);
            Assert.AreEqual(matrix7x4, matrix7[8.., 2..6]);
            Assert.AreEqual(vector3x1, matrix7[..3, 6]);
            Assert.AreEqual(vector5x1, matrix7[3..8, 6]);
            Assert.AreEqual(vector7x1, matrix7[8.., 6]);
            Assert.AreEqual(matrix3x6, matrix7[..3, 7..]);
            Assert.AreEqual(matrix5x6, matrix7[3..8, 7..]);
            Assert.AreEqual(matrix7x6, matrix7[8.., 7..]);

            Matrix matrix8 = Matrix.Concat(new object[,] { { vector3x1 }, { vector5x1 }, { vector7x1 } });
            Assert.AreEqual(vector3x1, matrix8[..3, 0]);
            Assert.AreEqual(vector5x1, matrix8[3..8, 0]);
            Assert.AreEqual(vector7x1, matrix8[8.., 0]);

            Matrix matrix9 = Matrix.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { (int)(-10), (long)(-20L), ddouble.E, 4.3d, 4.2f, "4.1" },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix9[8, 0]);
            Assert.AreEqual(-20, matrix9[8, 1]);
            Assert.AreEqual(ddouble.E, matrix9[8, 2]);
            Assert.AreEqual((ddouble)4.3d, matrix9[8, 3]);
            Assert.AreEqual((ddouble)4.2f, matrix9[8, 4]);
            Assert.AreEqual((ddouble)"4.1", matrix9[8, 5]);

            Matrix matrix10 = Matrix.Concat(
                new object[,] {
                    { vector3x1, vector3x1, vector3x1, vector3x1, vector3x1, vector3x1 },
                    { (int)(-10), (long)(-20L), ddouble.E, 4.3d, 4.2f, "4.1" },
                    { vector5x1, vector5x1, vector5x1, vector5x1, vector5x1, vector5x1 },
                    { vector7x1, vector7x1, vector7x1, vector7x1, vector7x1, vector7x1 } }
            );

            Assert.AreEqual(-10, matrix10[3, 0]);
            Assert.AreEqual(-20, matrix10[3, 1]);
            Assert.AreEqual(ddouble.E, matrix10[3, 2]);
            Assert.AreEqual((ddouble)4.3d, matrix10[3, 3]);
            Assert.AreEqual((ddouble)4.2f, matrix10[3, 4]);
            Assert.AreEqual((ddouble)"4.1", matrix10[3, 5]);

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Concat(new object[,] { { matrix3x2 }, { matrix3x4 }, { matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Concat(new object[,] { { matrix3x2, matrix5x2, matrix7x2 }, { matrix3x4, matrix5x4, matrix7x4 }, { matrix3x6, matrix5x6, matrix7x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Concat(new object[,] { { matrix3x2 }, { matrix5x4 }, { matrix7x2 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Concat(new object[,] { { matrix5x2, matrix3x4, matrix3x6 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Concat(new object[,] { { matrix3x2, matrix3x4, matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Matrix.Concat(new object[,] { { matrix3x2, 'b', matrix3x6 }, { matrix5x2, matrix5x4, matrix5x6 }, { matrix7x2, matrix7x4, matrix7x4 } });
            });
        }

        [TestMethod()]
        public void VConcatTest() {
            Matrix matrix = Matrix.VConcat(
                Vector.Fill(3, 1),
                Vector.Fill(3, 2),
                Vector.Fill(3, 3),
                Vector.Fill(3, 4),
                Vector.Fill(3, 5)
            );

            Matrix matrix_expected = new double[5, 3] {
                { 1, 1, 1 },
                { 2, 2, 2 },
                { 3, 3, 3 },
                { 4, 4, 4 },
                { 5, 5, 5 },
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
        }

        [TestMethod()]
        public void HConcatTest() {
            Matrix matrix = Matrix.HConcat(
                Vector.Fill(3, 1),
                Vector.Fill(3, 2),
                Vector.Fill(3, 3),
                Vector.Fill(3, 4),
                Vector.Fill(3, 5)
            );

            Matrix matrix_expected = new double[3, 5] {
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 },
                { 1, 2, 3, 4, 5 }
            };

            Console.WriteLine(matrix);

            Assert.AreEqual(matrix_expected, matrix);
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