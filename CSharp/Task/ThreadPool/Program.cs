using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadPoolExample
{
    class Program
    {
        #region
        //public static void Main()
        //{
        //    // Queue the task.
        //    ThreadPool.QueueUserWorkItem(ThreadProc);
        //    Console.WriteLine("Main thread does some work, then sleeps.");
        //    Thread.Sleep(1000);

        //    Console.WriteLine("Main thread exits.");
        //}

        //// This thread procedure performs the task.
        //static void ThreadProc(Object stateInfo)
        //{
        //    // No state object was passed to QueueUserWorkItem, so stateInfo is null.
        //    Console.WriteLine("Hello from the thread pool.");
        //}
        #endregion
        #region
        static void Main(string[] args)
        {
            Stopwatch mywatch = new Stopwatch();
            Console.WriteLine("Thread Pool Execution");
            mywatch.Start();
            ExecuteWithThreadPoolMethod();
            mywatch.Stop();
            Console.WriteLine("Time consumed by ProcessWithThreadPoolMethod is :" + mywatch.ElapsedTicks.ToString());
            mywatch.Reset();
            Console.WriteLine("Thread Execution");
            mywatch.Start();
            ExecuteWithThreadMethod();
            mywatch.Stop();
            Console.WriteLine("Time consumed by ExecuteWithThreadMethod is : " + mywatch.ElapsedTicks.ToString());
            Console.Read();
        }
        static void ExecuteWithThreadPoolMethod()
        {
            for (int i = 0; i <= 100; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessTask));
            }
        }
        static void ExecuteWithThreadMethod()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread obj = new Thread(ProcessTask);
                obj.Start();
            }
        }
        static void ProcessTask(object callback)
        {
        }
        #endregion
    }
}
