﻿using DoubleDouble;
using System;
using System.Diagnostics;
using System.Linq;

namespace Algebra {
    /// <summary>行列クラス</summary>
    public partial class Matrix {
        /// <summary>固有値計算</summary>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public static ddouble[] EigenValues(Matrix m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (m.Size <= 1) {
                return [m[0, 0]];
            }
            if (m.Size == 2) {
                return SortEigenByNorm(EigenValues2x2(m));
            }

            precision_level = precision_level >= 0 ? precision_level : 4 * m.Size;

            int n = m.Size, notconverged = n;
            int exponent = m.MaxExponent;
            (Matrix u, _, _) = PermutateDiagonal(ScaleB(m, -exponent));

            Vector eigen_values = Vector.Fill(n, 1);
            Vector eigen_values_prev = eigen_values.Copy();

            Vector eigen_diffnorms = Vector.Fill(n, ddouble.PositiveInfinity);
            Vector eigen_diffnorms_prev = eigen_diffnorms.Copy();

            Matrix d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    ddouble mu = EigenValues2x2(d[^2.., ^2..])[1];

                    if (ddouble.IsFinite(mu)) {
                        (Matrix q, Matrix r) = QR(DiagonalAdd(d, -mu));
                        d = DiagonalAdd(r * q, mu);
                    }
                    else {
                        (Matrix q, Matrix r) = QR(d);
                        d = r * q;
                    }

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues2x2(d);
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    ddouble eigen_diffnorm = ddouble.Abs(eigen_values[i] - eigen_values_prev[i]);
                    eigen_diffnorms[i] = eigen_diffnorm;
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    if (i >= 2 && iter_qr < precision_level) {
                        if (ddouble.ILogB(eigen_diffnorms[i]) > -98 || eigen_diffnorms_prev[i] > eigen_diffnorms[i]) {
                            break;
                        }
                    }

                    notconverged--;
                }

                if (notconverged <= 0) {
                    break;
                }

                if (d.Size > 2) {
                    Vector lower = d[^1, ..^1];
                    ddouble eigen = d[^1, ^1];

                    if (lower.MaxExponent < ddouble.ILogB(eigen) - 106L) {
                        d = d[..^1, ..^1];
                    }
                }

                eigen_values_prev[..notconverged] = eigen_values[..notconverged];
                eigen_diffnorms_prev[..notconverged] = eigen_diffnorms[..notconverged];
            }

            eigen_values = Vector.ScaleB(eigen_values, exponent);

            return SortEigenByNorm(eigen_values);
        }

        /// <summary>固有値・固有ベクトル</summary>
        /// <param name="eigen_values">固有値</param>
        /// <param name="eigen_vectors">固有ベクトル</param>
        /// <param name="precision_level">精度(収束ループを回す回数)</param>
        public static (ddouble[] eigen_values, Vector[] eigen_vectors) EigenValueVectors(Matrix m, int precision_level = -1) {
            if (!IsSquare(m) || m.Size < 1) {
                throw new ArgumentException("not square matrix", nameof(m));
            }

            if (IsZero(m)) {
                return (Vector.Zero(m.Size), Identity(m.Size).Horizontals);
            }
            if (m.Size <= 1) {
                return ([m[0, 0]], [new Vector(1)]);
            }
            if (m.Size == 2) {
                return SortEigenByNorm(EigenValueVectors2x2(m));
            }

            precision_level = precision_level >= 0 ? precision_level : 4 * m.Size;

            int n = m.Size, notconverged = n;
            int exponent = m.MaxExponent;
            (Matrix u, _, int[] perm_indexes) = PermutateDiagonal(ScaleB(m, -exponent));

            Vector eigen_values = Vector.Fill(n, 1);
            Vector eigen_values_prev = eigen_values.Copy();

            Vector eigen_diffnorms = Vector.Fill(n, ddouble.PositiveInfinity);
            Vector eigen_diffnorms_prev = eigen_diffnorms.Copy();

            Vector[] eigen_vectors = Identity(n).Horizontals;

            Matrix d = u;

            for (int iter_qr = 0; iter_qr <= precision_level; iter_qr++) {
                if (d.Size > 2) {
                    ddouble mu = EigenValues2x2(d[^2.., ^2..])[1];

                    if (ddouble.IsFinite(mu)) {
                        (Matrix q, Matrix r) = QR(DiagonalAdd(d, -mu));
                        d = DiagonalAdd(r * q, mu);
                    }
                    else {
                        (Matrix q, Matrix r) = QR(d);
                        d = r * q;
                    }

                    eigen_values[..d.Size] = d.Diagonals[..d.Size];
                }
                else {
                    eigen_values[..2] = EigenValues2x2(d);
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    ddouble eigen_diffnorm = ddouble.Abs(eigen_values[i] - eigen_values_prev[i]);
                    eigen_diffnorms[i] = eigen_diffnorm;
                }

                for (int i = notconverged - 1; i >= 0; i--) {
                    if (i >= 2 && iter_qr < precision_level) {
                        if (ddouble.ILogB(eigen_diffnorms[i]) > -98 || eigen_diffnorms_prev[i] > eigen_diffnorms[i]) {
                            break;
                        }
                    }

                    ddouble eigen_val = eigen_values[i];

                    Vector v = u[.., i], h = u[i, ..];
                    ddouble nondiagonal_absmax = 0d;
                    for (int k = 0; k < v.Dim; k++) {
                        if (k == i) {
                            continue;
                        }

                        nondiagonal_absmax =
                            ddouble.Max(nondiagonal_absmax, ddouble.Abs(v[k]), ddouble.Abs(h[k]));
                    }

                    ddouble eps = ddouble.Ldexp(nondiagonal_absmax, -74);

                    Matrix g = DiagonalAdd(u, -eigen_val + eps).Inverse;

                    Vector x;

                    if (IsFinite(g)) {
                        ddouble norm, norm_prev = ddouble.NaN;
                        x = Vector.Fill(n, 0.125);
                        x[i] = ddouble.One;

                        for (int iter_vector = 0; iter_vector < precision_level; iter_vector++) {
                            x = (g * x).Normal;

                            norm = (u * x - eigen_val * x).Norm;

                            if (ddouble.ILogB(norm) < -53 && norm >= norm_prev) {
                                break;
                            }

                            norm_prev = norm;
                        }
                    }
                    else {
                        x = Vector.Zero(n);
                        x[i] = 1d;
                    }

                    eigen_vectors[i] = x[perm_indexes];
                    notconverged--;
                }

                if (notconverged <= 0) {
                    break;
                }

                if (d.Size > 2) {
                    Vector lower = d[^1, ..^1];
                    ddouble eigen = d[^1, ^1];

                    if (lower.MaxExponent < ddouble.ILogB(eigen) - 106L) {
                        d = d[..^1, ..^1];
                    }
                }

                eigen_values_prev[..notconverged] = eigen_values[..notconverged];
                eigen_diffnorms_prev[..notconverged] = eigen_diffnorms[..notconverged];
            }

            eigen_values = Vector.ScaleB(eigen_values, exponent);

            return SortEigenByNorm((eigen_values, eigen_vectors));
        }

        private static ddouble[] EigenValues2x2(Matrix m) {
            Debug.Assert(m.Size == 2);

            ddouble m00 = m[0, 0], m11 = m[1, 1];
            ddouble m01 = m[0, 1], m10 = m[1, 0];

            ddouble b = m00 + m11, c = m00 - m11;

            ddouble d = ddouble.Sqrt(c * c + 4d * m01 * m10);

            ddouble val0 = (b + d) * 0.5d;
            ddouble val1 = (b - d) * 0.5d;

            if (ddouble.Abs(val0 - m11) >= ddouble.Abs(val1 - m11)) {
                return [val0, val1];
            }
            else {
                return [val1, val0];
            }
        }

        private static (ddouble[] eigen_values, Vector[] eigen_vectors) EigenValueVectors2x2(Matrix m) {
            Debug.Assert(m.Size == 2);

            ddouble m00 = m[0, 0], m11 = m[1, 1];
            ddouble m01 = m[0, 1], m10 = m[1, 0];

            long diagonal_scale = long.Max(ddouble.ILogB(m00), ddouble.ILogB(m11));
            long nondiagonal_scale = long.Max(ddouble.ILogB(m01), ddouble.ILogB(m10));

            if (diagonal_scale - nondiagonal_scale < 106L) {
                ddouble b = m00 + m11, c = m00 - m11;

                ddouble d = ddouble.Sqrt(c * c + 4d * m01 * m10);

                ddouble val0 = (b + d) * 0.5d;
                ddouble val1 = (b - d) * 0.5d;

                Vector vec0, vec1;
                if (ddouble.Abs(m10) > ddouble.Abs(m01)) {
                    vec0 = new Vector((c + d) / (2d * m10), 1d).Normal;
                    vec1 = new Vector((c - d) / (2d * m10), 1d).Normal;
                }
                else {
                    vec0 = new Vector(1d, (-c + d) / (2d * m01)).Normal;
                    vec1 = new Vector(1d, (-c - d) / (2d * m01)).Normal;
                }

                if (ddouble.Abs(val0 - m11) >= ddouble.Abs(val1 - m11)) {
                    return (new ddouble[] { val0, val1 }, new Vector[] { vec0, vec1 });
                }
                else {
                    return (new ddouble[] { val1, val0 }, new Vector[] { vec1, vec0 });
                }
            }
            else {
                if (m00 != m11) {
                    Vector vec0 = (1, 0);
                    Vector vec1 = new Vector(m01 / (m11 - m00), 1).Normal;

                    return (new ddouble[] { m00, m11 }, new Vector[] { vec0, vec1 });
                }
                else {
                    return (new ddouble[] { m00, m11 }, new Vector[] { (1, 0), (0, 1) });
                }
            }
        }

        private static ddouble[] SortEigenByNorm(ddouble[] eigen_values) {
            ddouble[] eigen_values_sorted = [.. eigen_values.OrderByDescending(ddouble.Abs)];

            return eigen_values_sorted;
        }

        private static (ddouble[] eigen_values, Vector[] eigen_vectors) SortEigenByNorm((ddouble[] eigen_values, Vector[] eigen_vectors) eigens) {
            Debug.Assert(eigens.eigen_values.Length == eigens.eigen_vectors.Length);

            IOrderedEnumerable<(ddouble val, Vector vec)> eigens_sorted =
                eigens.eigen_values.Zip(eigens.eigen_vectors).OrderByDescending(item => ddouble.Abs(item.First));

            ddouble[] eigen_values_sorted = eigens_sorted.Select(item => item.val).ToArray();
            Vector[] eigen_vectors_sorted = eigens_sorted.Select(item => item.vec).ToArray();

            return (eigen_values_sorted, eigen_vectors_sorted);
        }

        private static (Matrix matrix, int[] indexes, int[] indexes_invert) PermutateDiagonal(Matrix m) {
            Debug.Assert(IsSquare(m));

            int n = m.Size;

            Vector rates = Vector.Zero(n);

            for (int i = 0; i < n; i++) {
                ddouble diagonal = m[i, i];

                Vector nondigonal = Vector.Concat(m[i, ..i], m[i, (i + 1)..]);

                ddouble nondigonal_norm = nondigonal.Norm;

                ddouble rate = ddouble.Abs(diagonal) / (nondigonal_norm + ddouble.Epsilon);

                rates[i] = rate;
            }

            int[] indexes = rates.Select(item => (item.index, item.val)).OrderBy(item => item.val).Select(item => item.index).ToArray();

            Matrix m_perm = m[indexes, indexes];

            int[] indexes_invert = new int[n];

            for (int i = 0; i < n; i++) {
                indexes_invert[indexes[i]] = i;
            }

            return (m_perm, indexes, indexes_invert);
        }
    }
}
