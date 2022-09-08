using System;

namespace Line
{
    internal unsafe class Program
    {
        public static void Main(string[] args)
        {
            int a = DateTime.Now.Millisecond;
            Point p1 = new Point(0, 100, 100);
            Point p2 = new Point(3,1,-1);
            Point p3 = new Point(5, 2, 3);
            Line l = new Line(&p3,&p2);
            int b = DateTime.Now.Millisecond;
            Console.WriteLine(l.checksum(&p1)+"-------"+(b-a));


        }
    }
}