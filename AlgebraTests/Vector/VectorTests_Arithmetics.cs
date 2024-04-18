using Algebra;
using DoubleDouble;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgebraTests {
    public partial class VectorTests {
        [TestMethod()]
        public void OperatorTest() {
            Vector vector1 = new(1, 2);
            Vector vector2 = new(3, 4);

            Assert.AreEqual(new Vector(1, 2), +vector1);
            Assert.AreEqual(new Vector(-1, -2), -vector1);
            Assert.AreEqual(new Vector(4, 6), vector1 + vector2);
            Assert.AreEqual(new Vector(4, 6), vector2 + vector1);
            Assert.AreEqual(new Vector(4, 5), vector1 + 3);
            Assert.AreEqual(new Vector(4, 5), 3 + vector1);
            Assert.AreEqual(new Vector(-2, -2), vector1 - vector2);
            Assert.AreEqual(new Vector(2, 2), vector2 - vector1);
            Assert.AreEqual(new Vector(-2, -1), vector1 - 3);
            Assert.AreEqual(new Vector(2, 1), 3 - vector1);
            Assert.AreEqual(new Vector(2, 4), vector1 * 2);
            Assert.AreEqual(new Vector(2, 4), 2 * vector1);
            Assert.AreEqual(new Vector(0.5, 1), vector1 / 2);
            Assert.AreEqual(new Vector(2, 1), 2 / vector1);
            Assert.AreEqual(new Vector(3, 8), vector1 * vector2);
            Assert.AreEqual(new Vector((ddouble)1 / 3, 0.5), vector1 / vector2);
            Assert.AreEqual(new Vector(3, 2), vector2 / vector1);
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
    }
}