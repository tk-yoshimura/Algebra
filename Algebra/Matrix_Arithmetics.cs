using DoubleDouble;
using System;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>単項プラス</summary>
        public static Matrix operator +(Matrix matrix) {
            return matrix.Copy();
        }

        /// <summary>単項マイナス</summary>
        public static Matrix operator -(Matrix matrix) {
            Matrix ret = matrix.Copy();

            for (int i = 0; i < ret.Rows; i++) {
                for (int j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = -ret.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列加算</summary>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Matrix ret = new(matrix1.Rows, matrix1.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = matrix1.e[i, j] + matrix2.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列減算</summary>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Matrix ret = new(matrix1.Rows, matrix1.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = matrix1.e[i, j] - matrix2.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>要素ごとに積算</summary>
        public static Matrix ElementwiseMul(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Matrix ret = new(matrix1.Rows, matrix1.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = matrix1.e[i, j] * matrix2.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>要素ごとに除算</summary>
        public static Matrix ElementwiseDiv(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Matrix ret = new(matrix1.Rows, matrix1.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = matrix1.e[i, j] / matrix2.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列乗算</summary>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Columns != matrix2.Rows) {
                throw new ArgumentException($"mismatch {nameof(matrix1.Columns)} {nameof(matrix2.Rows)}", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            Matrix ret = new(matrix1.Rows, matrix2.Columns);
            int c = matrix1.Columns;

            for (int i = 0, j, k; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    for (k = 0; k < c; k++) {
                        ret.e[i, j] += matrix1.e[i, k] * matrix2.e[k, j];
                    }
                }
            }

            return ret;
        }

        /// <summary>行列・列ベクトル乗算</summary>
        public static Vector operator *(Matrix matrix, Vector vector) {
            if (matrix.Columns != vector.Dim) {
                throw new ArgumentException($"mismatch {nameof(matrix.Columns)} {nameof(vector.Dim)}", $"{nameof(matrix)},{nameof(vector)}");
            }

            Vector ret = Vector.Zero(matrix.Rows);

            for (int i = 0, j; i < matrix.Rows; i++) {
                for (j = 0; j < matrix.Columns; j++) {
                    ret.v[i] += matrix.e[i, j] * vector.v[j];
                }
            }

            return ret;
        }

        /// <summary>行列・行ベクトル乗算</summary>
        public static Vector operator *(Vector vector, Matrix matrix) {
            if (vector.Dim != matrix.Rows) {
                throw new ArgumentException($"mismatch {nameof(vector.Dim)} {nameof(matrix.Rows)}", $"{nameof(vector)},{nameof(matrix)}");
            }

            Vector ret = Vector.Zero(matrix.Columns);

            for (int j = 0, i; j < matrix.Columns; j++) {
                for (i = 0; i < matrix.Rows; i++) {
                    ret.v[j] += vector.v[i] * matrix.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列スカラー加算</summary>
        public static Matrix operator +(ddouble r, Matrix matrix) {
            Matrix ret = new(matrix.Rows, matrix.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = r + matrix.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列スカラー加算</summary>
        public static Matrix operator +(Matrix matrix, ddouble r) {
            return r + matrix;
        }

        /// <summary>行列スカラー減算</summary>
        public static Matrix operator -(ddouble r, Matrix matrix) {
            Matrix ret = new(matrix.Rows, matrix.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = r - matrix.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列スカラー減算</summary>
        public static Matrix operator -(Matrix matrix, ddouble r) {
            return (-r) + matrix;
        }

        /// <summary>行列スカラー倍</summary>
        public static Matrix operator *(ddouble r, Matrix matrix) {
            Matrix ret = new(matrix.Rows, matrix.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = r * matrix.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列スカラー倍</summary>
        public static Matrix operator *(Matrix matrix, ddouble r) {
            return r * matrix;
        }

        /// <summary>行列スカラー除算</summary>
        public static Matrix operator /(ddouble r, Matrix matrix) {
            Matrix ret = new(matrix.Rows, matrix.Columns);

            for (int i = 0, j; i < ret.Rows; i++) {
                for (j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = r / matrix.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>行列スカラー除算</summary>
        public static Matrix operator /(Matrix matrix, ddouble r) {
            return (1 / r) * matrix;
        }

        /// <summary>行列が等しいか</summary>
        public static bool operator ==(Matrix matrix1, Matrix matrix2) {
            if (ReferenceEquals(matrix1, matrix2)) {
                return true;
            }
            if (matrix1 is null || matrix2 is null) {
                return false;
            }

            if (matrix1.Shape != matrix2.Shape) {
                return false;
            }

            for (int i = 0, j; i < matrix1.Rows; i++) {
                for (j = 0; j < matrix2.Columns; j++) {
                    if (matrix1.e[i, j] != matrix2.e[i, j]) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>行列が異なるか判定</summary>
        public static bool operator !=(Matrix matrix1, Matrix matrix2) {
            return !(matrix1 == matrix2);
        }
    }
}
