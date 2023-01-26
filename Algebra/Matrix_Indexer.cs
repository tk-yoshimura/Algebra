using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>インデクサ</summary>
        /// <param name="row_index">行</param>
        /// <param name="column_index">列</param>
        public ddouble this[int row_index, int column_index] {
            get => e[row_index, column_index];
            set => e[row_index, column_index] = value;
        }

        /// <summary>インデクサ</summary>
        /// <param name="row_index">行</param>
        /// <param name="column_index">列</param>
        public ddouble this[Index row_index, Index column_index] {
            get => e[row_index.GetOffset(Rows), column_index.GetOffset(Columns)];
            set => e[row_index.GetOffset(Rows), column_index.GetOffset(Columns)] = value;
        }

        /// <summary>領域インデクサ</summary>
        /// <param name="row_range">行</param>
        /// <param name="column_range">列</param>
        public Matrix this[Range row_range, Range column_range] {
            get {
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                ddouble[,] m = new ddouble[rn, cn];
                for (int i = 0; i < rn; i++) {
                    for (int j = 0; j < cn; j++) {
                        m[i, j] = e[i + ri, j + ci];
                    }
                }

                return new(m);
            }

            set {
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                if (value.Rows != rn || value.Columns != cn) {
                    throw new ArgumentOutOfRangeException($"{nameof(row_range)},{nameof(column_range)}");
                }

                for (int i = 0; i < rn; i++) {
                    for (int j = 0; j < cn; j++) {
                        e[i + ri, j + ci] = value.e[i, j];
                    }
                }
            }
        }

        /// <summary>領域インデクサ</summary>
        /// <param name="row_range">行</param>
        /// <param name="column_index">列</param>
        public Vector this[Range row_range, Index column_index] {
            get {
                int c = column_index.GetOffset(Columns);
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);

                ddouble[] m = new ddouble[rn];
                for (int i = 0; i < rn; i++) {
                    m[i] = e[i + ri, c];
                }

                return new(m);
            }

            set {
                int c = column_index.GetOffset(Columns);
                (int ri, int rn) = row_range.GetOffsetAndLength(Rows);

                if (value.Dim != rn) {
                    throw new ArgumentOutOfRangeException($"{nameof(row_range)}");
                }

                for (int i = 0; i < rn; i++) {
                    e[i + ri, c] = value.v[i];
                }
            }
        }

        /// <summary>領域インデクサ</summary>
        /// <param name="row_index">行</param>
        /// <param name="column_range">列</param>
        public Vector this[Index row_index, Range column_range] {
            get {
                int r = row_index.GetOffset(Rows);
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                ddouble[] m = new ddouble[cn];
                for (int j = 0; j < cn; j++) {
                    m[j] = e[r, j + ci];
                }

                return new(m);
            }

            set {
                int r = row_index.GetOffset(Rows);
                (int ci, int cn) = column_range.GetOffsetAndLength(Columns);

                if (value.Dim != cn) {
                    throw new ArgumentOutOfRangeException($"{nameof(column_range)}");
                }

                for (int j = 0; j < cn; j++) {
                    e[r, j + ci] = value.v[j];
                }
            }
        }
    }
}
