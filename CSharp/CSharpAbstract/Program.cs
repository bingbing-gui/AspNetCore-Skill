using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpAbstract
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var binaryStr = Convert.ToString(529, 2);
            int count = 0;
            int LongC = 0;
            for (int i = 0; i < binaryStr.Length; i++)
            {
                if (binaryStr[i] == '0')
                {
                    count++;
                }
                else
                {
                    if (count > LongC)
                    {
                        LongC = count;
                    }
                    count = 0;
                }

            }
            Console.WriteLine(LongC);
            Console.WriteLine(binaryStr);

            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

           

            var index = SerachIndex(array, 6);

            List<int> list = new List<int> { 1, 3, 6, 9 };
            List<int> list2 = new List<int> { 1, 2, 4, 6, 11 };
            list.AddRange(list2);
            for (int i = 0; i < list.Count(); i++)
            {
                for (int j = i + 1; j < list.Count(); j++)
                {
                    if (list[i] > list[j])
                    {
                        int temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            list.ForEach(i => Console.WriteLine(i));

            string orign = "aabbbbcccccddd";
            List<char> dest = new List<char>();
            dest.AddRange(orign);
            var dict = new Dictionary<char, int>();
            foreach (var element in dest)
            {
                if (dict.ContainsKey(element))
                {
                    int i = dict[element];
                    dict[element] = i + 1;
                }
                else
                {
                    dict.Add(element, 1);
                }
            }
            var stringBuilder = new StringBuilder();
            foreach (var ele in dict)
            {
                stringBuilder.Append(ele.Key);
                stringBuilder.Append(ele.Value);
            }
            Console.WriteLine(stringBuilder.ToString());

            Console.ReadLine();
        }
        public static int SerachIndex(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            int middle = 0;
            while (left < right)
            {
                middle = (left + right) / 2;
                if (nums[middle] == target)
                {
                    return middle;
                }
                else if (nums[middle] > target)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }
            if (nums[middle] > target)
            {
                return middle - 1;
            }
            else
            {
                return middle;
            }
        }
        public static async Task<int> F()
        {

            //Console.WriteLine("2:"+Thread.CurrentThread.ManagedThreadId);
            //await E();
            //Console.WriteLine("3:" + Thread.CurrentThread.ManagedThreadId);
            return 0;
        }
        public static async Task<bool> E()
        {
            Console.WriteLine("4:" + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(5000);
            Console.WriteLine("5:" + Thread.CurrentThread.ManagedThreadId);
            return true;
        }
        public static void A(object o)
        {
            throw new DuplicateWaitObjectException();
        }
        public static void B(object b)
        {
            Console.WriteLine("ABCDEFG");
        }
    }
    abstract class Shape
    {
        public abstract int GetArea();
    }
    class Square : Shape
    {
        private int _side;

        public Square(int n) => _side = n;

        public override int GetArea() => _side * _side;
    }


    interface I
    {
        void M();
        void N();
    }
    abstract class C : I
    {
        public abstract void M();
        public void N()
        {

        }
    }

    abstract class BaseClass
    {
        protected int _x = 100;
        protected int _y = 150;

        public abstract void AbstractMethod();

        public abstract int X { get; }
        public abstract int Y { get; }

        public abstract int N { get; set; }
    }
    class DerivedClass : BaseClass
    {
        public override void AbstractMethod()
        {
            _x++;
            _y++;
        }
        public override int X
        {
            get
            {
                return _x + 10;
            }
        }
        public override int Y
        {
            get
            {
                return _y + 10;
            }
        }
        public override int N
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
