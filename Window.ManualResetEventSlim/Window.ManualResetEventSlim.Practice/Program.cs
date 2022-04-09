using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Window.ManualResetEventSlim.Practice
{
    class Program
    {
        static void Main()
        {
            var intervals = new int[][] {
                    new int[] { 1, 5 },
                    new int[] { 3, 4 },
                    new int[] { 6,7 },
                    new int[] { 8, 9 }
                };
            var arrays = Handle(intervals);
            var builder = new StringBuilder();
            builder.Append("[");
            foreach (var array in arrays)
            {
                builder.Append($"[{array[0]},{array[1]}]");
            }
            builder.Append("]");
            Console.WriteLine(builder.ToString());
            Console.ReadLine();
        }
        static int[][] Handle(int[][] intervals)
        {
            var list = new List<int>();
            foreach (var arr in intervals.OrderBy(s => s[0]))
            {
                if (list.Count != 0 && arr[0] <= list[list.Count - 1])
                {
                    if (arr[1] >= list[list.Count - 1])
                        list[list.Count - 1] = arr[1];
                }
                else
                {
                    list.AddRange(arr);
                }
            }
            return list.Chunk(2).ToArray();
        }
        static void Merge(List<int[]> intervals)
        {
            for (int i = 0; i < intervals.Count; i++)
            {

            }
        }
        public static ref int GetArrayRef(int[] items, int index)
        {
            return ref items[index];
        }

        static int Add(ref int i)
        {
            i++;
            return i;
        }
        //实现
        static IEnumerable<int> Fibonacci(int n)
        {
            var list = new List<int>();
            if (n >= 1)
                list.Add(1);
            int c = 1, b = 0;
            for (int i = 2; i <= n; ++i)
            {
                int a = b;
                b = c;
                c = a + b;
                list.Add(c);
            }
            return list;
            /*
             loop1 a=0 b=1 c=1
             loop2 a=1 b=1 c=2
             loop3 a=1 b=2 c=3
             loop4 a=2 b=3 c=5
             loop4 a=3 b=5 c=8
             loop4 a=5 b=8 c=13
             loop5 a=8 b=13 c=21
             loop6 a=13 b=21 c=34
             .................
             */
        }

        static void PrintFooBar(int p)
        {
            System.Threading.ManualResetEventSlim mres = new System.Threading.ManualResetEventSlim(false); // initialize as unsignaled
            System.Threading.ManualResetEventSlim mres2 = new System.Threading.ManualResetEventSlim(true); // initialize as unsignaled

            Thread t1 = new Thread(() =>
                {
                    for (int i = 0; i < 20; i++)
                    {
                        mres2.Wait();
                        Console.WriteLine("foo");
                        mres2.Reset();
                        mres.Set();
                    }
                }
                );
            t1.Start();

            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    mres.Wait();//true
                    Console.WriteLine("bar");
                    mres.Reset();
                    mres2.Set();
                }
            });
            t2.Start();
        }
        // Demonstrates:
        //      ManualResetEventSlim construction
        //      ManualResetEventSlim.Wait()
        //      ManualResetEventSlim.Set()
        //      ManualResetEventSlim.Reset()
        //      ManualResetEventSlim.IsSet
        static void MRES_SetWaitReset()
        {
            System.Threading.ManualResetEventSlim mres1 = new System.Threading.ManualResetEventSlim(false); // initialize as unsignaled
            System.Threading.ManualResetEventSlim mres2 = new System.Threading.ManualResetEventSlim(false); // initialize as unsignaled
            System.Threading.ManualResetEventSlim mres3 = new System.Threading.ManualResetEventSlim(true);  // initialize as signaled

            // Start an asynchronous Task that manipulates mres3 and mres2
            var observer = Task.Factory.StartNew(() =>
            {
                mres1.Wait();
                Console.WriteLine("observer sees signaled mres1!");
                Console.WriteLine("observer resetting mres3...");
                mres3.Reset(); // should switch to unsignaled
                Console.WriteLine("observer signalling mres2");
                mres2.Set();
            });

            Console.WriteLine("main thread: mres3.IsSet = {0} (should be true)", mres3.IsSet);
            Console.WriteLine("main thread signalling mres1");
            mres1.Set(); // This will "kick off" the observer Task
            mres2.Wait(); // This won't return until observer Task has finished resetting mres3
            Console.WriteLine("main thread sees signaled mres2!");
            Console.WriteLine("main thread: mres3.IsSet = {0} (should be false)", mres3.IsSet);

            // It's good form to Dispose() a ManualResetEventSlim when you're done with it
            observer.Wait(); // make sure that this has fully completed
            mres1.Dispose();
            mres2.Dispose();
            mres3.Dispose();
        }

        // Demonstrates:
        //      ManualResetEventSlim construction w/ SpinCount
        //      ManualResetEventSlim.WaitHandle
        static void MRES_SpinCountWaitHandle()
        {
            // Construct a ManualResetEventSlim with a SpinCount of 1000
            // Higher spincount => longer time the MRES will spin-wait before taking lock
            System.Threading.ManualResetEventSlim mres1 = new System.Threading.ManualResetEventSlim(false, 1000);
            System.Threading.ManualResetEventSlim mres2 = new System.Threading.ManualResetEventSlim(false, 1000);

            Task bgTask = Task.Factory.StartNew(() =>
            {
                // Just wait a little
                Thread.Sleep(100);

                // Now signal both MRESes
                Console.WriteLine("Task signalling both MRESes");
                mres1.Set();
                mres2.Set();
            });

            // A common use of MRES.WaitHandle is to use MRES as a participant in 
            // WaitHandle.WaitAll/WaitAny.  Note that accessing MRES.WaitHandle will
            // result in the unconditional inflation of the underlying ManualResetEvent.
            WaitHandle.WaitAll(new WaitHandle[] { mres1.WaitHandle, mres2.WaitHandle });
            Console.WriteLine("WaitHandle.WaitAll(mres1.WaitHandle, mres2.WaitHandle) completed.");

            // Clean up
            bgTask.Wait();
            mres1.Dispose();
            mres2.Dispose();
        }
    }
}
