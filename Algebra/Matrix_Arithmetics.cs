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
            ddouble[,] ret = new ddouble[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = -e[i, j];
                }
            }

            return new Matrix(ret, cloning: false);
        }

        /// <summary>行列加算</summary>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            ddouble[,] ret = new ddouble[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] + e2[i, j];
                }
            }

            return new Matrix(ret, cloning: false);
        }

        /// <summary>行列減算</summary>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            ddouble[,] ret = new ddouble[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] - e2[i, j];
                }
            }

            return new Matrix(ret, cloning: false);
        }

        /// <summary>要素ごとに積算</summary>
        public static Matrix ElementwiseMul(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            ddouble[,] ret = new ddouble[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] * e2[i, j];
                }
            }

            return new Matrix(ret, cloning: false);
        }

        /// <summary>要素ごとに除算</summary>
        public static Matrix ElementwiseDiv(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Shape != matrix2.Shape) {
                throw new ArgumentException("mismatch size", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            ddouble[,] ret = new ddouble[matrix1.Rows, matrix1.Columns], e1 = matrix1.e, e2 = matrix2.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = e1[i, j] / e2[i, j];
                }
            }

            return new Matrix(ret, cloning: false);
        }

        /// <summary>行列乗算</summary>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2) {
            if (matrix1.Columns != matrix2.Rows) {
                throw new ArgumentException($"mismatch {nameof(matrix1.Columns)} {nameof(matrix2.Rows)}", $"{nameof(matrix1)},{nameof(matrix2)}");
            }

            ddouble[,] ret = new ddouble[matrix1.Rows, matrix2.Columns], e1 = matrix1.e, e2 = matrix2.T.e;

            for (int i = 0, c = matrix1.Columns; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ddouble s = 0d;

                    for (int k = 0; k < c; k++) {
                        s += e1[i, k] * e2[j, k];
                    }

                    ret[i, j] = s;
                }
            }

            return new Matrix(ret, cloning: false);
        }

        /// <summary>行列・列ベクトル乗算</summary>
        public static Vector operator *(Matrix matrix, Vector vector) {
            if (matrix.Columns != vector.Dim) {
                throw new ArgumentException($"mismatch {nameof(matrix.Columns)} {nameof(vector.Dim)}", $"{nameof(matrix)},{nameof(vector)}");
            }

            ddouble[] ret = new ddouble[matrix.Rows], v = vector.v;
            ddouble[,] e = matrix.e;

            for (int i = 0; i < matrix.Rows; i++) {
                ddouble s = 0d;

                for (int j = 0; j < matrix.Columns; j++) {
                    s += e[i, j] * v[j];
                }

                ret[i] = s;
            }

            return new Vector(ret, cloning: false);
        }

        /// <summary>行列・行ベクトル乗算</summary>
        public static Vector operator *(Vector vector, Matrix matrix) {
            if (vector.Dim != matrix.Rows) {
                throw new ArgumentException($"mismatch {nameof(vector.Dim)} {nameof(matrix.Rows)}", $"{nameof(vector)},{nameof(matrix)}");
            }

            ddouble[] ret = new ddouble[matrix.Columns], v = vector.v;
            ddouble[,] e = matrix.T.e;

            for (int j = 0; j < matrix.Columns; j++) {
                ddouble s = 0d;

                for (int i = 0; i < matrix.Rows; i++) {
                    s += v[i] * e[j, i];
                }

                ret[j] = s;
            }

            return new Vector(ret, cloning: false);
        }

        /// <summary>行列スカラー加算</summary>
        public static Matrix operator +(ddouble r, Matrix matrix) {
            ddouble[,] ret = new ddouble[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r + e[i, j];
                }
            }

            return new Matrix(ret, cloning: false);
        }

        /// <summary>行列スカラー加算</summary>
        public static Matrix operator +(Matrix matrix, ddouble r) {
            return r + matrix;
        }

        /// <summary>行列スカラー減算</summary>
        public static Matrix operator -(ddouble r, Matrix matrix) {
            ddouble[,] ret = new ddouble[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r - e[i, j];
                }
            }

            return new Matrix(ret, cloning: false);
        }

        /// <summary>行列スカラー減算</summary>
        public static Matrix operator -(Matrix matrix, ddouble r) {
            return (-r) + matrix;
        }

        /// <summary>行列スカラー倍</summary>
        public static Matrix operator *(ddouble r, Matrix matrix) {
            ddouble[,] ret = new ddouble[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r * e[i, j];
                }
            }

            return new Matrix(ret, cloning: false);
        }

        /// <summary>行列スカラー倍</summary>
        public static Matrix operator *(Matrix matrix, ddouble r) {
            return r * matrix;
        }

        /// <summary>行列スカラー除算</summary>
        public static Matrix operator /(ddouble r, Matrix matrix) {
            ddouble[,] ret = new ddouble[matrix.Rows, matrix.Columns], e = matrix.e;

            for (int i = 0; i < ret.GetLength(0); i++) {
                for (int j = 0; j < ret.GetLength(1); j++) {
                    ret[i, j] = r / e[i, j];
                }
            }

            return new Matrix(ret, cloning: false);
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

            for (int i = 0; i < matrix1.Rows; i++) {
                for (int j = 0; j < matrix2.Columns; j++) {
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
