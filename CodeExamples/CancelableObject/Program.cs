using System;
using System.Threading;

namespace CancelableObjectPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;

                // User defined Class with its own method for cancellation
                var obj1 = new CancelableObject("1");
                var obj2 = new CancelableObject("2");
                var obj3 = new CancelableObject("3");

                // Register the object's cancel method with the token's
                // cancellation request.
                token.Register(() => obj1.Cancel());
                token.Register(() => obj2.Cancel());
                token.Register(() => obj3.Cancel());

                // Request cancellation on the token.
                cts.Cancel();
            }
        }
    }
}
