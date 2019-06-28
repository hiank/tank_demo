using UnityEngine;

namespace Moba
{
    public class DecimalVector3
    {

        public DecimalVector3()
        {
            x = y = z = 0;
        }

        public DecimalVector3(DecimalVector3 dv)
        {
            x = dv.x;
            y = dv.y;
            z = dv.z;
        }

        public DecimalVector3(decimal dx, decimal dy, decimal dz)
        {
            x = dx;
            y = dy;
            z = dz;
        }

        public DecimalVector3(Vector3 v)
        {
            x = new decimal(v.x);
            y = new decimal(v.y);
            z = new decimal(v.z);
        }

        public DecimalVector3(WarPb.DecimalVector3 wpv)
        {
            x = decimal.Parse(wpv.X);
            y = decimal.Parse(wpv.Y);
            z = decimal.Parse(wpv.Z);
        }


        public decimal x, y, z;

        public bool IsZero()
        {
            return x == 0 && y == 0 && z == 0;
        }

        public Vector3 ToVector3()
        {
            return new Vector3((float)x, (float)y, (float)z);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return this == obj as DecimalVector3;
        }

        public override int GetHashCode()
        {

            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        public static bool operator == (DecimalVector3 l, DecimalVector3 r)
        {
            return (l.x == r.x) && (l.y == r.y) && (l.z == r.z);
        }
        public static bool operator != (DecimalVector3 l, DecimalVector3 r)
        {
            return (l.x != r.x) || (l.y != r.y) || (l.z != r.z);
        }
    }
}
