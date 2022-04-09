using System;

namespace CSharpReadOnly
{
    class Program
    {
        static void Main(string[] args)
        {
            SamplePoint p1 = new SamplePoint(11, 21, 32);   // OK
            Console.WriteLine($"p1: x={p1.x}, y={p1.y}, z={p1.z}");
            SamplePoint p2 = new SamplePoint();
            p2.x = 55;   // OK
            Console.WriteLine($"p2: x={p2.x}, y={p2.y}, z={p2.z}");
        }
    }
    class Age
    {
        /// <summary>
        /// readonly是运行时的常量
        /// </summary>
        public readonly int y = 5;
        public static readonly uint timeStamp = (uint)DateTime.Now.Ticks;
        private readonly int _year;
        Age(int year)
        {
            _year = year;
        }
        void ChangeYear()
        {

        }
    }

    public class SamplePoint
    {
        public int x;
        //Initialize a readonly field
        public readonly int y = 25;
        public readonly int z;
        public SamplePoint()
        {
            z = 24;
        }
        public SamplePoint(int p1,int p2,int p3)
        {
            x = p1;
            y = p2;
            z = p3;
        }
    }
}
