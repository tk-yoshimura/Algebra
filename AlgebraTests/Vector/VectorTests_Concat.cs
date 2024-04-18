using Algebra;
using DoubleDouble;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class VectorTests {
        [TestMethod()]
        public void ConcatTest() {
            Vector vector1 = Vector.Fill(1, value: -1);
            Vector vector2 = Vector.Fill(2, value: -2);
            Vector vector4 = Vector.Fill(4, value: -3);

            Assert.AreEqual(new Vector(-1, -2, -2, -3, -3, -3, -3), Vector.Concat(vector1, vector2, vector4));
            Assert.AreEqual(new Vector(-2, -2, -3, -3, -3, -3, -1), Vector.Concat(vector2, vector4, vector1));
            Assert.AreEqual(new Vector(-1, -2, -2, 1, -3, -3, -3, -3), Vector.Concat(vector1, vector2, 1, vector4));
            Assert.AreEqual(new Vector(-1, -2, -2, 2, -3, -3, -3, -3), Vector.Concat(vector1, vector2, 2L, vector4));
            Assert.AreEqual(new Vector(-1, -2, -2, 3, -3, -3, -3, -3), Vector.Concat(vector1, vector2, (ddouble)3, vector4));
            Assert.AreEqual(new Vector(-1, -2, -2, 4, -3, -3, -3, -3), Vector.Concat(vector1, vector2, 4d, vector4));
            Assert.AreEqual(new Vector(-1, -2, -2, 5, -3, -3, -3, -3), Vector.Concat(vector1, vector2, 5f, vector4));
            Assert.AreEqual(new Vector(-1, -2, -2, "6.2", -3, -3, -3, -3), Vector.Concat(vector1, vector2, "6.2", vector4));

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Concat(vector1, vector2, 'b', vector4);
            });
        }
    }
}