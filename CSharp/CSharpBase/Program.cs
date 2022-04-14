using System;
using System.Collections.Generic;
using System.Linq;

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


            int N = 10;
            int[] arry = new int[N];
            Random rd = new Random();
            for (int i = 1; i <= N; i++)
            {
                int random = rd.Next(1, 1000);
                arry[i - 1] = random;
            }
            for (int i = 0; i < arry.Length; i++)
            {
                Console.WriteLine(arry[i]);
            }
            Console.ReadLine();

        }

        static Dictionary<string, Func<string, bool>> m_Validators = new Dictionary<string, Func<string, bool>>(StringComparer.OrdinalIgnoreCase)
            {
                { "--name", x => x != null && x.Length >= 3 && x.Length <= 10 },
                { "--count", x => x != null && int.TryParse(x, out int v) && v >= 10 && v <= 100 },
                { "--help", null },
            };
        public static int Validate(string[] args)
        {
            if (null == args) return -1; // we're asked not to throw exceptions
            HashSet<string> commands = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < args.Length; ++i)
            {
                if (!m_Validators.TryGetValue(args[i], out var validator))
                    return -1; // unknown command (--name, --count, --help expected)      
                               // If command appears two or more times we return -1, e.g.
                //--name ABC --count 34--name Other
                //If you accept duplicates, put just
                commands.Add(args[i]);
                if (!commands.Add(args[i]))
                    return -1;
                if (null != validator)
                {
                    if (i >= args.Length - 1)
                        return -1;
                    //--name or--count are the last operators
                    if (!validator(args[++i]))
                        return -1; // validation falure, e.g. --count abcd
                }
            }
            //validation has been passed; check if we have--help
            return commands.Contains("--help") ? 1 : 0;
        }
    }
}

