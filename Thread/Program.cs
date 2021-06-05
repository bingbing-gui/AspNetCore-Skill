using System;
using System.Diagnostics;
using System.Threading;

public class Example
{
    #region ThreadStart Type
    //public static void Main()
    //{
    //    var th = new Thread(ExecuteInForeground);
    //    th.Start();
    //    Thread.Sleep(1000);
    //    Console.WriteLine("Main thread ({0}) exiting...",
    //                      Thread.CurrentThread.ManagedThreadId);
    //}
    //private static void ExecuteInForeground()
    //{
    //    var sw = Stopwatch.StartNew();
    //    Console.WriteLine("Thread {0}: {1}, Priority {2}",
    //                      Thread.CurrentThread.ManagedThreadId,
    //                      Thread.CurrentThread.ThreadState,
    //                      Thread.CurrentThread.Priority);
    //    do
    //    {
    //        Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
    //                          Thread.CurrentThread.ManagedThreadId,
    //                          sw.ElapsedMilliseconds / 1000.0);
    //        Thread.Sleep(500);
    //    } while (sw.ElapsedMilliseconds <= 5000);
    //    sw.Stop();
    //    Console.Read();
    //}
    #endregion
    #region ParameterizedThreadStart Type
    //public static void Main()
    //{
    //    var th = new Thread(ExecuteInForeground);
    //    th.Start(4500);
    //    Thread.Sleep(1000);
    //    Console.WriteLine("Main thread ({0}) exiting...",
    //                      Thread.CurrentThread.ManagedThreadId);
    //}
    //private static void ExecuteInForeground(Object obj)
    //{
    //    int interval;
    //    try
    //    {
    //        interval = (int)obj;
    //        Console.WriteLine("obj=" + interval);
    //    }
    //    catch (InvalidCastException)
    //    {
    //        interval = 5000;
    //    }
    //    var sw = Stopwatch.StartNew();
    //    Console.WriteLine("Thread {0}: {1}, Priority {2}",
    //                      Thread.CurrentThread.ManagedThreadId,
    //                      Thread.CurrentThread.ThreadState,
    //                      Thread.CurrentThread.Priority);
    //    do
    //    {
    //        Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
    //                          Thread.CurrentThread.ManagedThreadId,
    //                          sw.ElapsedMilliseconds / 1000.0);
    //        Thread.Sleep(500);
    //    } while (sw.ElapsedMilliseconds <= interval);
    //    sw.Stop();
    //}
    #endregion

    #region 线程常用属性
    //static Object obj = new Object();

    //public static void Main()
    //{
    //    ThreadPool.QueueUserWorkItem(ShowThreadInformation);
    //    var th1 = new Thread(ShowThreadInformation);
    //    th1.Start();
    //    var th2 = new Thread(ShowThreadInformation);
    //    th2.IsBackground = true;
    //    th2.Start();
    //    Thread.Sleep(500);
    //    ShowThreadInformation(null);
    //}
    //private static void ShowThreadInformation(Object state)
    //{
    //    lock (obj)
    //    {
    //        var th = Thread.CurrentThread;
    //        Console.WriteLine("Managed thread #{0}: ", th.ManagedThreadId);
    //        Console.WriteLine("   Thread Name:{0}", th.Name);
    //        Console.WriteLine("   Thread ThreadState:{0}", th.ThreadState);
    //        Console.WriteLine("   Background thread: {0}", th.IsBackground);
    //        Console.WriteLine("   Thread pool thread: {0}", th.IsThreadPoolThread);
    //        Console.WriteLine("   Priority: {0}", th.Priority);
    //        Console.WriteLine("   Culture: {0}", th.CurrentCulture.Name);
    //        Console.WriteLine("   UI culture: {0}", th.CurrentUICulture.Name);
    //        Console.WriteLine();
    //    }
    //}
    #endregion
    #region
    public static void Main()
    {
        var th = new Thread(ExecuteInForeground);
        th.IsBackground = true;
        th.Start();
        Thread.Sleep(1000);
        Console.WriteLine("Main thread ({0}) exiting...",
                          Thread.CurrentThread.ManagedThreadId);
    }
    private static void ExecuteInForeground()
    {
        var sw = Stopwatch.StartNew();
        Console.WriteLine("Thread {0}: {1}, Priority {2}",
                          Thread.CurrentThread.ManagedThreadId,
                          Thread.CurrentThread.ThreadState,
                          Thread.CurrentThread.Priority);
        do
        {
            Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
                              Thread.CurrentThread.ManagedThreadId,
                              sw.ElapsedMilliseconds / 1000.0);
            Thread.Sleep(500);
        } while (sw.ElapsedMilliseconds <= 5000);
        sw.Stop();
    }
    #endregion
}
