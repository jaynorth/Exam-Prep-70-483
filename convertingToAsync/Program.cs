using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace convertingToAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

    
        }

        static async Task MainAsync(string[] args)
        {
            /* synchrononous method here */
            DoSync.DoStuff();

            
            // all of my async code and await calls go here
            DoASync.DoStuffAsync();
            Console.WriteLine("waiting for async");
            Console.ReadKey();
        }
    }

    public static class DoSync
    {
        public static void DoStuff()
        {
            Task.Run(() => Thread.Sleep(2000));
            var t2 = Task.Run(() => {
                Thread.Sleep(2000);
                return 5;
            });
            Console.WriteLine("do stuff synchronously");
            Console.WriteLine(t2.Result);
        }
    }

    public static class DoASync
    {
        public static async void DoStuffAsync()
        {
            await Task.Run(() => Thread.Sleep(2000)); //difference here is it (a)waits 2 seconds but not block ui thread.
            var t2 = await Task.Run(() => {
                Thread.Sleep(2500);
                return 9;
            });
            var t3 = Task.Run(() => {
                Thread.Sleep(500);
                return 11;
            });
            Console.WriteLine("do stuff Async");
            Console.WriteLine(t2);
            Console.WriteLine(t3.Result);
        }
    }
}
