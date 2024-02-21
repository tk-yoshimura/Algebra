using DoubleDouble;

namespace Algebra {
    ///<summary>ベクトルクラス</summary>
    public partial class Vector {
        /// <summary>メッシュグリッド</summary>
        public static (Vector x, Vector y) MeshGrid(Vector x, Vector y) {
            int n = checked(x.Dim * y.Dim);

            ddouble[] rx = x.v, ry = y.v;
            ddouble[] vx = new ddouble[n], vy = new ddouble[n];

            for (int i = 0, idx = 0; i < y.Dim; i++) {
                for (int j = 0; j < x.Dim; j++, idx++) {
                    vx[idx] = rx[j];
                    vy[idx] = ry[i];
                }
            }

            return (
                new Vector(vx, cloning: false),
                new Vector(vy, cloning: false)
            );
        }

        /// <summary>メッシュグリッド</summary>
        public static (Vector x, Vector y, Vector z) MeshGrid(Vector x, Vector y, Vector z) {
            int n = checked(x.Dim * y.Dim * z.Dim);

            ddouble[] rx = x.v, ry = y.v, rz = z.v;
            ddouble[] vx = new ddouble[n], vy = new ddouble[n], vz = new ddouble[n];

            for (int i = 0, idx = 0; i < z.Dim; i++) {
                for (int j = 0; j < y.Dim; j++) {
                    for (int k = 0; k < x.Dim; k++, idx++) {
                        vx[idx] = rx[k];
                        vy[idx] = ry[j];
                        vz[idx] = rz[i];
                    }
                }
            }

            return (
                new Vector(vx, cloning: false),
                new Vector(vy, cloning: false),
                new Vector(vz, cloning: false)
            );
        }

        /// <summary>メッシュグリッド</summary>
        public static (Vector x, Vector y, Vector z, Vector w) MeshGrid(Vector x, Vector y, Vector z, Vector w) {
            int n = checked(x.Dim * y.Dim * z.Dim * w.Dim);

            ddouble[] rx = x.v, ry = y.v, rz = z.v, rw = w.v;
            ddouble[] vx = new ddouble[n], vy = new ddouble[n], vz = new ddouble[n], vw = new ddouble[n];

            for (int i = 0, idx = 0; i < w.Dim; i++) {
                for (int j = 0; j < z.Dim; j++) {
                    for (int k = 0; k < y.Dim; k++) {
                        for (int m = 0; m < x.Dim; m++, idx++) {
                            vx[idx] = rx[m];
                            vy[idx] = ry[k];
                            vz[idx] = rz[j];
                            vw[idx] = rw[i];
                        }
                    }
                }
            }

            return (
                new Vector(vx, cloning: false),
                new Vector(vy, cloning: false),
                new Vector(vz, cloning: false),
                new Vector(vw, cloning: false)
            );
        }
    }
}
