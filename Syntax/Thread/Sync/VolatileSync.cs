using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class VolatileSync
    {
        static volatile Int32 count;
        static Int32 value;
        /// <summary>
        /// 线程同步 volatile
        /// </summary>
        public static void TestVolatile()
        {
            Thread thread2 = new Thread(new ThreadStart(ReadVolatile));
            thread2.Start();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(20);
                Thread thread = new Thread(new ThreadStart(WriteVolatile));
                thread.Start();
            }
            Console.ReadKey();
        }

        private static void WriteVolatile()
        {
            Int32 temp = 0;
            for (int i = 0; i < 10000000; i++)
            {
                temp += 1;
            }

            value += temp;
            count = 1;
        }

        private static void ReadVolatile()
        {
            while (true)
            {
                if (count == 1)
                {
                    Console.WriteLine("累计计数:{1}", Thread.CurrentThread.ManagedThreadId, value);
                    count = 0;
                }
            }
        }
    }
}
