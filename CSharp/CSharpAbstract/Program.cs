using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpAbstract
{

    class Program
    {
        static async Task Main(string[] args)
        {
            //int[] array = { 2, 3, 4, 6, 9 };

            //foreach (ref int i in array.AsSpan())
            //{
            //    array[i] = i++;
            //}
            //Square square = new Square(12);
            //Console.WriteLine($"Area of the square = {square.GetArea()}");
            //System.Threading.Thread thread = new System.Threading.Thread(A);
            //thread.Start();
            //System.Threading.Thread thread2 = new System.Threading.Thread(B);
            //thread2.Start();
            //System.Threading.ThreadPool.QueueUserWorkItem(A);
            //System.Threading.ThreadPool.QueueUserWorkItem(B);
            //Console.Read();
            //CancellationTokenSource t = new CancellationTokenSource();
            ////Task<int> t2=Task<int>.Run(F,);
            ////t2.ContinueWith()   
            //ProcessPriorityClass
            ////F();
            //B("");
            //var sw = new Stopwatch();
            //sw.Start();
            //Task delay = Task.Delay(5000);
            //Console.WriteLine("async: Running for {0} seconds", sw.Elapsed.TotalSeconds);

            //Console.WriteLine("1:" + Thread.CurrentThread.ManagedThreadId);
            //Console.ReadLine();


            //var task = new Task<string>();
            //task.Start()

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
