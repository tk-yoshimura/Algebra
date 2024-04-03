using DoubleDouble;
using System;

namespace Algebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector {
        /// <summary>写像キャスト</summary>
        public static implicit operator Vector((Func<ddouble, ddouble> func, Vector arg) sel) {
            return Func(sel.func, sel.arg);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Vector((Func<ddouble, ddouble, ddouble> func, (Vector vector1, Vector vector2) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Vector((Func<ddouble, ddouble, ddouble, ddouble> func, (Vector vector1, Vector vector2, Vector vector3) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2, sel.args.vector3);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Vector((Func<ddouble, ddouble, ddouble, ddouble, ddouble> func, (Vector vector1, Vector vector2, Vector vector3, Vector vector4) args) sel) {
            return Func(sel.func, sel.args.vector1, sel.args.vector2, sel.args.vector3, sel.args.vector4);
        }

        /// <summary>写像</summary>
        public static Vector Func(Func<ddouble, ddouble> f, Vector vector) {
            ddouble[] x = vector.v, v = new ddouble[vector.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i]);
            }

            return new Vector(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Vector Func(Func<ddouble, ddouble, ddouble> f, Vector vector1, Vector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)}");
            }

            ddouble[] x = vector1.v, y = vector2.v, v = new ddouble[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i]);
            }

            return new Vector(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Vector Func(Func<ddouble, ddouble, ddouble, ddouble> f, Vector vector1, Vector vector2, Vector vector3) {
            if (vector1.Dim != vector2.Dim || vector1.Dim != vector3.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)},{nameof(vector3)}");
            }

            ddouble[] x = vector1.v, y = vector2.v, z = vector3.v, v = new ddouble[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i], z[i]);
            }

            return new Vector(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Vector Func(Func<ddouble, ddouble, ddouble, ddouble, ddouble> f, Vector vector1, Vector vector2, Vector vector3, Vector vector4) {
            if (vector1.Dim != vector2.Dim || vector1.Dim != vector3.Dim || vector1.Dim != vector4.Dim) {
                throw new ArgumentException("mismatch size", $"{nameof(vector1)},{nameof(vector2)},{nameof(vector3)},{nameof(vector4)}");
            }

            ddouble[] x = vector1.v, y = vector2.v, z = vector3.v, w = vector4.v, v = new ddouble[vector1.Dim];

            for (int i = 0; i < v.Length; i++) {
                v[i] = f(x[i], y[i], z[i], w[i]);
            }

            return new Vector(v, cloning: false);
        }
    }
}
