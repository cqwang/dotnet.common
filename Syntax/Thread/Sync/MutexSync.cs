using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class MutexSync
    {
        /// <summary>
        /// 线程同步 Mutex
        /// </summary>
        public static void TestMutex()
        {
            bool created;
            //第一个参数定义了该互斥体的所有权是否应属于调用线程
            //第二个参数是互斥体名字，操作系统能识别该字符串，以此实现各进程之间的同步
            //第三个参数，如果系统中已存在该命名的互斥体返回false，否则返回true
            Mutex mutex = new Mutex(false, "TestMutex", out created);
            mutex = Mutex.OpenExisting("TestMutex");//打开系统中已存在的互斥体
            if (mutex.WaitOne(500))//500为等待超时时间
            {
                try
                {
                    //synchronized region
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}
