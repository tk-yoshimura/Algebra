using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algebra.Tests {
    [TestClass()]
    public class VectorTests {
        [TestMethod()]
        public void VectorTest() {
            Vector vector = new Vector(1, 2, 3, 4);

            Assert.AreEqual(vector[0], 1);
            Assert.AreEqual(vector[1], 2);
            Assert.AreEqual(vector[2], 3);
            Assert.AreEqual(vector[3], 4);

            Assert.AreEqual(vector.X, 1);
            Assert.AreEqual(vector.Y, 2);
            Assert.AreEqual(vector.Z, 3);
            Assert.AreEqual(vector.W, 4);

            Assert.AreEqual(vector.Dim, 4);

            vector.X = 4;
            vector.Y = 3;
            vector.Z = 2;
            vector.W = 1;

            Assert.AreEqual(vector.X, 4);
            Assert.AreEqual(vector.Y, 3);
            Assert.AreEqual(vector.Z, 2);
            Assert.AreEqual(vector.W, 1);

            vector[0] = 1;
            vector[1] = 2;
            vector[2] = 3;
            vector[3] = 4;

            Assert.AreEqual(vector[0], 1);
            Assert.AreEqual(vector[1], 2);
            Assert.AreEqual(vector[2], 3);
            Assert.AreEqual(vector[3], 4);
        }

        [TestMethod()]
        public void NormTest() {
            Vector vector = new Vector(-3, 4);

            Assert.AreEqual(vector.Norm, 5);
            Assert.AreEqual(vector.SquareNorm, 25);
        }

        [TestMethod()]
        public void NormalTest() {
            Vector vector = new Vector(1, 2, -3);

            Assert.AreEqual(vector.Normal.X, 1 / Math.Sqrt(1 + 4 + 9));
            Assert.AreEqual(vector.Normal.Y, 2 / Math.Sqrt(1 + 4 + 9));
            Assert.AreEqual(vector.Normal.Z, -3 / Math.Sqrt(1 + 4 + 9));
        }

        [TestMethod()]
        public void OperatorTest() {
            Vector vector1 = new Vector(1, 2);
            Vector vector2 = new Vector(3, 4);

            Assert.AreEqual(+vector1, new Vector(1, 2));
            Assert.AreEqual(-vector1, new Vector(-1, -2));
            Assert.AreEqual(vector1 + vector2, new Vector(4, 6));
            Assert.AreEqual(vector2 + vector1, new Vector(4, 6));
            Assert.AreEqual(vector1 - vector2, new Vector(-2, -2));
            Assert.AreEqual(vector2 - vector1, new Vector(2, 2));
            Assert.AreEqual(vector1 * 2, new Vector(2, 4));
            Assert.AreEqual(2 * vector1, new Vector(2, 4));
            Assert.AreEqual(vector1 / 2, new Vector(0.5, 1));
        }

        [TestMethod()]
        public void DistanceTest() {
            Vector vector1 = new Vector(1, 2);
            Vector vector2 = new Vector(2, 1);

            Assert.AreEqual(Vector.Distance(vector1, vector2), Math.Sqrt(2));
            Assert.AreEqual(Vector.SquareDistance(vector1, vector2), 2);
        }

        [TestMethod()]
        public void InnerProductTest() {
            Vector vector1 = new Vector(1, 2);
            Vector vector2 = new Vector(3, 4);
            Vector vector3 = new Vector(2, -1);

            Assert.AreEqual(Vector.InnerProduct(vector1, vector2), 11);
            Assert.AreEqual(Vector.InnerProduct(vector2, vector1), 11);
            Assert.AreEqual(Vector.InnerProduct(vector1, vector3), 0);
            Assert.AreEqual(Vector.InnerProduct(vector2, vector3), 2);
        }

        [TestMethod()]
        public void ZeroTest() {
            Vector vector = Vector.Zero(3);

            Assert.AreEqual(vector.X, 0);
            Assert.AreEqual(vector.Y, 0);
            Assert.AreEqual(vector.Z, 0);
        }

        [TestMethod()]
        public void IsZeroTest() {
            Vector vector = Vector.Zero(3);

            Assert.AreEqual(Vector.IsZero(vector), true);

            vector.X = 1;

            Assert.AreEqual(Vector.IsZero(vector), false);
        }

        [TestMethod()]
        public void InvalidTest() {
            Vector vector = Vector.Invalid(3);

            Assert.AreEqual(double.IsNaN(vector.X), true);
            Assert.AreEqual(double.IsNaN(vector.Y), true);
            Assert.AreEqual(double.IsNaN(vector.Z), true);
        }

        [TestMethod()]
        public void IsValidTest() {
            Vector vector = Vector.Zero(3);

            Assert.AreEqual(Vector.IsValid(vector), true);

            vector.X = double.NaN;

            Assert.AreEqual(Vector.IsValid(vector), false);
        }

        [TestMethod()]
        public void OperatorEqualTest() {
            Vector vector = new Vector(1, 2);

            Assert.AreEqual(vector == new Vector(1, 2), true);
            Assert.AreEqual(vector != new Vector(2, 1), true);
            Assert.AreEqual(vector != new Vector(1), true);
            Assert.AreEqual(vector != new Vector(1, 2, 3), true);
            Assert.AreEqual(vector != null, true);
            Assert.AreEqual(vector != new Vector(1, double.NaN), true);
        }

        [TestMethod()]
        public void EqualsTest() {
            Vector vector = new Vector(1, 2);

            Assert.AreEqual(vector.Equals(new Vector(1, 2)), true);
            Assert.AreEqual(vector.Equals(new Vector(2, 1)), false);
            Assert.AreEqual(vector.Equals(new Vector(1)), false);
            Assert.AreEqual(vector.Equals(new Vector(1, 2, 3)), false);
            Assert.AreEqual(vector.Equals(null), false);
            Assert.AreEqual(vector.Equals(new Vector(1, double.NaN)), false);
        }

        [TestMethod()]
        public void ToStringTest() {
            Vector vector1 = new Vector(1, 2, 3);
            Vector vector2 = new Vector();
            Vector vector3 = new Vector(1);

            Assert.AreEqual(vector1.ToString(), "1,2,3");
            Assert.AreEqual(vector2.ToString(), string.Empty);
            Assert.AreEqual(vector3.ToString(), "1");
        }

        [TestMethod()]
        public void CopyTest() {
            Vector vector1 = new Vector(1, 2);
            Vector vector2 = vector1.Copy();

            vector2.X = 2;

            Assert.AreEqual(vector1.X, 1);
            Assert.AreEqual(vector1.Y, 2);
            Assert.AreEqual(vector2.X, 2);
            Assert.AreEqual(vector2.Y, 2);
        }
    }
}