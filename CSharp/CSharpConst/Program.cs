using System;

namespace CSharpConst
{
    class Program
    {
        const int X = 0;
        public const double GravitationalConstant = 6.673e-11;
        private const string ProductName = "Visual C#";
        const string Language = "C#";
        const string Platform = ".NET";
        const string Version = "10.0";
        //C# 10 支持
        //const string FullProductName = $"{Platform} - Language: {Language} Version: {Version}";
        static void Main(string[] args)
        {
            var sc = new SampleClass(11, 22);
            Console.WriteLine($"x={sc.x},y={sc.y}");
            Console.WriteLine($"C1={SampleClass.C1},C2={SampleClass.C2}");
            const int C = 707;
            Console.WriteLine($"My local constant={C}");
            Console.ReadLine();
        }
    }

}
