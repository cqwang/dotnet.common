using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ThreadStaticLocalStorage
    {
        static int value1 = 0;

        [ThreadStatic]
        static int value2 = 0;

        /// <summary>
        /// 线程本地化存储 静态变量
        /// </summary>
        /// <param name="args"></param>
        public static void TestThreadStatic(string[] args)
        {
            Task task1 = new Task(() =>
            {
                Console.WriteLine("value1:" + value1);
                value1 = 1;
            });
            task1.Start();

            Task task2 = new Task(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine("value1:" + value1);
                value1 = 2;
            });
            task2.Start();

            Task task3 = new Task(() =>
            {
                Console.WriteLine("value2:" + value2);
                value2 = 3;

            });
            task3.Start();

            Task task4 = new Task(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine("value2:" + value2);
                value2 = 4;
            });
            task4.Start();

            Task.WaitAll(task1, task2, task3, task4);
            Console.WriteLine("Over");
            Console.Read();
        }
    }
}
