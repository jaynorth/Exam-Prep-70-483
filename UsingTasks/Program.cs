using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingTasks
{
    class Program
    {
        static void Main(string[] args)
        {

            /* Task with continuation */
            Console.WriteLine("/* Task with continuation */");
            Task<int> t = Task.Run(() =>
           {
               return 42;
           }).ContinueWith((i) =>
           {
               return i.Result * 2;
           });

            Console.WriteLine(t.Result);

            /* Task with continuation - different writing in 2 steps*/
            Console.WriteLine("/* Task with continuation - different writing in 2 steps*/");

            Task<int> t2 = Task.Run(() =>
            {
                return 52;
            });

            var t3 = t2.ContinueWith((i) =>
            {
                return i.Result * 2;
            });

            Console.WriteLine(t3.Result);

            /* Task with continuation - with options */
            Console.WriteLine("/* Task with continuation - with options */");

            Task<int> t4 = Task.Run(() =>
            {
                return 52;
            });

            t4.ContinueWith((i) =>
            {
                Console.WriteLine("Task Canceled");
                
            }, TaskContinuationOptions.OnlyOnCanceled); 

            t4.ContinueWith((i) =>
            {
                Console.WriteLine("Task Faulted");
    
            }, TaskContinuationOptions.OnlyOnFaulted);

            var CompletedTask = t4.ContinueWith((i) =>
            {
                Console.WriteLine("Task Completed");
           
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            CompletedTask.Wait();

            /* Task with Parent and children */
            Console.WriteLine("/* Task with Parent and children */");

            Task<int[]> parent = Task.Run(() =>
            {
                var results = new int[3];
                new Task(() => results[0] = 0,
                    TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = 1,
                    TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = 2,
                    TaskCreationOptions.AttachedToParent).Start();

                return results;
            });

            var FinalTask = parent.ContinueWith(
                parentTask =>
                {
                    foreach (int i in parentTask.Result)
                    {
                        Console.WriteLine(i);
                    }
                });
            FinalTask.Wait();

            /* Task with Parent and children - Using a Factory */
            Console.WriteLine("/* Task with Parent and children - Using a Factory */");

            Task<int[]> parentTask2 = Task.Run(() =>
            {
                var Results = new int[3];

                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously);

                tf.StartNew(() => Results[0] = 0);
                tf.StartNew(() => Results[1] = 1);
                tf.StartNew(() => Results[2] = 2);
                return Results;
            });

            var FinalTask2 = parentTask2.ContinueWith(
                pTask => {
                    foreach (var item in parentTask2.Result)
                    {
                        Console.WriteLine(item);
                    }
                });
            FinalTask2.Wait();

            /* Tasks Using WaitALL */
            Console.WriteLine("/* Tasks Using WaitALL */");

            Task[] tasks = new Task[3];
                tasks[0] = Task.Run(() => {
                    Thread.Sleep(1000);
                    Console.WriteLine("1");
                    return 1;
                });
                tasks[1] = Task.Run(() => {
                    Thread.Sleep(1000);
                    Console.WriteLine("2");
                    return 2;
                });
                tasks[2] = Task.Run(() => {
                    Thread.Sleep(1000);
                    Console.WriteLine("3");
                    return 3;
                }
                );
            Task.WaitAll(tasks);

            /* Tasks Using WhenAll and ContinueWith*/
            Console.WriteLine("/* Tasks Using WhenAll */ ");

            Task[] tasksWhenAll = new Task[3];
            tasksWhenAll[0] = Task.Run(() => {
                Thread.Sleep(1000);
                Console.WriteLine("1");
                return 1;
            });
            tasksWhenAll[1] = Task.Run(() => {
                Thread.Sleep(1000);
                Console.WriteLine("2");
                return 2;
            });
            tasksWhenAll[2] = Task.Run(() => {
                Thread.Sleep(1000);
                Console.WriteLine("3");
                return 3;
            }
            );
            var test = Task.WhenAll(tasksWhenAll);

            test.ContinueWith((x) =>
            {
                Console.WriteLine("Task When all reached, continued with");
            });

            Console.WriteLine("hello, Task when all waiting ...");

            Task waiting = Task.Run(() => Thread.Sleep(3000));

            Console.WriteLine("while waiting ...");

            waiting.Wait();
        }
    }
}
