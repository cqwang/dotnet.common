using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ThreadLocalLocalStorage
    {
        static ThreadLocal<string> local;
        /// <summary>
        /// 线程本地化存储
        /// </summary>
        public static void TestThreadLocal()
        {
            //创建ThreadLocal并提供默认值
            local = new ThreadLocal<string>(() => "defaultValue");

            //修改TLS的线程
            Thread.Sleep(100);
            Thread th = new Thread(() =>
            {
                local.Value = "newValue";
                DisplayThreadLocal();
            });

            th.Start();
            th.Join();
            DisplayThreadLocal();

            Console.Read();
        }

        static void DisplayThreadLocal()
        {
            Console.WriteLine("{0} {1}", Thread.CurrentThread.ManagedThreadId, local.Value);
        }
    }
}
