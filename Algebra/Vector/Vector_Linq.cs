using DoubleDouble;
using System;

namespace Algebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector {
        /// <summary>Any句</summary>
        public static bool Any(Vector vector, Func<ddouble, bool> cond) {
            for (int i = 0; i < vector.Dim; i++) {
                if (cond(vector.v[i])) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>All句</summary>
        public static bool All(Vector vector, Func<ddouble, bool> cond) {
            for (int i = 0; i < vector.Dim; i++) {
                if (!cond(vector.v[i])) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>Count句</summary>
        public static long Count(Vector vector, Func<ddouble, bool> cond) {
            long cnt = 0;

            for (int i = 0; i < vector.Dim; i++) {
                if (cond(vector.v[i])) {
                    cnt++;
                }
            }

            return cnt;
        }
    }
}
