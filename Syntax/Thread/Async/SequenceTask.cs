using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class SequenceTask
    {
        /// <summary>
        /// 有序执行多个线程
        /// </summary>
        public static void TestSequenceTaskWithContinue()
        {
            try
            {
                Task.Factory.StartNew<int>(delegate
                {
                    return Enumerable.Range(1, 100).Sum();
                }).ContinueWith(delegate (Task<int> task)
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine(task.Exception.GetBaseException().Message);
                    }
                    else
                    {
                        Console.WriteLine(task.Result);
                        Console.WriteLine("Runing Continue Task");
                    }
                });
            }
            catch (AggregateException exs)
            {
                foreach (var ex in exs.Flatten().InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            while (true)
            {
                Thread.Sleep(5000);
                Console.WriteLine(DateTime.Now);
            }
        }

        public static void TestSequenceTaskWithAwaiter()
        {
            Task<int> task1 = Task.Run<int>(() => { return Enumerable.Range(1, 100).Sum(); });
            var awaiter = task1.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Console.WriteLine("task1 finished");
                int result = awaiter.GetResult();
                Console.WriteLine(result);
            });
            Thread.Sleep(1000);
        }
    }
}
