using Algebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgebraTests {
    public partial class MatrixTests {
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
    }
}