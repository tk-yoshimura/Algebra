using Algebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class MatrixTests {
        [TestMethod()]
        public void CholeskyTest() {
            foreach (Matrix matrix in MatrixTestCases.PositiveMatrixs) {
                Console.WriteLine($"test: {matrix}");

                Matrix l = Matrix.Cholesky(matrix);
                Matrix v = l * l.T;

                Assert.IsTrue((matrix - v).Norm < 1e-28);
            }
        }

        [TestMethod()]
        public void InversePositiveSymmetric() {
            foreach (Matrix m in MatrixTestCases.PositiveMatrixs) {
                Console.WriteLine($"test: {m}");

                Matrix r = Matrix.InversePositiveSymmetric(m);

                Assert.IsTrue((m * r - Matrix.Identity(m.Size)).Norm < 1e-25);
            }
        }

        [TestMethod()]
        public void SlovePositiveSymmetric() {
            foreach (Matrix m in MatrixTestCases.PositiveMatrixs) {
                Console.WriteLine($"test: {m}");

                Vector v = Vector.Zero(m.Size);
                for (int i = 0; i < v.Dim; i++) {
                    v[i] = i + 2;
                }

                Matrix r = Matrix.InversePositiveSymmetric(m);

                Vector u = Matrix.SolvePositiveSymmetric(m, v);
                Vector t = r * v;

                Assert.IsTrue((t - u).Norm < 1e-25);
            }
        }
    }
}