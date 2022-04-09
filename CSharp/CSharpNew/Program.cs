using System;
/**/
namespace CSharpNew
{
    public class BaseC
    {
        public int x;
        public void Invoke()
        {
        }
        public class NestedC
        {
            public int x = 100;
            public int y;
        }
    }
    public class DerivedC : BaseC
    {
        new public class NestedC
        {
            public int x = 200;
            public int y;
            public int z;
        }
        public new int x;
        public new void Invoke()
        {

        }
        static void Main(string[] args)
        {
            NestedC nestedC = new NestedC();

            Console.WriteLine($"x={nestedC.x};y={nestedC.y};z={nestedC.z}");

            BaseC.NestedC nested = new BaseC.NestedC();

            var d = new DerivedC();
            var b = new BaseC();
            b = d;
            Console.WriteLine($"x={nested.x};y={nested.y};");

        }
    }
}
