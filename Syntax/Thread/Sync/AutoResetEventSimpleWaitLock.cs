using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class SimpleWaitLockTest
    {
        public static void Test()
        {
            SimpleWaitLock slock = new SimpleWaitLock();
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(slock.Enter);
                thread.Name = "thread" + i.ToString();
                thread.Start();
            }
            Console.Read();
        }
    }


    public class SimpleWaitLock : IDisposable
    {
        private AutoResetEvent m_autoResetEvent = new AutoResetEvent(true);

        public void Enter()
        {
            m_autoResetEvent.WaitOne(); //阻止当前线程直到收到信号才执行

            Console.WriteLine("Thread {0} Enter;", Thread.CurrentThread.Name);
            Thread.Sleep(1000);
            Exit();
        }

        public void Exit()
        {
            Console.WriteLine("Thread {0} Exit;", Thread.CurrentThread.Name);
            m_autoResetEvent.Set();//将事件状态设置为终止状态，允许一个或多个等待线程继续

        }

        public void Dispose()
        {
            m_autoResetEvent.Close();
            Console.WriteLine("Thread {0} dispose;", Thread.CurrentThread.Name);
        }
    }
}
