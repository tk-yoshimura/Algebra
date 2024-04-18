using Algebra;
using DoubleDouble;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    [TestClass()]
    public partial class VectorTests {
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

            vector[^1] = 1;
            vector[^2] = 2;
            vector[^3] = 3;
            vector[^4] = 4;

            Assert.AreEqual(1, vector[^1]);
            Assert.AreEqual(2, vector[^2]);
            Assert.AreEqual(3, vector[^3]);
            Assert.AreEqual(4, vector[^4]);

            string str = string.Empty;
            foreach ((int index, ddouble val) in vector) {
                str += $"({index},{val}),";
            }

            Assert.AreEqual("(0,4),(1,3),(2,2),(3,1),", str);
        }

        [TestMethod()]
        public void NormTest() {
            Vector vector1 = new(-3, 4);
            Vector vector2 = new(0, 0);

            Assert.AreEqual(5, vector1.Norm);
            Assert.AreEqual(0, vector2.Norm);
            Assert.AreEqual(25, vector1.SquareNorm);
        }

        [TestMethod()]
        public void SumTest() {
            Vector vector = new(1, 2, 3, 4);

            Assert.AreEqual(10, vector.Sum);
        }

        [TestMethod()]
        public void NormalTest() {
            Vector vector = new(1, 2, -3);

            Assert.AreEqual(1 / ddouble.Sqrt(1 + 4 + 9), vector.Normal.X);
            Assert.AreEqual(2 / ddouble.Sqrt(1 + 4 + 9), vector.Normal.Y);
            Assert.AreEqual(-3 / ddouble.Sqrt(1 + 4 + 9), vector.Normal.Z);
        }

        [TestMethod()]
        public void DotTest() {
            Vector vector1 = new(1, 2);
            Vector vector2 = new(3, 4);
            Vector vector3 = new(2, -1);

            Assert.AreEqual(11, Vector.Dot(vector1, vector2));
            Assert.AreEqual(11, Vector.Dot(vector2, vector1));
            Assert.AreEqual(0, Vector.Dot(vector1, vector3));
            Assert.AreEqual(2, Vector.Dot(vector2, vector3));
        }

        [TestMethod()]
        public void CrossTest() {
            Vector vector1 = new(1, 2, 3);
            Vector vector2 = new(4, -5, -6);
            Vector vector3 = new(3, 1, 0);
            Vector vector4 = new(2, 5, 1);

            Assert.AreEqual(Vector.Zero(3), Vector.Cross(vector1, vector2) + Vector.Cross(vector2, vector1));
            Assert.AreEqual(new Vector(1, -3, 13), Vector.Cross(vector3, vector4));
        }

        [TestMethod()]
        public void PolynomialTest() {
            Vector x = new(1, 2, 3, 4);
            Vector coef = new(5, 7, 11, 13, 17);

            Assert.AreEqual(new Vector(53, 439, 1853, 5393), Vector.Polynomial(x, coef));
            Assert.AreEqual(13, Vector.Polynomial(-1, coef));

            Assert.AreEqual(new Vector(5, 5, 5, 5), Vector.Polynomial(x, new Vector(5)));
            Assert.AreEqual(5, Vector.Polynomial(-1, new Vector(5)));

            Assert.AreEqual(new Vector(0, 0, 0, 0), Vector.Polynomial(x, new Vector(Array.Empty<double>())));
            Assert.AreEqual(0, Vector.Polynomial(-1, new Vector(Array.Empty<double>())));
        }

        [TestMethod()]
        public void DistanceTest() {
            Vector vector1 = new(1, 2);
            Vector vector2 = new(2, 1);

            Assert.AreEqual(ddouble.Sqrt(2), Vector.Distance(vector1, vector2));
            Assert.AreEqual(2, Vector.SquareDistance(vector1, vector2));
        }

        [TestMethod()]
        public void ZeroTest() {
            Vector vector = Vector.Zero(3);

            Assert.AreEqual(0, vector.X);
            Assert.AreEqual(0, vector.Y);
            Assert.AreEqual(0, vector.Z);
        }

        [TestMethod()]
        public void FillTest() {
            Vector vector = Vector.Fill(3, value: 7);

            Assert.AreEqual(7, vector.X);
            Assert.AreEqual(7, vector.Y);
            Assert.AreEqual(7, vector.Z);
        }

        [TestMethod()]
        public void ArangeTest() {
            Vector vector = Vector.Arange(8);

            for (int i = 0; i < vector.Dim; i++) {
                Assert.AreEqual(i, vector[i]);
            }
        }

        [TestMethod()]
        public void InvalidTest() {
            Vector vector = Vector.Invalid(3);

            Assert.IsTrue(ddouble.IsNaN(vector.X));
            Assert.IsTrue(ddouble.IsNaN(vector.Y));
            Assert.IsTrue(ddouble.IsNaN(vector.Z));
        }

        [TestMethod()]
        public void IsZeroTest() {
            Vector vector = Vector.Zero(3);

            Assert.IsTrue(Vector.IsZero(vector));

            vector.X = 1;

            Assert.IsFalse(Vector.IsZero(vector));
        }

        [TestMethod()]
        public void IsValidTest() {
            Vector vector = Vector.Zero(3);

            Assert.IsTrue(Vector.IsValid(vector));

            vector.X = double.NaN;

            Assert.IsFalse(Vector.IsValid(vector));
        }

        [TestMethod()]
        public void TupleTest() {
            ddouble x, y, z, w, e0, e1, e2, e3, e4, e5, e6, e7;

            Vector vector2 = (2, 4);
            (x, y) = vector2;
            Assert.AreEqual((2, 4), (x, y));

            Vector vector3 = (2, 4, 6);
            (x, y, z) = vector3;
            Assert.AreEqual((2, 4, 6), (x, y, z));

            Vector vector4 = (2, 4, 6, 8);
            (x, y, z, w) = vector4;
            Assert.AreEqual((2, 4, 6, 8), (x, y, z, w));

            Vector vector5 = (2, 4, 6, 8, 1);
            (e0, e1, e2, e3, e4) = vector5;
            Assert.AreEqual((2, 4, 6, 8, 1), (e0, e1, e2, e3, e4));

            Vector vector6 = (2, 4, 6, 8, 1, 3);
            (e0, e1, e2, e3, e4, e5) = vector6;
            Assert.AreEqual((2, 4, 6, 8, 1, 3), (e0, e1, e2, e3, e4, e5));

            Vector vector7 = (2, 4, 6, 8, 1, 3, 5);
            (e0, e1, e2, e3, e4, e5, e6) = vector7;
            Assert.AreEqual((2, 4, 6, 8, 1, 3, 5), (e0, e1, e2, e3, e4, e5, e6));

            Vector vector8 = (2, 4, 6, 8, 1, 3, 5, 7);
            (e0, e1, e2, e3, e4, e5, e6, e7) = vector8;
            Assert.AreEqual((2, 4, 6, 8, 1, 3, 5, 7), (e0, e1, e2, e3, e4, e5, e6, e7));

            Assert.ThrowsException<InvalidOperationException>(() => {
                (x, y, z) = vector2;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (x, y, z, w) = vector3;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4) = vector4;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5) = vector5;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5, e6) = vector6;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5, e6, e7) = vector7;
            });
            Assert.ThrowsException<InvalidOperationException>(() => {
                (e0, e1, e2, e3, e4, e5, e6) = vector8;
            });
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

        [TestMethod()]
        public void ToStringTest() {
            Vector vector1 = new(1, 2, 3);
            Vector vector2 = new(Array.Empty<double>());
            Vector vector3 = new(1d);

            Assert.AreEqual("[ 1, 2, 3 ]", vector1.ToString());
            Assert.AreEqual("invalid", vector2.ToString());
            Assert.AreEqual("[ 1 ]", vector3.ToString());
            Assert.AreEqual("[ 1, 2, 3 ]", vector1.ToString());
            Assert.AreEqual("[ 1.0000e0, 2.0000e0, 3.0000e0 ]", vector1.ToString("e4"));
            Assert.AreEqual("[ 1.0000e0, 2.0000e0, 3.0000e0 ]", $"{vector1:e4}");
        }
    }
}