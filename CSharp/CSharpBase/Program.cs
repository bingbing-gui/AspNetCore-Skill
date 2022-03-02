using System;

namespace CSharpBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee E = new Employee();
            E.GetInfo();

            DerivedClass md = new DerivedClass();
            DerivedClass md1 = new DerivedClass(1);
        }
    }

}
