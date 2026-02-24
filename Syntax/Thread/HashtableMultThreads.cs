using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class HashtableMultThreads
    {
        public static void Test()
        {
            table = new Hashtable(10000);
            //table = Hashtable.Synchronized(new Hashtable(1000000));
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoWriteTask1), 5000);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoWriteTask2), 5000);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoWriteTask3), 5000);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoReadTask1), 5000);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoReadTask2), 5000);

            while (true)
            {
                Thread.Sleep(2000);
                Console.WriteLine(table.Count);
            }
        }



        private static Hashtable table = null;

        private static void DoWriteTask1(object obj)
        {
            for (int i = 0; i < int.Parse(obj.ToString()); i++)
            {
                Thread.Sleep(10);//表示操作耗时
                table.Add(i, i);
            }

            Console.WriteLine("DoWriteTask1: count {0}", table.Count);
        }

        private static void DoWriteTask2(object obj)
        {
            int temp = int.Parse(obj.ToString());
            for (int i = temp; i < 2 * temp; i++)
            {
                Thread.Sleep(10);//表示操作耗时
                table.Add(i, i);
            }

            Console.WriteLine("DoWriteTask2: count {0}", table.Count);
        }

        private static void DoWriteTask3(object obj)
        {
            int temp = int.Parse(obj.ToString());
            for (int i = temp; i < 2 * temp; i++)
            {
                Thread.Sleep(5);//表示操作耗时
                table.Remove(i);
            }

            Console.WriteLine("DoWriteTask3: count {0}", table.Count);
        }

        private static void DoThreadSafeWriteTask1(object obj)
        {
            lock (table.SyncRoot)
            {
                for (int i = 0; i < int.Parse(obj.ToString()); i++)
                {
                    Thread.Sleep(10);//表示操作耗时
                    table.Add(i, i);
                }
            }

            Console.WriteLine("DoThreadSafeWriteTask1: count {0}", table.Count);
        }

        private static void DoReadTask1(object obj)
        {
            object value;
            int temp = int.Parse(obj.ToString());
            for (int i = temp; i < 2 * temp; i++)
            {
                Thread.Sleep(4);//表示操作耗时
                if (table.ContainsKey(i))
                    value = table[i];
            }

            Console.WriteLine("DoReadTask1: count {0}", table.Count);
        }

        private static void DoReadTask2(object obj)
        {
            object value;
            int temp = int.Parse(obj.ToString());
            for (int i = temp; i < 2 * temp; i++)
            {
                Thread.Sleep(4);//表示操作耗时
                if (table.ContainsKey(i))
                    value = table[i];
            }

            Console.WriteLine("DoReadTask2: count {0}", table.Count);
        }

        private static void DoThreadSafeReadTask1(object obj)
        {
            lock (table.SyncRoot)
            {
                foreach (object item in table.Values)
                {
                    //
                }

                Console.WriteLine("DoThreadSafeReadTask1: count {0}", table.Count);
            }
        }
    }
}
