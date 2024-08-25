using DoubleDouble;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector {
        /// <summary>インデクサ</summary>
        public ddouble this[int index] {
            get => v[index];
            set => v[index] = value;
        }

        /// <summary>インデクサ</summary>
        public ddouble this[Index index] {
            get => v[index.GetOffset(Dim)];
            set => v[index.GetOffset(Dim)] = value;
        }

        /// <summary>領域インデクサ</summary>
        public Vector this[Range range] {
            get {
                (int index, int counts) = range.GetOffsetAndLength(Dim);

                ddouble[] ret = new ddouble[counts];
                for (int i = 0; i < counts; i++) {
                    ret[i] = v[i + index];
                }

                return new(ret);
            }

            set {
                (int index, int counts) = range.GetOffsetAndLength(Dim);

                if (value.Dim != counts) {
                    throw new ArgumentOutOfRangeException(nameof(range));
                }

                for (int i = 0; i < counts; i++) {
                    v[i + index] = value.v[i];
                }
            }
        }

        /// <summary>配列インデクサ</summary>
        public Vector this[int[] indexes] {
            get {
                ddouble[] ret = new ddouble[indexes.Length];
                for (int i = 0; i < indexes.Length; i++) {
                    ret[i] = v[indexes[i]];
                }

                return new(ret);
            }

            set {
                if (value.Dim != indexes.Length) {
                    throw new ArgumentException("invalid size", nameof(indexes));
                }

                for (int i = 0; i < indexes.Length; i++) {
                    v[indexes[i]] = value.v[i];
                }
            }
        }

        /// <summary>配列インデクサ</summary>
        public Vector this[IEnumerable<int> indexes] {
            get => this[indexes.ToArray()];
            set => this[indexes.ToArray()] = value;
        }
    }
}
