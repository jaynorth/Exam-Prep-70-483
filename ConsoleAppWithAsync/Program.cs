using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWithAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            /* https://ardalis.com/better-performance-from-async-operations */
            /* Enables console app to use async  */
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            // all of my async code and await calls go here
            var partyStatus = new PartyStatus();

            var timer = Stopwatch.StartNew();

            partyStatus.InvitesSent = await SendInvites();
            partyStatus.FoodCost = await OrderFood();
            partyStatus.IsHouseClean = await CleanHouse();

            Console.WriteLine($"Elapsed time: {timer.ElapsedMilliseconds}ms");
        }

        public static async Task<int> SendInvites()
        {
            await Task.Delay(2000);

            return 100;
        }
        public static async Task<decimal> OrderFood()
        {
            await Task.Delay(2000);

            return 123.23m;
        }
        public static async Task<bool> CleanHouse()
        {
            await Task.Delay(2000);

            return true;
        }
    }

    internal class PartyStatus
    {
        public PartyStatus()
        {
        }

        public int InvitesSent { get; internal set; }
        public decimal FoodCost { get; internal set; }
        public bool IsHouseClean { get; internal set; }
    }
}
