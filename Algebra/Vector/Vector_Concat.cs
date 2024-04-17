using DoubleDouble;
using System;
using System.Collections.Generic;

namespace Algebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector {
        /// <summary>結合</summary>
        public static Vector Concat(params object[] blocks) {
            List<ddouble> v = [];

            foreach (object obj in blocks) {
                if (obj is Vector vector) {
                    v.AddRange(vector.v);
                }
                else if (obj is ddouble vdd) {
                    v.Add(vdd);
                }
                else if (obj is double vd) {
                    v.Add(vd);
                }
                else if (obj is int vi) {
                    v.Add(vi);
                }
                else if (obj is long vl) {
                    v.Add(vl);
                }
                else if (obj is float vf) {
                    v.Add(vf);
                }
                else if (obj is string vs) {
                    v.Add(vs);
                }
                else {
                    throw new ArgumentException($"unsupported type '{obj.GetType().Name}'", nameof(blocks));
                }
            }

            return new Vector(v);
        }

        /// <summary>結合</summary>
        public static Vector Concat(params Vector[] blocks) {
            List<ddouble> v = [];

            foreach (Vector vector in blocks) {
                v.AddRange(vector.v);
            }

            return new Vector(v);
        }
    }
}
