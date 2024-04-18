using Algebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class VectorTests {
        [TestMethod()]
        public void FuncTest() {
            Vector vector1 = new(1, 2, 4, 8);
            Vector vector2 = new(2, 3, 5, 9);
            Vector vector3 = new(3, 4, 6, 10);
            Vector vector4 = new(4, 5, 7, 11);
            Vector vector5 = new(5, 6, 8, 12, 20);

            Assert.AreEqual(new Vector(2, 4, 8, 16), Vector.Func(v => 2 * v, vector1));
            Assert.AreEqual(new Vector(5, 8, 14, 26), Vector.Func((v1, v2) => v1 + 2 * v2, vector1, vector2));
            Assert.AreEqual(new Vector(17, 24, 38, 66), Vector.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector1, vector2, vector3));
            Assert.AreEqual(new Vector(49, 64, 94, 154), Vector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector3, vector4));
            Assert.AreEqual(new Vector(49, 64, 94, 154), Vector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector3, vector4));

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Func((v1, v2) => v1 + 2 * v2, vector1, vector5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Func((v1, v2) => v1 + 2 * v2, vector5, vector1);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector1, vector2, vector5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector1, vector5, vector2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Func((v1, v2, v3) => v1 + 2 * v2 + 4 * v3, vector5, vector1, vector2);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector3, vector5);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector2, vector5, vector3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector1, vector5, vector2, vector3);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                Vector.Func((v1, v2, v3, v4) => v1 + 2 * v2 + 4 * v3 + 8 * v4, vector5, vector1, vector2, vector3);
            });
        }
    }
}