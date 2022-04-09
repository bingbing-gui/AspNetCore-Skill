using System;

namespace CSharpEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new int[5] { 1, 2, 3, 4, 5 };
            foreach (ref int v in array.AsSpan())
            {
                v++;
            }
            Console.WriteLine(string.Join(",", array)); 
        }
    }
}
