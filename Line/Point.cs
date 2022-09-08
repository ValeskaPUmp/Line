using System;

namespace Line
{
    public struct Point
    {
        public float x { get; }
        public float y { get; }
        public float z { get; }
        public Point(float x, float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
    }
}