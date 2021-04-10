using System;
using System.Linq;

namespace Algebra {
    ///<summary>ベクトルクラス</summary>
    public class Vector : ICloneable {
        private readonly double[] v;

        /// <summary>コンストラクタ</summary>
        public Vector(params double[] v) {
            this.v = (double[])v.Clone();
        }

        /// <summary>インデクサ</summary>
        public double this[int index] {
            get {
                return v[index];
            }
            set {
                v[index] = value;
            }
        }

        /// <summary>X成分</summary>
        public double X {
            get {
                return v[0];
            }
            set {
                v[0] = value;
            }
        }

        /// <summary>Y成分</summary>
        public double Y {
            get {
                return v[1];
            }
            set {
                v[1] = value;
            }
        }

        /// <summary>Z成分</summary>
        public double Z {
            get {
                return v[2];
            }
            set {
                v[2] = value;
            }
        }

        /// <summary>W成分</summary>
        public double W {
            get {
                return v[3];
            }
            set {
                v[3] = value;
            }
        }

        /// <summary>次元数</summary>
        public int Dim => v.Length;

        /// <summary>ノルム</summary>
        public double Norm => Math.Sqrt(SquareNorm);

        /// <summary>ノルム2乗</summary>
        public double SquareNorm {
            get {
                double norm = 0;
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
                    ret[0, i] = v[i];
                }

                return ret;
            }
        }

        /// <summary>列ベクトル</summary>
        public Matrix Vertical {
            get {
                Matrix ret = new(Dim, 1);
                for (int i = 0; i < Dim; i++) {
                    ret[i, 0] = v[i];
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
            double[] v = new double[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = -vector[i];
            }
            return new Vector(v);
        }

        /// <summary>ベクトル加算</summary>
        public static Vector operator +(Vector vector1, Vector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("unmatch size", $"{vector1},{vector2}");
            }

            int size = vector1.Dim;
            double[] v = new double[size];

            for (int i = 0; i < size; i++) {
                v[i] = vector1[i] + vector2[i];
            }

            return new Vector(v);
        }

        /// <summary>ベクトル減算</summary>
        public static Vector operator -(Vector vector1, Vector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("unmatch size", $"{vector1},{vector2}");
            }

            int size = vector1.Dim;
            double[] v = new double[size];

            for (int i = 0; i < size; i++) {
                v[i] = vector1[i] - vector2[i];
            }

            return new Vector(v);
        }

        /// <summary>スカラー倍</summary>
        public static Vector operator *(double r, Vector vector) {
            double[] v = new double[vector.Dim];

            for (int i = 0; i < vector.Dim; i++) {
                v[i] = vector[i] * r;
            }

            return new Vector(v);
        }

        /// <summary>スカラー倍</summary>
        public static Vector operator *(Vector vector, double r) {
            return r * vector;
        }

        /// <summary>スカラー逆数倍</summary>
        public static Vector operator /(Vector vector, double r) {
            return (1 / r) * vector;
        }

        /// <summary>ベクトル間距離</summary>
        public static double Distance(Vector vector1, Vector vector2) {
            return (vector1 - vector2).Norm;
        }

        /// <summary>ベクトル間距離2乗</summary>
        public static double SquareDistance(Vector vector1, Vector vector2) {
            return (vector1 - vector2).SquareNorm;
        }

        /// <summary>ベクトル内積</summary>
        public static double InnerProduct(Vector vector1, Vector vector2) {
            if (vector1.Dim != vector2.Dim) {
                throw new ArgumentException("unmatch size", $"{vector1},{vector2}");
            }

            double sum = 0;
            for (int i = 0, dim = vector1.Dim; i < dim; i++) {
                sum += vector1[i] * vector2[i];
            }

            return sum;
        }

        /// <summary>ゼロベクトル</summary>
        public static Vector Zero(int size) {
            return new Vector(new double[size]);
        }

        /// <summary>ゼロベクトルか判定</summary>
        public static bool IsZero(Vector vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (vector[i] != 0) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>不正なベクトル</summary>
        public static Vector Invalid(int size) {
            double[] v = new double[size];
            for (int i = 0; i < size; i++) {
                v[i] = double.NaN;
            }

            return new Vector(v);
        }

        /// <summary>有効なベクトルか判定</summary>
        public static bool IsValid(Vector vector) {
            for (int i = 0; i < vector.Dim; i++) {
                if (double.IsNaN(vector[i]) || double.IsInfinity(vector[i])) {
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
            return (!(obj is null)) && obj is Vector vector && vector == this;
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
