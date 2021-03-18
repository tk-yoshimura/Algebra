using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algebra.Tests {
    [TestClass()]
    public class MatrixTests {
        [TestMethod()]
        public void MatrixTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new Matrix(3, 2);

            Assert.AreEqual(matrix1[0, 0], 1);
            Assert.AreEqual(matrix1[0, 1], 2);
            Assert.AreEqual(matrix1[1, 0], 3);
            Assert.AreEqual(matrix1[1, 1], 4);

            Assert.AreEqual(matrix1.Columns, 2);
            Assert.AreEqual(matrix1.Rows, 2);
            Assert.AreEqual(matrix1.Size, 2);

            Assert.AreEqual(matrix2[0, 0], 0);
            Assert.AreEqual(matrix2[0, 1], 0);
            Assert.AreEqual(matrix2[1, 0], 0);
            Assert.AreEqual(matrix2[1, 1], 0);
            Assert.AreEqual(matrix2[2, 0], 0);
            Assert.AreEqual(matrix2[2, 1], 0);

            Assert.AreEqual(matrix2.Rows, 3);
            Assert.AreEqual(matrix2.Columns, 2);
        }

        [TestMethod()]
        public void NormTest() {
            Matrix matrix = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });

            Assert.AreEqual(matrix.Norm, Math.Sqrt(30));
        }

        [TestMethod()]
        public void TransposeTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = matrix1.Transpose;
            Matrix matrix3 = matrix2.Transpose;

            Assert.AreEqual(matrix2, new Matrix(new double[,] { { 1, 4 }, { 2, 5 }, { 3, 6 } }));
            Assert.AreEqual(matrix1, matrix3);
        }

        [TestMethod()]
        public void InverseTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Matrix matrix3 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix4 = new Matrix(new double[,] { { 0, 1 }, { 0, 0 } });
            Matrix matrix5 = new Matrix(new double[,] { { 2, 1, 1, 2 }, { 4, 2, 3, 1 }, { -2, -2, 0, -1 }, { 1, 1, 2, 6 } });

            Assert.AreEqual(matrix1.Rows, matrix1.Transpose.Columns);
            Assert.AreEqual(matrix1.Columns, matrix1.Transpose.Rows);

            Assert.AreEqual(matrix2.Rows, matrix2.Transpose.Columns);
            Assert.AreEqual(matrix2.Columns, matrix2.Transpose.Rows);

            Assert.AreEqual(matrix3.Rows, matrix3.Transpose.Columns);
            Assert.AreEqual(matrix3.Columns, matrix3.Transpose.Rows);

            Assert.AreEqual((matrix1 * matrix1.Inverse * matrix1 - matrix1).Norm < 1e-12, true);
            Assert.AreEqual((matrix2 * matrix2.Inverse * matrix2 - matrix2).Norm < 1e-12, true);
            Assert.AreEqual((matrix3 * matrix3.Inverse * matrix3 - matrix3).Norm < 1e-12, true);

            Assert.AreEqual((matrix1.Inverse * matrix1 * matrix1.Inverse - matrix1.Inverse).Norm < 1e-12, true);
            Assert.AreEqual((matrix2.Inverse * matrix2 * matrix2.Inverse - matrix2.Inverse).Norm < 1e-12, true);
            Assert.AreEqual((matrix3.Inverse * matrix3 * matrix3.Inverse - matrix3.Inverse).Norm < 1e-12, true);

            Assert.AreEqual(((matrix1 * matrix1.Inverse).Transpose - matrix1 * matrix1.Inverse).Norm < 1e-12, true);
            Assert.AreEqual(((matrix2 * matrix2.Inverse).Transpose - matrix2 * matrix2.Inverse).Norm < 1e-12, true);
            Assert.AreEqual(((matrix3 * matrix3.Inverse).Transpose - matrix3 * matrix3.Inverse).Norm < 1e-12, true);

            Assert.AreEqual(((matrix1.Inverse * matrix1).Transpose - matrix1.Inverse * matrix1).Norm < 1e-12, true);
            Assert.AreEqual(((matrix2.Inverse * matrix2).Transpose - matrix2.Inverse * matrix2).Norm < 1e-12, true);
            Assert.AreEqual(((matrix3.Inverse * matrix3).Transpose - matrix3.Inverse * matrix3).Norm < 1e-12, true);

            Assert.AreEqual(Matrix.IsValid(matrix4.Inverse), false);
            Assert.AreEqual(Matrix.IsValid(Matrix.Zero(2, 2).Inverse), false);
            Assert.AreEqual(Matrix.IsIdentity(Matrix.Identity(2).Inverse), true);

            Assert.AreEqual((matrix5.Inverse.Inverse - matrix5).Norm < 1e-12, true);
        }

        [TestMethod()]
        public void HorizontalTest() {
            Vector vector = new Vector(1, 2, 3);

            Assert.AreEqual(vector.Horizontal, new Matrix(new double[,] { { 1, 2, 3 } }));

            Matrix matrix = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(matrix.Horizontal(0), new Vector(1, 2, 3));
            Assert.AreEqual(matrix.Horizontal(1), new Vector(4, 5, 6));
        }

        [TestMethod()]
        public void VerticalTest() {
            Vector vector = new Vector(1, 2, 3);

            Assert.AreEqual(vector.Vertical, new Matrix(new double[,] { { 1 }, { 2 }, { 3 } }));

            Matrix matrix = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(matrix.Vertical(0), new Vector(1, 4));
            Assert.AreEqual(matrix.Vertical(1), new Vector(2, 5));
            Assert.AreEqual(matrix.Vertical(2), new Vector(3, 6));
        }

        [TestMethod()]
        public void OperatorTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = new Matrix(new double[,] { { 7, 8, 9 }, { 1, 2, 3 } });
            Matrix matrix3 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Vector vector1 = new Vector(5, 4, 3);
            Vector vector2 = new Vector(5, 4);

            Assert.AreEqual(+matrix1, new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }));
            Assert.AreEqual(-matrix1, new Matrix(new double[,] { { -1, -2, -3 }, { -4, -5, -6 } }));
            Assert.AreEqual(matrix1 + matrix2, new Matrix(new double[,] { { 8, 10, 12 }, { 5, 7, 9 } }));
            Assert.AreEqual(matrix2 + matrix1, new Matrix(new double[,] { { 8, 10, 12 }, { 5, 7, 9 } }));
            Assert.AreEqual(matrix1 - matrix2, new Matrix(new double[,] { { -6, -6, -6 }, { 3, 3, 3 } }));
            Assert.AreEqual(matrix2 - matrix1, new Matrix(new double[,] { { 6, 6, 6 }, { -3, -3, -3 } }));
            Assert.AreEqual(matrix1 * matrix3, new Matrix(new double[,] { { 22, 28 }, { 49, 64 } }));
            Assert.AreEqual(matrix3 * matrix1, new Matrix(new double[,] { { 9, 12, 15 }, { 19, 26, 33 }, { 29, 40, 51 } }));

            Assert.AreEqual(matrix1 * vector1, new Vector(22, 58));
            Assert.AreEqual(vector1 * matrix3, new Vector(32, 44));
            Assert.AreEqual(vector2 * matrix1, new Vector(21, 30, 39));
            Assert.AreEqual(matrix3 * vector2, new Vector(13, 31, 49));

            Assert.AreEqual(matrix1 * 2, new Matrix(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }));
            Assert.AreEqual(2 * matrix1, new Matrix(new double[,] { { 2, 4, 6 }, { 8, 10, 12 } }));
            Assert.AreEqual(matrix1 / 2, new Matrix(new double[,] { { 0.5, 1, 1.5 }, { 2, 2.5, 3 } }));
        }

        [TestMethod()]
        public void OperatorEqualTest() {
            Matrix matrix = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(matrix == new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } }), true);
            Assert.AreEqual(matrix != new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 7 } }), true);
            Assert.AreEqual(matrix != new Matrix(new double[,] { { 1, 2 }, { 4, 5 } }), true);
            Assert.AreEqual(matrix != new Matrix(new double[,] { { 1, 2, 3, 4 }, { 4, 5, 6, 7 } }), true);
            Assert.AreEqual(matrix != new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }), true);
            Assert.AreEqual(matrix != null, true);
            Assert.AreEqual(matrix != new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, double.NaN } }), true);
        }

        [TestMethod()]
        public void EqualsTest() {
            Matrix matrix = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

            Assert.AreEqual(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } })), true);
            Assert.AreEqual(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 7 } })), false);
            Assert.AreEqual(matrix.Equals(new Matrix(new double[,] { { 1, 2 }, { 4, 5 } })), false);
            Assert.AreEqual(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3, 4 }, { 4, 5, 6, 7 } })), false);
            Assert.AreEqual(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } })), false);
            Assert.AreEqual(matrix.Equals(null), false);
            Assert.AreEqual(matrix.Equals(new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, double.NaN } })), false);
        }

        [TestMethod()]
        public void ZeroTest() {
            Matrix matrix = Matrix.Zero(2, 2);

            Assert.AreEqual(matrix[0, 0], 0);
            Assert.AreEqual(matrix[1, 0], 0);
            Assert.AreEqual(matrix[0, 1], 0);
            Assert.AreEqual(matrix[1, 1], 0);
        }

        [TestMethod()]
        public void IdentityTest() {
            Matrix matrix = Matrix.Identity(2);

            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[1, 0], 0);
            Assert.AreEqual(matrix[0, 1], 0);
            Assert.AreEqual(matrix[1, 1], 1);
        }

        [TestMethod()]
        public void InvalidTest() {
            Matrix matrix = Matrix.Invalid(2, 1);

            Assert.AreEqual(double.IsNaN(matrix[0, 0]), true);
            Assert.AreEqual(double.IsNaN(matrix[1, 0]), true);
        }

        [TestMethod()]
        public void IsEqualSizeTest() {
            Matrix matrix1 = new Matrix(2, 2);
            Matrix matrix2 = new Matrix(2, 3);

            Assert.AreEqual(Matrix.IsEqualSize(matrix1, matrix1), true);
            Assert.AreEqual(Matrix.IsEqualSize(matrix1, matrix2), false);
        }

        [TestMethod()]
        public void IsSquareTest() {
            Matrix matrix1 = new Matrix(2, 2);
            Matrix matrix2 = new Matrix(2, 3);

            Assert.AreEqual(Matrix.IsSquare(matrix1), true);
            Assert.AreEqual(Matrix.IsSquare(matrix2), false);
        }

        [TestMethod()]
        public void IsDiagonalTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 0 }, { 0, 2 } });
            Matrix matrix2 = new Matrix(new double[,] { { 1, 1 }, { 0, 2 } });
            Matrix matrix3 = new Matrix(new double[,] { { 1, 0, 0 }, { 0, 2, 0 } });
            Matrix matrix4 = new Matrix(new double[,] { { 1, 0 }, { 0, 2 }, { 0, 0 } });

            Assert.AreEqual(Matrix.IsDiagonal(matrix1), true);
            Assert.AreEqual(Matrix.IsDiagonal(matrix2), false);
            Assert.AreEqual(Matrix.IsDiagonal(matrix3), false);
            Assert.AreEqual(Matrix.IsDiagonal(matrix4), false);
        }

        [TestMethod()]
        public void IsZeroTest() {
            Matrix matrix = Matrix.Zero(2, 3);

            Assert.AreEqual(Matrix.IsZero(matrix), true);

            matrix[0, 0] = 1;

            Assert.AreEqual(Matrix.IsZero(matrix), false);
        }

        [TestMethod()]
        public void IsIdentityTest() {
            Matrix matrix = Matrix.Identity(2);

            Assert.AreEqual(Matrix.IsIdentity(matrix), true);

            matrix[0, 1] = 1;

            Assert.AreEqual(Matrix.IsIdentity(matrix), false);
        }

        [TestMethod()]
        public void IsSymmetricTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 2 }, { 2, 3 } });
            Matrix matrix2 = new Matrix(new double[,] { { 1, 2 }, { 3, 3 } });

            Assert.AreEqual(Matrix.IsSymmetric(matrix1), true);
            Assert.AreEqual(Matrix.IsSymmetric(matrix2), false);
        }

        [TestMethod()]
        public void IsValidTest() {
            Assert.AreEqual(Matrix.IsValid(Matrix.Zero(3, 2)), true);
            Assert.AreEqual(Matrix.IsValid(Matrix.Invalid(2, 3)), false);
        }

        [TestMethod()]
        public void IsRegularTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Matrix matrix3 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix4 = new Matrix(new double[,] { { 0, 1 }, { 0, 0 } });

            Assert.AreEqual(Matrix.IsRegular(matrix1), true);
            Assert.AreEqual(Matrix.IsRegular(matrix2), true);
            Assert.AreEqual(Matrix.IsRegular(matrix2), true);
            Assert.AreEqual(Matrix.IsRegular(matrix4), false);
            Assert.AreEqual(Matrix.IsRegular(Matrix.Zero(2, 2)), false);
            Assert.AreEqual(Matrix.IsRegular(Matrix.Identity(2)), true);
        }

        [TestMethod()]
        public void DiagonalsTest() {
            Matrix matrix = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

            double[] diagonals = matrix.Diagonals;

            Assert.AreEqual(diagonals[0], 1);
            Assert.AreEqual(diagonals[1], 5);
            Assert.AreEqual(diagonals[2], 9);
        }

        [TestMethod()]
        public void LUDecompositionTest() {
            Matrix matrix = new Matrix(new double[,] { { 2, 3, 1, 2 }, { 4, 1, 3, -2 }, { 2, 2, -3, 1 }, { 1, -3, 2, 4 } });

            matrix.LUDecomposition(out Matrix lower, out Matrix upper);

            foreach (var diagonal in lower.Diagonals) {
                Assert.AreEqual(diagonal, 1);
            }

            Assert.AreEqual((matrix - lower * upper).Norm < 1e-12, true);
        }

        [TestMethod()]
        public void QRDecompositionTest() {
            Matrix matrix = new Matrix(new double[,] { { 12, -51, 4 }, { 6, 167, -68 }, { -4, 24, -41 } });

            matrix.QRDecomposition(out Matrix q, out Matrix r);

            Assert.AreEqual((matrix - q * r).Norm < 1e-12, true);
            Assert.AreEqual((q * q.Transpose - Matrix.Identity(matrix.Size)).Norm < 1e-12, true);
        }

        [TestMethod()]
        public void CalculateEigenValuesTest() {
            Matrix matrix = new Matrix(new double[,] { { 1, 2 }, { 4, 5 } });
            double[] eigen_values = matrix.CalculateEigenValues();

            Assert.AreEqual(Math.Abs(eigen_values[0] - (3 + 2 * Math.Sqrt(3))) < 1e-12, true);
            Assert.AreEqual(Math.Abs(eigen_values[1] - (3 - 2 * Math.Sqrt(3))) < 1e-12, true);
        }

        [TestMethod()]
        public void CalculateEigenVectorTest() {
            Matrix matrix = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 8, 7, 9 } });

            matrix.CalculateEigenValueVectors(out double[] eigen_values, out Vector[] eigen_vectors);

            for (int i = 0; i < matrix.Size; i++) {
                double eigen_value = eigen_values[i];
                Vector eigen_vector = eigen_vectors[i];

                Assert.AreEqual((matrix * eigen_vector - eigen_value * eigen_vector).Norm < 1e-6, true);
            }
        }

        [TestMethod()]
        public void DetTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new Matrix(new double[,] { { 1, 2 }, { 2, 4 } });

            Assert.AreEqual(matrix1.Det, -2);
            Assert.AreEqual(matrix2.Det, 0);
        }

        [TestMethod()]
        public void TraceTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 } });
            Matrix matrix2 = new Matrix(new double[,] { { 1, 2 }, { 2, 4 } });

            Assert.AreEqual(matrix1.Trace, -1);
            Assert.AreEqual(matrix2.Trace, 1);
        }

        [TestMethod()]
        public void CopyTest() {
            Matrix matrix1 = Matrix.Zero(2, 2);
            Matrix matrix2 = matrix1.Copy();

            matrix2[1, 1] = 1;

            Assert.AreEqual(matrix1[1, 1], 0);
            Assert.AreEqual(matrix2[1, 1], 1);
        }

        [TestMethod()]
        public void ToStringTest() {
            Matrix matrix1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix matrix2 = new Matrix(new double[,] { { 1, 2, 3 } });
            Matrix matrix3 = new Matrix(new double[,] { { 1 }, { 2 }, { 3 } });
            Matrix matrix4 = Matrix.Invalid(2, 2);

            Assert.AreEqual(matrix1.ToString(), "{ { 1, 2, 3 }, { 4, 5, 6 } }");
            Assert.AreEqual(matrix2.ToString(), "{ { 1, 2, 3 } }");
            Assert.AreEqual(matrix3.ToString(), "{ { 1 }, { 2 }, { 3 } }");
            Assert.AreEqual(matrix4.ToString(), "Invalid Matrix");
        }
    }
}