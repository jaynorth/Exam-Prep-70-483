using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LongTasksContinuewith
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "empty";
            Console.WriteLine("before tasks strings: {0}", s);

            var t1 = Task.Run(() =>
            {
                Task.Delay(1000);
                Thread.Sleep(2000);
                Console.WriteLine("task1");
                s = "task1 string";
            });

            var t2 = Task.Run(() =>
            {
                Task.Delay(1000);
                Console.WriteLine("task2");
                s = "task2 string";
            });

            Console.WriteLine("after tasks strings: {0}",s);

            //Task.WhenAll(t1,t2).ContinueWith((x) =>
            //{
            //    Task.Delay(5000);
            //    Console.WriteLine("tasks completed");
            //    //s = "continue";
                
            //    Console.WriteLine("after tasks completed strings: {0}", s);
                
            //});

            Console.WriteLine("before readkey");
            Console.ReadKey();

        }
    }
}
