using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class DataSlotLocalStorage
    {
        static LocalDataStoreSlot slot;

        /// <summary>
        /// 线程本地化存储 未命名槽
        /// </summary>
        public static void TestLocalDataStoreSlot()
        {
            //创建Slot
            slot = Thread.AllocateDataSlot();

            //设置TLS中的值
            Thread.SetData(slot, "defaultValue");

            //修改TLS的线程
            Thread th = new Thread(() =>
            {
                Thread.SetData(slot, "newValue");
                DisplayLocalDataStoreSlot();

            });

            th.Start();
            th.Join();
            DisplayLocalDataStoreSlot();
            Console.Read();
        }

        private static void DisplayLocalDataStoreSlot()
        {
            Console.WriteLine("{0} {1}", Thread.CurrentThread.ManagedThreadId, Thread.GetData(slot));
        }
    }
}
