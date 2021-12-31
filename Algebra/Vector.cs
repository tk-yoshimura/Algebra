using DoubleDouble;
using System;
using System.Diagnostics;
using System.Linq;

namespace Algebra {
    ///<summary>ベクトルクラス</summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class Vector : ICloneable {
        internal readonly ddouble[] v;

        /// <summary>コンストラクタ</summary>
        public Vector(params double[] v) : this(v.Length) {
            for (int i = 0; i < v.Length; i++) {
                this.v[i] = v[i];
            }
        }

        /// <summary>コンストラクタ</summary>
        public Vector(params ddouble[] v) {
            this.v = (ddouble[])v.Clone();
        }

        /// <summary>コンストラクタ</summary>
        public Vector(int size) {
            this.v = new ddouble[size];
        }

        /// <summary>インデクサ</summary>
        public ddouble this[int index] {
            get => v[index];
            set => v[index] = value;
        }

        /// <summary>X成分</summary>
        public ddouble X {
            get => v[0];
            set => v[0] = value;
        }

        /// <summary>Y成分</summary>
        public ddouble Y {
            get => v[1];
            set => v[1] = value;
        }

        /// <summary>Z成分</summary>
        public ddouble Z {
            get => v[2];
            set => v[2] = value;
        }

        /// <summary>W成分</summary>
        public ddouble W {
            get => v[3];
            set => v[3] = value;
        }

        /// <summary>次元数</summary>
        public int Dim => v.Length;

        /// <summary>ノルム</summary>
        public ddouble Norm => ddouble.Sqrt(SquareNorm);

        /// <summary>ノルム2乗</summary>
        public ddouble SquareNorm {
            get {
                ddouble norm = 0;
                foreach (var vi in v) {
                    norm += vi * vi;
                }

                return norm;
            }
        }

        /// <summary>行ベクトル</summary>
        public Matrix Horizontal {
            get {
                Matrix ret = new(1, Dim);
                for (int i = 0; i < Dim; i++) {
                    ret.e[0, i] = v[i];
                }

                return ret;
            }
        }

        /// <summary>列ベクトル</summary>
        public Matrix Vertical {
            get {
                Matrix ret = new(Dim, 1);
                for (int i = 0; i < Dim; i++) {
                    ret.e[i, 0] = v[i];
                }

                return ret;
            }
        }

        /// <summary>正規化</summary>
        public Vector Normal => this / Norm;

        /// <summary>単項プラス</summary>
        public static Vector operator +(Vector vector) {
            return (Vector)vector.Clone();
        }

        /// <summary>単項マイナス</summary>
        public static Vector operator -(Vector vector) {
            ddouble[] v = new ddouble[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = -vector.v[i];
            }
            return new Vector(v);
        }

        /// <summary>ベクトル加算</summary>
        public static Vector operator +(Vector vector1, Vector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("unmatch size", $"{vector1},{vector2}");
            }

            int size = vector1.Dim;
            ddouble[] v = new ddouble[size];

            for (int i = 0; i < size; i++) {
                v[i] = vector1.v[i] + vector2.v[i];
            }

            return new Vector(v);
        }

        /// <summary>ベクトル減算</summary>
        public static Vector operator -(Vector vector1, Vector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("unmatch size", $"{vector1},{vector2}");
            }

            int size = vector1.Dim;
            ddouble[] v = new ddouble[size];

            for (int i = 0; i < size; i++) {
                v[i] = vector1.v[i] - vector2.v[i];
            }

            return new Vector(v);
        }

        /// <summary>スカラー倍</summary>
        public static Vector operator *(ddouble r, Vector vector) {
            ddouble[] v = new ddouble[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = vector.v[i] * r;
            }

            return new Vector(v);
        }

        /// <summary>スカラー倍</summary>
        public static Vector operator *(Vector vector, ddouble r) {
            return r * vector;
        }

        /// <summary>スカラー逆数倍</summary>
        public static Vector operator /(Vector vector, ddouble r) {
            return (1d / r) * vector;
        }

        /// <summary>ベクトル間距離</summary>
        public static ddouble Distance(Vector vector1, Vector vector2) {
            return (vector1 - vector2).Norm;
        }

        /// <summary>ベクトル間距離2乗</summary>
        public static ddouble SquareDistance(Vector vector1, Vector vector2) {
            return (vector1 - vector2).SquareNorm;
        }

        /// <summary>ベクトル内積</summary>
        public static ddouble InnerProduct(Vector vector1, Vector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("unmatch size", $"{vector1},{vector2}");
            }

            ddouble sum = 0;
            for (int i = 0, dim = vector1.Dim; i < dim; i++) {
                sum += vector1.v[i] * vector2.v[i];
            }

            return sum;
        }

        /// <summary>ゼロベクトル</summary>
        public static Vector Zero(int size) {
            return new Vector(new ddouble[size]);
        }

        /// <summary>ゼロベクトルか判定</summary>
        public static bool IsZero(Vector vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (vector.v[i] != 0d) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>不正なベクトル</summary>
        public static Vector Invalid(int size) {
            ddouble[] v = new ddouble[size];
            for (int i = 0; i < size; i++) {
                v[i] = ddouble.NaN;
            }

            return new Vector(v);
        }

        /// <summary>有効なベクトルか判定</summary>
        public static bool IsValid(Vector vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (ddouble.IsNaN(vector.v[i]) || ddouble.IsInfinity(vector.v[i])) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>ベクトルが等しいか</summary>
        public static bool operator ==(Vector vector1, Vector vector2) {
            if (ReferenceEquals(vector1, vector2)) {
                return true;
            }
            if (vector1 is null || vector2 is null) {
                return false;
            }

            return vector1.v.SequenceEqual(vector2.v);
        }

        /// <summary>ベクトルが異なるか判定</summary>
        public static bool operator !=(Vector vector1, Vector vector2) {
            return !(vector1 == vector2);
        }

        /// <summary>等しいか判定</summary>
        public override bool Equals(object obj) {
            return (obj is not null) && obj is Vector vector && vector == this;
        }

        /// <summary>ハッシュ値</summary>
        public override int GetHashCode() {
            return Dim > 0 ? v[0].GetHashCode() : 0;
        }

        /// <summary>クローン</summary>
        public object Clone() {
            return new Vector(v);
        }

        /// <summary>コピー</summary>
        public Vector Copy() {
            return new Vector(v);
        }

        /// <summary>文字列化</summary>
        public override string ToString() {
            if (Dim <= 0) {
                return string.Empty;
            }

            string str = $"{v[0]}";

            for (int i = 1; i < Dim; i++) {
                str += $",{v[i]}";
            }

            return str;
        }
    }
}
