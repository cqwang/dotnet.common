using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class SemaphoreSync
    {
        private static SemaphoreSlim _sem = new SemaphoreSlim(3);
        /// <summary>
        /// 线程同步 信号量
        /// </summary>
        public static void TestSemaphore()
        {
            for (int i = 1; i <= 5; i++)
            {
                new Thread(Enter).Start(i);
            }
            Console.ReadLine();
        }

        private static void Enter(object id)
        {
            Console.WriteLine(id + " 开始排队...");
            _sem.Wait();
            Console.WriteLine(id + " 开始执行！");
            Thread.Sleep(1000 * (int)id);
            Console.WriteLine(id + " 执行完毕，离开！");
            _sem.Release();
        }
    }
}
