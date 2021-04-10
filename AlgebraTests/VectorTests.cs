using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algebra.Tests {
    [TestClass()]
    public class VectorTests {
        [TestMethod()]
        public void VectorTest() {
            Vector vector = new(1, 2, 3, 4);

            Assert.AreEqual(1, vector[0]);
            Assert.AreEqual(2, vector[1]);
            Assert.AreEqual(3, vector[2]);
            Assert.AreEqual(4, vector[3]);

            Assert.AreEqual(1, vector.X);
            Assert.AreEqual(2, vector.Y);
            Assert.AreEqual(3, vector.Z);
            Assert.AreEqual(4, vector.W);

            Assert.AreEqual(4, vector.Dim);

            vector.X = 4;
            vector.Y = 3;
            vector.Z = 2;
            vector.W = 1;

            Assert.AreEqual(4, vector.X);
            Assert.AreEqual(3, vector.Y);
            Assert.AreEqual(2, vector.Z);
            Assert.AreEqual(1, vector.W);

            vector[0] = 1;
            vector[1] = 2;
            vector[2] = 3;
            vector[3] = 4;

            Assert.AreEqual(1, vector[0]);
            Assert.AreEqual(2, vector[1]);
            Assert.AreEqual(3, vector[2]);
            Assert.AreEqual(4, vector[3]);
        }

        [TestMethod()]
        public void NormTest() {
            Vector vector = new(-3, 4);

            Assert.AreEqual(5, vector.Norm);
            Assert.AreEqual(25, vector.SquareNorm);
        }

        [TestMethod()]
        public void NormalTest() {
            Vector vector = new(1, 2, -3);

            Assert.AreEqual(1 / Math.Sqrt(1 + 4 + 9), vector.Normal.X);
            Assert.AreEqual(2 / Math.Sqrt(1 + 4 + 9), vector.Normal.Y);
            Assert.AreEqual(-3 / Math.Sqrt(1 + 4 + 9), vector.Normal.Z);
        }

        [TestMethod()]
        public void OperatorTest() {
            Vector vector1 = new(1, 2);
            Vector vector2 = new(3, 4);

            Assert.AreEqual(new Vector(1, 2), +vector1);
            Assert.AreEqual(new Vector(-1, -2), -vector1);
            Assert.AreEqual(new Vector(4, 6), vector1 + vector2);
            Assert.AreEqual(new Vector(4, 6), vector2 + vector1);
            Assert.AreEqual(new Vector(-2, -2), vector1 - vector2);
            Assert.AreEqual(new Vector(2, 2), vector2 - vector1);
            Assert.AreEqual(new Vector(2, 4), vector1 * 2);
            Assert.AreEqual(new Vector(2, 4), 2 * vector1);
            Assert.AreEqual(new Vector(0.5, 1), vector1 / 2);
        }

        [TestMethod()]
        public void DistanceTest() {
            Vector vector1 = new(1, 2);
            Vector vector2 = new(2, 1);

            Assert.AreEqual(Math.Sqrt(2), Vector.Distance(vector1, vector2));
            Assert.AreEqual(2, Vector.SquareDistance(vector1, vector2));
        }

        [TestMethod()]
        public void InnerProductTest() {
            Vector vector1 = new(1, 2);
            Vector vector2 = new(3, 4);
            Vector vector3 = new(2, -1);

            Assert.AreEqual(11, Vector.InnerProduct(vector1, vector2));
            Assert.AreEqual(11, Vector.InnerProduct(vector2, vector1));
            Assert.AreEqual(0, Vector.InnerProduct(vector1, vector3));
            Assert.AreEqual(2, Vector.InnerProduct(vector2, vector3));
        }

        [TestMethod()]
        public void ZeroTest() {
            Vector vector = Vector.Zero(3);

            Assert.AreEqual(0, vector.X);
            Assert.AreEqual(0, vector.Y);
            Assert.AreEqual(0, vector.Z);
        }

        [TestMethod()]
        public void IsZeroTest() {
            Vector vector = Vector.Zero(3);

            Assert.IsTrue(Vector.IsZero(vector));

            vector.X = 1;

            Assert.IsFalse(Vector.IsZero(vector));
        }

        [TestMethod()]
        public void InvalidTest() {
            Vector vector = Vector.Invalid(3);

            Assert.IsTrue(double.IsNaN(vector.X));
            Assert.IsTrue(double.IsNaN(vector.Y));
            Assert.IsTrue(double.IsNaN(vector.Z));
        }

        [TestMethod()]
        public void IsValidTest() {
            Vector vector = Vector.Zero(3);

            Assert.IsTrue(Vector.IsValid(vector));

            vector.X = double.NaN;

            Assert.IsFalse(Vector.IsValid(vector));
        }

        [TestMethod()]
        public void OperatorEqualTest() {
            Vector vector = new(1, 2);

            Assert.IsTrue(vector == new Vector(1, 2));
            Assert.IsTrue(vector != new Vector(2, 1));
            Assert.IsTrue(vector != new Vector(1));
            Assert.IsTrue(vector != new Vector(1, 2, 3));
            Assert.IsFalse(vector == null);
            Assert.IsTrue(vector != null);
            Assert.IsTrue(vector != new Vector(1, double.NaN));
        }

        [TestMethod()]
        public void EqualsTest() {
            Vector vector = new(1, 2);

            Assert.IsTrue(vector.Equals(new Vector(1, 2)));
            Assert.IsFalse(vector.Equals(new Vector(2, 1)));
            Assert.IsFalse(vector.Equals(new Vector(1)));
            Assert.IsFalse(vector.Equals(new Vector(1, 2, 3)));
            Assert.IsFalse(vector.Equals(null));
            Assert.IsFalse(vector.Equals(new Vector(1, double.NaN)));
        }

        [TestMethod()]
        public void ToStringTest() {
            Vector vector1 = new(1, 2, 3);
            Vector vector2 = new();
            Vector vector3 = new(1);

            Assert.AreEqual("1,2,3", vector1.ToString());
            Assert.AreEqual(string.Empty, vector2.ToString());
            Assert.AreEqual("1", vector3.ToString());
        }

        [TestMethod()]
        public void CopyTest() {
            Vector vector1 = new(1, 2);
            Vector vector2 = vector1.Copy();

            vector2.X = 2;

            Assert.AreEqual(1, vector1.X);
            Assert.AreEqual(2, vector1.Y);
            Assert.AreEqual(2, vector2.X);
            Assert.AreEqual(2, vector2.Y);
        }
    }
}