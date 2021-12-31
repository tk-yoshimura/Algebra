using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algebra.Tests {
    [TestClass()]
    public class MatrixTests {
        [TestMethod()]
        public void MatrixTest() {
            Matrix matrix1 = new(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new(3, 2);

            Assert.AreEqual(1, matrix1[0, 0]);
            Assert.AreEqual(2, matrix1[0, 1]);
            Assert.AreEqual(3, matrix1[1, 0]);
            Assert.AreEqual(4, matrix1[1, 1]);

            Assert.AreEqual(2, matrix1.Columns);
            Assert.AreEqual(2, matrix1.Rows);
            Assert.AreEqual(2, matrix1.Size);

            Assert.AreEqual(0, matrix2[0, 0]);
            Assert.AreEqual(0, matrix2[0, 1]);
            Assert.AreEqual(0, matrix2[1, 0]);
            Assert.AreEqual(0, matrix2[1, 1]);
            Assert.AreEqual(0, matrix2[2, 0]);
            Assert.AreEqual(0, matrix2[2, 1]);

            Assert.AreEqual(3, matrix2.Rows);
            Assert.AreEqual(2, matrix2.Columns);
        }

        [TestMethod()]
        public void NormTest() {
            Matrix matrix = new(new double[,] { { 1, 2 }, { 3, 4 } });

            Assert.AreEqual(Math.Sqrt(30), matrix.Norm);
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

            Assert.IsTrue((matrix1 * matrix1.Inverse * matrix1 - matrix1).Norm < 1e-12);
            Assert.IsTrue((matrix2 * matrix2.Inverse * matrix2 - matrix2).Norm < 1e-12);
            Assert.IsTrue((matrix3 * matrix3.Inverse * matrix3 - matrix3).Norm < 1e-12);

            Assert.IsTrue((matrix1.Inverse * matrix1 * matrix1.Inverse - matrix1.Inverse).Norm < 1e-12);
            Assert.IsTrue((matrix2.Inverse * matrix2 * matrix2.Inverse - matrix2.Inverse).Norm < 1e-12);
            Assert.IsTrue((matrix3.Inverse * matrix3 * matrix3.Inverse - matrix3.Inverse).Norm < 1e-12);

            Assert.IsTrue(((matrix1 * matrix1.Inverse).Transpose - matrix1 * matrix1.Inverse).Norm < 1e-12);
            Assert.IsTrue(((matrix2 * matrix2.Inverse).Transpose - matrix2 * matrix2.Inverse).Norm < 1e-12);
            Assert.IsTrue(((matrix3 * matrix3.Inverse).Transpose - matrix3 * matrix3.Inverse).Norm < 1e-12);

            Assert.IsTrue(((matrix1.Inverse * matrix1).Transpose - matrix1.Inverse * matrix1).Norm < 1e-12);
            Assert.IsTrue(((matrix2.Inverse * matrix2).Transpose - matrix2.Inverse * matrix2).Norm < 1e-12);
            Assert.IsTrue(((matrix3.Inverse * matrix3).Transpose - matrix3.Inverse * matrix3).Norm < 1e-12);

            Assert.IsFalse(Matrix.IsValid(matrix4.Inverse));
            Assert.IsFalse(Matrix.IsValid(Matrix.Zero(2, 2).Inverse));
            Assert.IsTrue(Matrix.IsIdentity(Matrix.Identity(2).Inverse));

            Assert.IsTrue((matrix5.Inverse.Inverse - matrix5).Norm < 1e-12);
            Assert.IsTrue((matrix6.Inverse.Inverse - matrix6).Norm < 1e-12);
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
            Assert.AreEqual(new Matrix(new double[,] { { -6, -6, -6 }, { 3, 3, 3 } }), matrix1 - matrix2);
            Assert.AreEqual(new Matrix(new double[,] { { 6, 6, 6 }, { -3, -3, -3 } }), matrix2 - matrix1);
            Assert.AreEqual(new Matrix(new double[,] { { 22, 28 }, { 49, 64 } }), matrix1 * matrix3);
            Assert.AreEqual(new Matrix(new double[,] { { 9, 12, 15 }, { 19, 26, 33 }, { 29, 40, 51 } }), matrix3 * matrix1);

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

            Assert.IsTrue(double.IsNaN(matrix[0, 0]));
            Assert.IsTrue(double.IsNaN(matrix[1, 0]));
        }

        [TestMethod()]
        public void IsEqualSizeTest() {
            Matrix matrix1 = new(2, 2);
            Matrix matrix2 = new(2, 3);

            Assert.IsTrue(Matrix.IsEqualSize(matrix1, matrix1));
            Assert.IsFalse(Matrix.IsEqualSize(matrix1, matrix2));
        }

        [TestMethod()]
        public void IsSquareTest() {
            Matrix matrix1 = new(2, 2);
            Matrix matrix2 = new(2, 3);

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

            double[] diagonals = matrix.Diagonals;

            Assert.AreEqual(1, diagonals[0]);
            Assert.AreEqual(5, diagonals[1]);
            Assert.AreEqual(9, diagonals[2]);
        }

        [TestMethod()]
        public void LUDecompositionTest() {
            Matrix matrix = new(new double[,] { { 2, 3, 1, 2 }, { 4, 1, 3, -2 }, { 2, 2, -3, 1 }, { 1, -3, 2, 4 } });

            (Matrix lower, Matrix upper) = matrix.LUDecomposition();

            foreach (var diagonal in lower.Diagonals) {
                Assert.AreEqual(1, diagonal);
            }

            Assert.IsTrue((matrix - lower * upper).Norm < 1e-12);
        }

        [TestMethod()]
        public void QRDecompositionTest() {
            Matrix matrix = new(new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } });

            (Matrix q, Matrix r) = matrix.QRDecomposition();

            Assert.IsTrue((matrix - q * r).Norm < 1e-12);
            Assert.IsTrue((q * q.Transpose - Matrix.Identity(matrix.Size)).Norm < 1e-12);
        }

        [TestMethod()]
        public void CalculateEigenValuesTest() {
            Matrix matrix = new(new double[,] { { 1, 2 }, { 4, 5 } });
            double[] eigen_values = matrix.CalculateEigenValues();

            Assert.IsTrue(Math.Abs(eigen_values[0] - (3 + 2 * Math.Sqrt(3))) < 1e-12);
            Assert.IsTrue(Math.Abs(eigen_values[1] - (3 - 2 * Math.Sqrt(3))) < 1e-12);
        }

        [TestMethod()]
        public void CalculateEigenVectorTest() {
            Matrix matrix = new(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 8, 7, 9 } });

            (double[] eigen_values, Vector[] eigen_vectors) = matrix.CalculateEigenValueVectors();

            for (int i = 0; i < matrix.Size; i++) {
                double eigen_value = eigen_values[i];
                Vector eigen_vector = eigen_vectors[i];

                Assert.IsTrue((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-6);
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

            Assert.AreEqual("{ { 1, 2, 3 }, { 4, 5, 6 } }", matrix1.ToString());
            Assert.AreEqual("{ { 1, 2, 3 } }", matrix2.ToString());
            Assert.AreEqual("{ { 1 }, { 2 }, { 3 } }", matrix3.ToString());
            Assert.AreEqual("Invalid Matrix", matrix4.ToString());
        }
    }
}