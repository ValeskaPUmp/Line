using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Line
{
    public unsafe struct Line
    {
        private Point* p1;
        private Point* p2;
        private float normalx;
        private float normaly;
        private float normalz;
        private Point* p3;
        
        public Line(Point* p1, Point* p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            normalx = p1->x - p2->x;
            normaly = p1->y - p2->y;
            normalz = p1->z - p2->z;
            p3 = null;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport("kernel32.dll")]
        static extern bool VirtualProtect(int* lpAddress, uint dwSize, uint flNewProtect, uint* lpflOldProtect);

        private float FloatSqrt(float sqrt)
        {
            int ki = BitConverter.ToInt32(BitConverter.GetBytes(sqrt),0);
            int i = *(int*) &ki;
            int x = (1 << 29) + (i >> 1) - (1 << 22);
            return BitConverter.ToSingle(BitConverter.GetBytes(x), 0);
        }

        public float checksum(Point* p3)
        {
            this.p3 = p3;
            float checkp1 = FloatSqrt(FloatPow(p3->x - p1->x, 2) + FloatPow(p3->y - p1->y, 2)+FloatPow(p3->z - p1->z, 2));
            float checkp2 = FloatSqrt(FloatPow(p3->x - p2->x, 2) + FloatPow(p3->y - p2->y, 2)+FloatPow(p3->z - p2->z, 2));
            if(checkp1<checkp2)
            {
                return connect(p1);
            }
            else
            {
                return connect(p2);
            }
        }

        private float connect(Point* point)
        {
            float nortx = point->x - p3->x;
            float norty = point->x - p3->x;
            float nortz = point->z - p3->z;
            Point p = new Point((norty * normalz - nortz * normaly), ((nortx * normalz) - (nortz * normalx)),
                ((nortx * normaly) - (norty * normalx)));
            return FloatSqrt((FloatPow(p.x, 2) + FloatPow(p.y, 2) + FloatPow(p.z, 2)) /
                             (FloatPow(nortx, 2) + FloatPow(norty, 2) + FloatPow(nortz, 2)));
        }
        private float FloatPow(float a, float b)
        {
            
            long i = BitConverter.DoubleToInt64Bits(a);
            i = (long) (4606853616395542500L +b * (i - 4606853616395542500L));
            return (float) BitConverter.Int64BitsToDouble(i);
        }



    }
}