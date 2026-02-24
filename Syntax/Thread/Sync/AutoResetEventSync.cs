using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class AutoResetEventSync
    {
        /// <summary>
        /// 线程同步 生产者消费者模型
        /// </summary>
        public static void TestAutoResetEvent_ProducterConsumer()
        {
            var autoResetEvent = new AutoResetEvent(false);
            var queue = new Queue<int>();
            var producterThread = new Thread(() =>
            {
                var rand = new Random();
                while (true)
                {
                    var value = rand.Next(100);
                    lock (queue)
                    {
                        queue.Enqueue(value);
                    }
                    Thread.Sleep(rand.Next(400, 1200));
                    Console.WriteLine("产生了数据{0}。", value);
                    autoResetEvent.Set();
                }
            });
            var consumerThread = new Thread(() =>
            {
                while (true)
                {
                    autoResetEvent.WaitOne();
                    int value = 0;
                    bool hasValue = true;
                    while (hasValue)
                    {
                        lock (queue)
                        {
                            hasValue = (queue.Count > 0);
                            if (hasValue)
                            {
                                value = queue.Dequeue();
                            }
                        }
                        Thread.Sleep(800);
                        Console.WriteLine("处理了数据{0}。", value);
                    }
                }
            });

            producterThread.Start();
            consumerThread.Start();
            Console.ReadLine();
        }
    }
}
