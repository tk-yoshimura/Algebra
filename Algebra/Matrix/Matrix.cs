using DoubleDouble;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Algebra {
    /// <summary>行列クラス</summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Matrix :
        ICloneable, IFormattable,
        IEnumerable<(int row_index, int column_index, ddouble val)>,
        IAdditionOperators<Matrix, Matrix, Matrix>,
        ISubtractionOperators<Matrix, Matrix, Matrix>,
        IMultiplyOperators<Matrix, Matrix, Matrix>,
        IUnaryPlusOperators<Matrix, Matrix>,
        IUnaryNegationOperators<Matrix, Matrix> {

        internal readonly ddouble[,] e;

        /// <summary>コンストラクタ</summary>
        /// <param name="m">行列要素配列</param>
        protected Matrix(ddouble[,] m, bool cloning) {
            this.e = cloning ? (ddouble[,])m.Clone() : m;
        }

        /// <summary>コンストラクタ </summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        protected Matrix(int rows, int columns) {
            if (rows <= 0 || columns <= 0) {
                throw new ArgumentOutOfRangeException($"{nameof(rows)},{nameof(columns)}");
            }

            this.e = new ddouble[rows, columns];
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="m">行列要素配列</param>
        public Matrix(double[,] m) : this(m.GetLength(0), m.GetLength(1)) {
            ArgumentNullException.ThrowIfNull(m, nameof(m));

            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    e[i, j] = m[i, j];
                }
            }
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="m">行列要素配列</param>
        public Matrix(ddouble[,] m) : this(m, cloning: true) { }

        /// <summary>行数</summary>
        public int Rows => e.GetLength(0);

        /// <summary>列数</summary>
        public int Columns => e.GetLength(1);

        /// <summary>形状</summary>
        public (int rows, int columns) Shape => (e.GetLength(0), e.GetLength(1));

        /// <summary>サイズ(正方行列のときのみ有効)</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Size {
            get {
                if (!IsSquare(this)) {
                    throw new ArithmeticException("not square matrix");
                }

                return Rows;
            }
        }

        /// <summary>キャスト</summary>
        public static implicit operator ddouble[,](Matrix matrix) {
            return (ddouble[,])matrix.e.Clone();
        }

        /// <summary>キャスト</summary>
        public static explicit operator double[,](Matrix matrix) {
            ddouble[,] e = matrix.e;
            double[,] ret = new double[e.GetLength(0), e.GetLength(1)];

            for (int i = 0; i < e.GetLength(0); i++) {
                for (int j = 0; j < e.GetLength(1); j++) {
                    ret[i, j] = (double)e[i, j];
                }
            }

            return ret;
        }

        /// <summary>キャスト</summary>
        public static implicit operator Matrix(ddouble[,] arr) {
            return new Matrix(arr);
        }

        /// <summary>キャスト</summary>
        public static implicit operator Matrix(double[,] arr) {
            return new Matrix(arr);
        }

        /// <summary>転置</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix T => Transpose(this);

        /// <summary>転置</summary>
        public static Matrix Transpose(Matrix m) {
            Matrix ret = new(m.Columns, m.Rows);

            for (int i = 0; i < m.Rows; i++) {
                for (int j = 0; j < m.Columns; j++) {
                    ret.e[j, i] = m.e[i, j];
                }
            }

            return ret;
        }

        /// <summary>逆行列</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Matrix Inverse => Invert(this);

        /// <summary>逆行列</summary>
        public static Matrix Invert(Matrix m) {
            if (IsZero(m) || !IsFinite(m)) {
                return Invalid(m.Columns, m.Rows);
            }
            if (m.Rows == m.Columns) {
                if (m.Rows == 1) {
                    return new Matrix(new ddouble[,] { { 1d / m.e[0, 0] } }, cloning: false);
                }
                if (m.Rows == 2) {
                    return Invert2x2(m);
                }
                if (m.Rows == 3) {
                    return Invert3x3(m);
                }

                return GaussianEliminate(m);
            }
            else if (m.Rows < m.Columns) {
                Matrix mt = m.T, mr = m * mt;
                return mt * mr.Inverse;
            }
            else {
                Matrix mt = m.T, mr = m.T * m;
                return mr.Inverse * mt;
            }
        }

        /// <summary>ノルム</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Norm =>
            IsFinite(this) ? (IsZero(this) ? 0d : ddouble.Ldexp(ddouble.Sqrt(ScaleB(this, -MaxExponent).SquareNorm), MaxExponent))
            : !IsValid(this) ? ddouble.NaN : ddouble.PositiveInfinity;

        /// <summary>ノルム2乗</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble SquareNorm {
            get {
                ddouble sum_sq = 0d;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        sum_sq += e[i, j] * e[i, j];
                    }
                }

                return sum_sq;
            }
        }

        /// <summary>合計</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Sum {
            get {
                ddouble sum = 0d;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        sum += e[i, j];
                    }
                }

                return sum;
            }
        }

        /// <summary>平均</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble Mean => Sum / checked(Rows * Columns);

        /// <summary>最大指数</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int MaxExponent {
            get {
                int max_exponent = int.MinValue;

                for (int i = 0; i < Rows; i++) {
                    for (int j = 0; j < Columns; j++) {
                        if (ddouble.IsFinite(e[i, j])) {
                            max_exponent = Math.Max(Math.ILogB((double)e[i, j]), max_exponent);
                        }
                    }
                }

                return max_exponent;
            }
        }

        /// <summary>2べき乗スケーリング</summary>
        public static Matrix ScaleB(Matrix matrix, int n) {
            Matrix ret = matrix.Copy();

            for (int i = 0; i < ret.Rows; i++) {
                for (int j = 0; j < ret.Columns; j++) {
                    ret.e[i, j] = ddouble.Ldexp(ret.e[i, j], n);
                }
            }

            return ret;
        }

        /// <summary>行ベクトル</summary>
        /// <param name="row_index">行</param>
        public Vector Horizontal(int row_index) {
            Vector ret = Vector.Zero(Columns);

            for (int i = 0; i < Columns; i++) {
                ret.v[i] = e[row_index, i];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Vector[] Horizontals => (new Vector[Rows]).Select((_, idx) => Horizontal(idx)).ToArray();

        /// <summary>列ベクトル</summary>
        /// <param name="column_index">列</param>
        public Vector Vertical(int column_index) {
            Vector ret = Vector.Zero(Rows);

            for (int i = 0; i < Rows; i++) {
                ret.v[i] = e[i, column_index];
            }

            return ret;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Vector[] Verticals => (new Vector[Columns]).Select((_, idx) => Vertical(idx)).ToArray();

        /// <summary>対角成分</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ddouble[] Diagonals {
            get {
                if (!IsSquare(this)) {
                    throw new ArithmeticException("not square matrix");
                }

                ddouble[] diagonals = new ddouble[Size];

                for (int i = 0; i < Size; i++) {
                    diagonals[i] = e[i, i];
                }

                return diagonals;
            }
        }


        public static Matrix FromDiagonals(ddouble[] vs) {
            ddouble[,] v = new ddouble[vs.Length, vs.Length];

            for (int i = 0; i < vs.Length; i++) {
                v[i, i] = vs[i];
            }

            return new Matrix(v, cloning: false);
        }

        /// <summary>ゼロ行列</summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        public static Matrix Zero(int rows, int columns) {
            return new Matrix(rows, columns);
        }

        /// <summary>ゼロ行列</summary>
        /// <param name="size"行列サイズ</param>
        public static Matrix Zero(int size) {
            return new Matrix(size, size);
        }

        /// <summary>定数行列</summary>
        public static Matrix Fill(int rows, int columns, ddouble value) {
            ddouble[,] v = new ddouble[rows, columns];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    v[i, j] = value;
                }
            }

            return new Matrix(v, cloning: false);
        }

        /// <summary>定数行列</summary>
        public static Matrix Fill(int size, ddouble value) {
            return Fill(size, size, value);
        }

        /// <summary>単位行列</summary>
        /// <param name="size">行列サイズ</param>
        public static Matrix Identity(int size) {
            Matrix ret = new(size, size);

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    ret.e[i, j] = (i == j) ? 1 : 0;
                }
            }

            return ret;
        }

        /// <summary>不正な行列</summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        public static Matrix Invalid(int rows, int columns) {
            return Fill(rows, columns, value: ddouble.NaN);
        }

        /// <summary>不正な行列</summary>
        /// <param name="size">行列サイズ</param>
        public static Matrix Invalid(int size) {
            return Fill(size, value: ddouble.NaN);
        }

        /// <summary>正方行列か判定</summary>
        public static bool IsSquare(Matrix matrix) {
            return matrix.Rows == matrix.Columns;
        }

        /// <summary>対角行列か判定</summary>
        public static bool IsDiagonal(Matrix matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (i != j && matrix.e[i, j] != 0) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>ゼロ行列か判定</summary>
        public static bool IsZero(Matrix matrix) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (matrix.e[i, j] != 0d) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>単位行列か判定</summary>
        public static bool IsIdentity(Matrix matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (i == j) {
                        if (matrix.e[i, j] != 1d) {
                            return false;
                        }
                    }
                    else {
                        if (matrix.e[i, j] != 0d) {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>対称行列か判定</summary>
        public static bool IsSymmetric(Matrix matrix) {
            if (!IsSquare(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = i + 1; j < matrix.Columns; j++) {
                    if (matrix.e[i, j] != matrix.e[j, i]) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>有限行列か判定</summary>
        public static bool IsFinite(Matrix matrix) {
            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (!ddouble.IsFinite(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>無限要素を含む行列か判定</summary>
        public static bool IsInfinity(Matrix matrix) {
            if (!IsValid(matrix)) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (ddouble.IsInfinity(matrix.e[i, j])) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>有効な行列か判定</summary>
        public static bool IsValid(Matrix matrix) {
            if (matrix.Rows < 1 || matrix.Columns < 1) {
                return false;
            }

            for (int i = 0; i < matrix.Rows; i++) {
                for (int j = 0; j < matrix.Columns; j++) {
                    if (ddouble.IsNaN(matrix.e[i, j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>正則行列か判定</summary>
        public static bool IsRegular(Matrix matrix) {
            return IsFinite(Invert(matrix));
        }

        /// <summary>等しいか判定</summary>
        public override bool Equals(object obj) {
            return (obj is not null) && obj is Matrix matrix && matrix == this;
        }

        /// <summary>ハッシュ値</summary>
        public override int GetHashCode() {
            return e[0, 0].GetHashCode();
        }

        /// <summary>クローン</summary>
        public object Clone() {
            return new Matrix(e);
        }

        /// <summary>ディープコピー</summary>
        public Matrix Copy() {
            return new Matrix(e);
        }

        public IEnumerator<(int row_index, int column_index, ddouble val)> GetEnumerator() {
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    yield return (i, j, e[i, j]);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
