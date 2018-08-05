using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancelTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            var task = Task.Factory.StartNew(() =>
            {

                // Were we already canceled?
                ct.ThrowIfCancellationRequested();

                bool moreToDo = true;
                while (moreToDo)
                {

                    Thread.Sleep(100);
                    Console.WriteLine("Lopping...");

                    // Poll on this property if you have to do
                    // other cleanup before throwing.
                    if (ct.IsCancellationRequested)
                    {
                        // Clean up here, then...

                        // Canncel task  by exception
                        ct.ThrowIfCancellationRequested();

                        // Canncel task by return.
                        //break;
                    }

                }
            }, tokenSource2.Token); // Pass same token to StartNew.

            var cancelTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Wait one second.");
                Thread.Sleep(1000);
                tokenSource2.Cancel();

            });

            // Just continue on this thread, or Wait/WaitAll with try-catch:
            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Console.WriteLine(e.Message + " " + v.Message);
            }
            finally
            {
                tokenSource2.Dispose();
            }

            cancelTask.Wait();

            Console.WriteLine("Task status: {0}.", task.Status);
            Console.ReadKey();
        }
    }
}
