using DoubleDouble;
using System;

namespace Algebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector {
        public static implicit operator Vector((ddouble x, ddouble y) v) {
            return new Vector([v.x, v.y], cloning: false);
        }

        public void Deconstruct(out ddouble x, out ddouble y) {
            if (Dim != 2) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y) = (v[0], v[1]);
        }

        public static implicit operator Vector((ddouble x, ddouble y, ddouble z) v) {
            return new Vector([v.x, v.y, v.z], cloning: false);
        }

        public void Deconstruct(out ddouble x, out ddouble y, out ddouble z) {
            if (Dim != 3) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y, z) = (v[0], v[1], v[2]);
        }

        public static implicit operator Vector((ddouble x, ddouble y, ddouble z, ddouble w) v) {
            return new Vector([v.x, v.y, v.z, v.w], cloning: false);
        }

        public void Deconstruct(out ddouble x, out ddouble y, out ddouble z, out ddouble w) {
            if (Dim != 4) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (x, y, z, w) = (v[0], v[1], v[2], v[3]);
        }

        public static implicit operator Vector((ddouble e0, ddouble e1, ddouble e2, ddouble e3, ddouble e4) v) {
            return new Vector([v.e0, v.e1, v.e2, v.e3, v.e4], cloning: false);
        }

        public void Deconstruct(out ddouble e0, out ddouble e1, out ddouble e2, out ddouble e3, out ddouble e4) {
            if (Dim != 5) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4) = (v[0], v[1], v[2], v[3], v[4]);
        }

        public static implicit operator Vector((ddouble e0, ddouble e1, ddouble e2, ddouble e3, ddouble e4, ddouble e5) v) {
            return new Vector([v.e0, v.e1, v.e2, v.e3, v.e4, v.e5], cloning: false);
        }

        public void Deconstruct(out ddouble e0, out ddouble e1, out ddouble e2, out ddouble e3, out ddouble e4, out ddouble e5) {
            if (Dim != 6) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5) = (v[0], v[1], v[2], v[3], v[4], v[5]);
        }

        public static implicit operator Vector((ddouble e0, ddouble e1, ddouble e2, ddouble e3, ddouble e4, ddouble e5, ddouble e6) v) {
            return new Vector([v.e0, v.e1, v.e2, v.e3, v.e4, v.e5, v.e6], cloning: false);
        }

        public void Deconstruct(out ddouble e0, out ddouble e1, out ddouble e2, out ddouble e3, out ddouble e4, out ddouble e5, out ddouble e6) {
            if (Dim != 7) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5, e6) = (v[0], v[1], v[2], v[3], v[4], v[5], v[6]);
        }

        public static implicit operator Vector((ddouble e0, ddouble e1, ddouble e2, ddouble e3, ddouble e4, ddouble e5, ddouble e6, ddouble e7) v) {
            return new Vector([v.e0, v.e1, v.e2, v.e3, v.e4, v.e5, v.e6, v.e7], cloning: false);
        }

        public void Deconstruct(out ddouble e0, out ddouble e1, out ddouble e2, out ddouble e3, out ddouble e4, out ddouble e5, out ddouble e6, out ddouble e7) {
            if (Dim != 8) {
                throw new InvalidOperationException($"vector dim={Dim}");
            }

            (e0, e1, e2, e3, e4, e5, e6, e7) = (v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7]);
        }
    }
}
