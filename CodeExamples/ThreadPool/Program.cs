using System;
using System.Threading;

namespace ThreadPool
{
    class Program
    {
        static void Main()
        {
            using (CancellationTokenSource source = new CancellationTokenSource())
            {
                Console.WriteLine("Start thread by thread pool.");

                System.Threading.ThreadPool.QueueUserWorkItem(DoSomeWork, source.Token);

                Console.WriteLine("Waiting...");
                Thread.Sleep(2500);

                Console.WriteLine("Cancel...");
                source.Cancel();

                Thread.Sleep(2500);
                Console.WriteLine("Done.");
            }
        }

        static void DoSomeWork(object obj)
        {
            CancellationToken token = (CancellationToken)obj;

            for (int i = 0; i < 100000; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("In iteration {0}, cancellation has been requested...",
                        i + 1);
                    // Perform cleanup if necessary.
                    //...
                    // Terminate the operation.
                    break;
                }
                // Simulate some work.
                Thread.SpinWait(500000);
            }
        }
    }
}
