﻿using DoubleDouble;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algebra.Tests {
    [TestClass()]
    public class MatrixTests {
        [TestMethod()]
        public void MatrixTest() {
            Matrix matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
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

            Assert.ThrowsException<InvalidOperationException>(() => {
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
            Matrix matrix = new(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } });

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
            Matrix matrix_src = new(new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } });
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
            Matrix matrix = new(new double[,] { { 1, 2 }, { 3, 4 } });

            Assert.AreEqual(ddouble.Sqrt(30), matrix.Norm);
            Assert.AreEqual(30, matrix.SquareNorm);
        }

        [TestMethod()]
        public void SumTest() {
            Matrix matrix = new(new double[,] { { 1, 2 }, { 3, 4 } });

            Assert.AreEqual(10, matrix.Sum);
        }

        [TestMethod()]
        public void TransposeTest() {
            Matrix matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = matrix1.Transpose;
            Matrix matrix3 = matrix2.Transpose;

            Assert.AreEqual(new Matrix(new double[,] { { 1, 4 }, { 2, 5 }, { 3, 6 } }), matrix2);
            Assert.AreEqual(matrix1, matrix3);
        }

        [TestMethod()]
        public void InverseTest() {
            Matrix matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = new(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Matrix matrix3 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix4 = new(new double[,] { { 0, 1 }, { 0, 0 } });
            Matrix matrix5 = new(new double[,] { { 2, 1, 1, 2 }, { 4, 2, 3, 1 }, { -2, -2, 0, -1 }, { 1, 1, 2, 6 } });
            Matrix matrix6 = new(new double[,] {
                { 1, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 2, 1, 1, 0 },
                { 0, 0, 0, 1, 4, 2, 1, 1 },
                { 0, 0, 0, 0, 5, 4, 2, 1 },
                { 0, 0, 0, 0, 7, 5, 4, 2 },
                { 0, 0, 0, 0, 6, 7, 5, 4 },
                { 0, 0, 0, 0, 8, 6, 7, 5 },
            });

            Assert.AreEqual(matrix1.Rows, matrix1.Transpose.Columns);
            Assert.AreEqual(matrix1.Columns, matrix1.Transpose.Rows);

            Assert.AreEqual(matrix2.Rows, matrix2.Transpose.Columns);
            Assert.AreEqual(matrix2.Columns, matrix2.Transpose.Rows);

            Assert.AreEqual(matrix3.Rows, matrix3.Transpose.Columns);
            Assert.AreEqual(matrix3.Columns, matrix3.Transpose.Rows);

            Assert.IsTrue((matrix1 * matrix1.Inverse * matrix1 - matrix1).Norm < 1e-28);
            Assert.IsTrue((matrix2 * matrix2.Inverse * matrix2 - matrix2).Norm < 1e-28);
            Assert.IsTrue((matrix3 * matrix3.Inverse * matrix3 - matrix3).Norm < 1e-28);

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }), matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } }), matrix2);
            Assert.AreEqual(new Matrix(new double[,] { { 1, 2 }, { 3, 4 } }), matrix3);

            Assert.IsTrue((matrix1.Inverse * matrix1 * matrix1.Inverse - matrix1.Inverse).Norm < 1e-28);
            Assert.IsTrue((matrix2.Inverse * matrix2 * matrix2.Inverse - matrix2.Inverse).Norm < 1e-28);
            Assert.IsTrue((matrix3.Inverse * matrix3 * matrix3.Inverse - matrix3.Inverse).Norm < 1e-28);

            Assert.IsTrue(((matrix1 * matrix1.Inverse).Transpose - matrix1 * matrix1.Inverse).Norm < 1e-28);
            Assert.IsTrue(((matrix2 * matrix2.Inverse).Transpose - matrix2 * matrix2.Inverse).Norm < 1e-28);
            Assert.IsTrue(((matrix3 * matrix3.Inverse).Transpose - matrix3 * matrix3.Inverse).Norm < 1e-28);

            Assert.IsTrue(((matrix1.Inverse * matrix1).Transpose - matrix1.Inverse * matrix1).Norm < 1e-28);
            Assert.IsTrue(((matrix2.Inverse * matrix2).Transpose - matrix2.Inverse * matrix2).Norm < 1e-28);
            Assert.IsTrue(((matrix3.Inverse * matrix3).Transpose - matrix3.Inverse * matrix3).Norm < 1e-28);

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
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new(new double[,] { { 0, 1 }, { 0, 0 } });
            Matrix matrix3 = new(new double[,] { { 1, 2, -3 }, { 2, -1, 3 }, { -3, 2, 1 } });
            Matrix matrix4 = new(new double[,] { { 2, 1, 1, 2 }, { 4, 2, 3, 1 }, { -2, -2, 0, -1 }, { 1, 1, 2, 6 } });
            Matrix matrix5 = new(new double[,] {
                { 1, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 2, 1, 1, 0 },
                { 0, 0, 0, 1, 4, 2, 1, 1 },
                { 0, 0, 0, 0, 5, 4, 2, 1 },
                { 0, 0, 0, 0, 7, 5, 4, 2 },
                { 0, 0, 0, 0, 6, 7, 5, 4 },
                { 0, 0, 0, 0, 8, 6, 7, 5 },
            });

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

            Matrix matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(new Vector(1, 2, 3), matrix.Horizontal(0));
            Assert.AreEqual(new Vector(4, 5, 6), matrix.Horizontal(1));
        }

        [TestMethod()]
        public void VerticalTest() {
            Vector vector = new(1, 2, 3);

            Assert.AreEqual(new Matrix(new double[,] { { 1 }, { 2 }, { 3 } }), vector.Vertical);

            Matrix matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(new Vector(1, 4), matrix.Vertical(0));
            Assert.AreEqual(new Vector(2, 5), matrix.Vertical(1));
            Assert.AreEqual(new Vector(3, 6), matrix.Vertical(2));
        }

        [TestMethod()]
        public void OperatorTest() {
            Matrix matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = new(new double[,] { { 7, 8, 9 }, { 1, 2, 3 } });
            Matrix matrix3 = new(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
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
            Matrix matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

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
            Matrix matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

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

            Assert.IsTrue(Matrix.IsEqualSize(matrix1, matrix1));
            Assert.IsFalse(Matrix.IsEqualSize(matrix1, matrix2));
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
            Matrix matrix1 = new(new double[,] { { 1, 0 }, { 0, 2 } });
            Matrix matrix2 = new(new double[,] { { 1, 1 }, { 0, 2 } });
            Matrix matrix3 = new(new double[,] { { 1, 0, 0 }, { 0, 2, 0 } });
            Matrix matrix4 = new(new double[,] { { 1, 0 }, { 0, 2 }, { 0, 0 } });

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
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 2, 3 } });
            Matrix matrix2 = new(new double[,] { { 1, 2 }, { 3, 3 } });

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
            Matrix matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = new(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Matrix matrix3 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix4 = new(new double[,] { { 0, 1 }, { 0, 0 } });

            Assert.IsTrue(Matrix.IsRegular(matrix1));
            Assert.IsTrue(Matrix.IsRegular(matrix2));
            Assert.IsTrue(Matrix.IsRegular(matrix3));
            Assert.IsFalse(Matrix.IsRegular(matrix4));
            Assert.IsFalse(Matrix.IsRegular(Matrix.Zero(2, 2)));
            Assert.IsTrue(Matrix.IsRegular(Matrix.Identity(2)));
        }

        [TestMethod()]
        public void DiagonalsTest() {
            Matrix matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

            ddouble[] diagonals = matrix.Diagonals;

            Assert.AreEqual(1, diagonals[0]);
            Assert.AreEqual(5, diagonals[1]);
            Assert.AreEqual(9, diagonals[2]);
        }

        [TestMethod()]
        public void LUDecomposeTest() {
            Matrix matrix = new(new double[,] { { 2, 3, 1, 2 }, { 4, 1, 3, -2 }, { 2, 2, -3, 1 }, { 1, -3, 2, 4 } });

            (Matrix lower, Matrix upper) = matrix.LUDecompose();

            Assert.AreEqual(new Matrix(new double[,] { { 2, 3, 1, 2 }, { 4, 1, 3, -2 }, { 2, 2, -3, 1 }, { 1, -3, 2, 4 } }), matrix);

            foreach (var diagonal in lower.Diagonals) {
                Assert.AreEqual(1, diagonal);
            }

            Assert.IsTrue((matrix - lower * upper).Norm < 1e-28);
        }

        [TestMethod()]
        public void QRDecomposeTest() {
            Matrix matrix = new(new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } });

            (Matrix q, Matrix r) = matrix.QRDecompose();

            Assert.AreEqual(new Matrix(new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } }), matrix);

            Assert.IsTrue((matrix - q * r).Norm < 1e-12);
            Assert.IsTrue((q * q.Transpose - Matrix.Identity(matrix.Size)).Norm < 1e-28);
        }

        [TestMethod()]
        public void CalculateEigenValuesTest() {
            Matrix matrix = new(new double[,] { { 1, 2 }, { 4, 5 } });
            ddouble[] eigen_values = matrix.CalculateEigenValues();

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2 }, { 4, 5 } }), matrix);

            Assert.IsTrue(ddouble.Abs(eigen_values[0] - (3 + 2 * ddouble.Sqrt(3))) < 1e-28);
            Assert.IsTrue(ddouble.Abs(eigen_values[1] - (3 - 2 * ddouble.Sqrt(3))) < 1e-28);
        }

        [TestMethod()]
        public void CalculateEigenVectorTest() {
            Matrix matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 8, 7, 9 } });

            (ddouble[] eigen_values, Vector[] eigen_vectors) = matrix.CalculateEigenValueVectors();

            Assert.AreEqual(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 8, 7, 9 } }), matrix);

            for (int i = 0; i < matrix.Size; i++) {
                ddouble eigen_value = eigen_values[i];
                Vector eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-15);
            }
        }

        [TestMethod()]
        public void DetTest() {
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new(new double[,] { { 1, 2 }, { 2, 4 } });

            Assert.AreEqual(-2, matrix1.Det);
            Assert.AreEqual(0, matrix2.Det);
        }

        [TestMethod()]
        public void TraceTest() {
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new(new double[,] { { 1, 2 }, { 2, 4 } });

            Assert.AreEqual(-1, matrix1.Trace);
            Assert.AreEqual(1, matrix2.Trace);
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
            Matrix matrix1 = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = new(new double[,] { { 1, 2, 3 } });
            Matrix matrix3 = new(new double[,] { { 1 }, { 2 }, { 3 } });
            Matrix matrix4 = Matrix.Invalid(2, 2);

            Assert.AreEqual("[ [ 1, 2, 3 ], [ 4, 5, 6 ] ]", matrix1.ToString());
            Assert.AreEqual("[ [ 1, 2, 3 ] ]", matrix2.ToString());
            Assert.AreEqual("[ [ 1 ], [ 2 ], [ 3 ] ]", matrix3.ToString());
            Assert.AreEqual("Invalid Matrix", matrix4.ToString());
        }
    }
}