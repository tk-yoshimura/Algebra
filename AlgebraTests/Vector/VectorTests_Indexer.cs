using Algebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgebraTests {
    public partial class VectorTests {
        [TestMethod()]
        public void RangeIndexerGetterTest() {
            Vector vector = new(1, 2, 3, 4, 5);

            Assert.AreEqual(new Vector(1, 2, 3, 4, 5), vector[..]);

            Assert.AreEqual(new Vector(2, 3, 4, 5), vector[1..]);
            Assert.AreEqual(new Vector(3, 4, 5), vector[2..]);
            Assert.AreEqual(new Vector(1, 2, 3, 4), vector[..^1]);
            Assert.AreEqual(new Vector(1, 2, 3, 4), vector[..4]);
            Assert.AreEqual(new Vector(1, 2, 3), vector[..^2]);
            Assert.AreEqual(new Vector(1, 2, 3), vector[..3]);

            Assert.AreEqual(new Vector(2, 3, 4), vector[1..4]);
            Assert.AreEqual(new Vector(2, 3, 4), vector[1..^1]);
        }

        [TestMethod()]
        public void RangeIndexerSetterTest() {
            Vector vector_src = new(1, 2, 3, 4, 5);
            Vector vector_dst;

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[..] = vector_src;
            Assert.AreEqual(new Vector(1, 2, 3, 4, 5), vector_dst);

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[1..] = vector_src[1..];
            Assert.AreEqual(new Vector(0, 2, 3, 4, 5), vector_dst);

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[2..] = vector_src[2..];
            Assert.AreEqual(new Vector(0, 0, 3, 4, 5), vector_dst);

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[..^1] = vector_src[..^1];
            Assert.AreEqual(new Vector(1, 2, 3, 4, 0), vector_dst);

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[..4] = vector_src[..4];
            Assert.AreEqual(new Vector(1, 2, 3, 4, 0), vector_dst);

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[..^2] = vector_src[..^2];
            Assert.AreEqual(new Vector(1, 2, 3, 0, 0), vector_dst);

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[..3] = vector_src[..3];
            Assert.AreEqual(new Vector(1, 2, 3, 0, 0), vector_dst);

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[1..4] = vector_src[1..4];
            Assert.AreEqual(new Vector(0, 2, 3, 4, 0), vector_dst);

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[1..^1] = vector_src[1..^1];
            Assert.AreEqual(new Vector(0, 2, 3, 4, 0), vector_dst);

            vector_dst = Vector.Zero(vector_src.Dim);
            vector_dst[0..^2] = vector_src[1..^1];
            Assert.AreEqual(new Vector(2, 3, 4, 0, 0), vector_dst);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                vector_dst = Vector.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..^2];
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                vector_dst = Vector.Zero(vector_src.Dim);
                vector_dst[0..^2] = vector_src[1..];
            });
        }

        [TestMethod()]
        public void ArrayIndexerTest() {
            Vector v = new(1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual(new Vector(5, 2, 3, 7), v[[4, 1, 2, 6]]);

            v[[2, 1, 3]] = new(4, 0, 8);

            Assert.AreEqual(4, v[2]);
            Assert.AreEqual(0, v[1]);
            Assert.AreEqual(8, v[3]);
        }
    }
}