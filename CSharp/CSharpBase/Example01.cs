using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBase
{
    public class Person
    {
        protected string ssn = "444-55-6666";
        protected string name = "John L. Malgraine";
        public virtual void GetInfo()
        {
            Console.WriteLine("Name: {0}", name);
            Console.WriteLine("SSN: {0}", ssn);
        }
    }
    class Employee : Person
    {
        public string id = "ABC567EFG";
        public override void GetInfo()
        {
            // Calling the base class GetInfo method:
            base.GetInfo();
            Console.WriteLine("Employee ID: {0}", id);
        }
    }
}
