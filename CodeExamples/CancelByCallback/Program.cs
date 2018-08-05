using System;
using System.Net;
using System.Threading;

namespace CancelByCallback
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            StartWebRequest(cts.Token);

            // cancellation will cause the web 
            // request to be cancelled
            Console.WriteLine("Press 'c' to cancel");
            if (Console.ReadKey(true).KeyChar == 'c')
            {
                cts.Cancel();
                Console.WriteLine("Press any key to exit.");
            }
        }

        static void StartWebRequest(CancellationToken token)
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += (s, e) => Console.WriteLine("Request completed.");

            // Cancellation on the token will 
            // call CancelAsync on the WebClient.
            token.Register(() =>
            {
                wc.CancelAsync();
                Console.WriteLine("Request cancelled!");
            });

            Console.WriteLine("Starting request.");
            wc.DownloadStringAsync(new Uri("http://www.contoso.com"));
        }
    }
}
