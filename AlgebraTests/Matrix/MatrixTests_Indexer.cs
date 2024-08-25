using Algebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class MatrixTests {
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
        public void ArrayIndexerTest() {
            Matrix m = new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } };

            Assert.AreEqual(new double[,] { { 14, 12, 13 }, { 4, 2, 3 }, { 9, 7, 8 } }, m[[2, 0, 1], [3, 1, 2]]);

            m[[1, 0, 2], [3, 1, 2]] = new double[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };

            Assert.AreEqual(0, m[1, 3]);
            Assert.AreEqual(1, m[1, 1]);
            Assert.AreEqual(2, m[1, 2]);

            Assert.AreEqual(3, m[0, 3]);
            Assert.AreEqual(4, m[0, 1]);
            Assert.AreEqual(5, m[0, 2]);

            Assert.AreEqual(6, m[2, 3]);
            Assert.AreEqual(7, m[2, 1]);
            Assert.AreEqual(8, m[2, 2]);
        }

        [TestMethod()]
        public void RowArrayIndexerTest() {
            Matrix m = new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } };

            Assert.AreEqual(new double[,] { { 14, 12, 13 }, { 19, 17, 18 } }, m[2.., [3, 1, 2]]);

            m[1..3, [3, 1, 2]] = new double[,] { { 0, 1, 2 }, { 3, 4, 5 } };

            Assert.AreEqual(0, m[1, 3]);
            Assert.AreEqual(1, m[1, 1]);
            Assert.AreEqual(2, m[1, 2]);

            Assert.AreEqual(3, m[2, 3]);
            Assert.AreEqual(4, m[2, 1]);
            Assert.AreEqual(5, m[2, 2]);
        }

        [TestMethod()]
        public void ColumnArrayIndexerTest() {
            Matrix m = new double[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 } };

            Assert.AreEqual(new double[,] { { 12, 13, 14 }, { 2, 3, 4 }, { 7, 8, 9 } }, m[[2, 0, 1], 1..4]);

            m[[1, 0, 2], 1..4] = new double[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };

            Assert.AreEqual(0, m[1, 1]);
            Assert.AreEqual(1, m[1, 2]);
            Assert.AreEqual(2, m[1, 3]);

            Assert.AreEqual(3, m[0, 1]);
            Assert.AreEqual(4, m[0, 2]);
            Assert.AreEqual(5, m[0, 3]);

            Assert.AreEqual(6, m[2, 1]);
            Assert.AreEqual(7, m[2, 2]);
            Assert.AreEqual(8, m[2, 3]);
        }
    }
}