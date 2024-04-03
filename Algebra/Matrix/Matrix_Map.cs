using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix((Func<ddouble, ddouble> func, Matrix arg) sel) {
            return Func(sel.func, sel.arg);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix((Func<ddouble, ddouble, ddouble> func, (Matrix matrix1, Matrix matrix2) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix((Func<ddouble, ddouble, ddouble, ddouble> func, (Matrix matrix1, Matrix matrix2, Matrix matrix3) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2, sel.args.matrix3);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix((Func<ddouble, ddouble, ddouble, ddouble, ddouble> func, (Matrix matrix1, Matrix matrix2, Matrix matrix3, Matrix matrix4) args) sel) {
            return Func(sel.func, sel.args.matrix1, sel.args.matrix2, sel.args.matrix3, sel.args.matrix4);
        }

        /// <summary>写像キャスト</summary>
        public static implicit operator Matrix((Func<ddouble, ddouble, ddouble> func, Vector vector_row, Vector vector_column) sel) {
            return Map(sel.func, sel.vector_row, sel.vector_column);
        }

        /// <summary>写像</summary>
        public static Matrix Func(Func<ddouble, ddouble> f, Matrix matrix) {
            ddouble[,] x = matrix.e, v = new ddouble[matrix.Rows, matrix.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j]);
                }
            }

            return new Matrix(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Matrix Func(Func<ddouble, ddouble, ddouble> f, Matrix matrix1, Matrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            ddouble[,] x = matrix1.e, y = matrix2.e, v = new ddouble[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j]);
                }
            }

            return new Matrix(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Matrix Func(Func<ddouble, ddouble, ddouble, ddouble> f, Matrix matrix1, Matrix matrix2, Matrix matrix3) {
            if (matrix1.Shape != matrix2.Shape || matrix1.Shape != matrix3.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)},{nameof(matrix3)}");
            }

            ddouble[,] x = matrix1.e, y = matrix2.e, z = matrix3.e, v = new ddouble[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j], z[i, j]);
                }
            }

            return new Matrix(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Matrix Func(Func<ddouble, ddouble, ddouble, ddouble, ddouble> f, Matrix matrix1, Matrix matrix2, Matrix matrix3, Matrix matrix4) {
            if (matrix1.Shape != matrix2.Shape || matrix1.Shape != matrix3.Shape || matrix1.Shape != matrix4.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)},{nameof(matrix3)},{nameof(matrix4)}");
            }

            ddouble[,] x = matrix1.e, y = matrix2.e, z = matrix3.e, w = matrix4.e, v = new ddouble[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(x[i, j], y[i, j], z[i, j], w[i, j]);
                }
            }

            return new Matrix(v, cloning: false);
        }

        /// <summary>写像</summary>
        public static Matrix Map(Func<ddouble, ddouble, ddouble> f, Vector vector_row, Vector vector_column) {
            ddouble[] row = vector_row.v, col = vector_column.v;
            ddouble[,] v = new ddouble[row.Length, col.Length];

            for (int i = 0; i < v.GetLength(0); i++) {
                for (int j = 0; j < v.GetLength(1); j++) {
                    v[i, j] = f(row[i], col[j]);
                }
            }

            return new Matrix(v, cloning: false);
        }
    }
}
