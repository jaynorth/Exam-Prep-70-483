using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingTasks
{
    class Program
    {
        static void Main(string[] args)
        {

            /* Task with continuation */
            Task<int> t = Task.Run(() =>
           {
               return 42;
           }).ContinueWith((i) =>
           {
               return i.Result * 2;
           });

            Console.WriteLine(t.Result);

            /* Task with continuation - different writing in 2 steps*/
            Task<int> t2 = Task.Run(() =>
            {
                return 52;
            });

            var t3 = t2.ContinueWith((i) =>
            {
                return i.Result * 2;
            });

            Console.WriteLine(t3.Result);
        }
    }
}
