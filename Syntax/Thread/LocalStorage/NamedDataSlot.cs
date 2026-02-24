using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class NamedDataSlotLocalStorage
    {
        /// <summary>
        /// 线程本地化存储 命名槽
        /// </summary>
        public static void TestNamedDataSlot()
        {
            // allocate some named data slots
            Thread.AllocateNamedDataSlot("ErrNo");
            Thread.AllocateNamedDataSlot("ErrSource");

            // create and start a second thread
            Thread t2 = new Thread(new ThreadStart(SetNamedDataSlot));
            t2.Name = "t2";
            t2.Start();

            // create a third thread
            Thread.Sleep(100);
            Thread t3 = new Thread(new ThreadStart(SetNamedDataSlot));
            t3.Name = "t3";
            t3.Start();

            // clean up the data slots
            Thread.FreeNamedDataSlot("ErrNo");
            Thread.FreeNamedDataSlot("ErrSource");
            Console.ReadKey();
        }

        /// <summary>
        /// the SetError method sets a random error number
        /// </summary>
        private static void SetNamedDataSlot()
        {
            Random r = new Random();
            Thread.SetData(Thread.GetNamedDataSlot("ErrNo"), r.Next(100));
            Thread.SetData(Thread.GetNamedDataSlot("ErrSource"), Thread.CurrentThread.Name);
            WriteNamedDataSlot();
        }

        /// <summary>
        /// the WriteError method writes error info from the current thread
        /// </summary>
        private static void WriteNamedDataSlot()
        {
            Console.WriteLine("Error number = " + Thread.GetData(Thread.GetNamedDataSlot("ErrNo")));
            Console.WriteLine("Error source = " + Thread.GetData(Thread.GetNamedDataSlot("ErrSource")));
            Console.WriteLine();
        }
    }
}
