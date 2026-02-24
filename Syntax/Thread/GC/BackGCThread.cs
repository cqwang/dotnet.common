using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    /// <summary>
    /// 垃圾回收线程，单例执行
    /// </summary>
    public class BackGCThread
    {
        /// <summary>
        /// 状态
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// 线程状态标识
        /// </summary>
        public bool IsRunning
        {
            get
            {
                lock (locker)
                {
                    return isRunning;
                }
            }
        }

        /// <summary>
        /// 锁
        /// </summary>
        private object locker = new object();

        private BackGCThread() { }
        private readonly static BackGCThread instance = new BackGCThread();
        public static BackGCThread Current
        {
            get
            {
                return instance;
            }
        }

        protected void DoAction()
        {
            while (IsRunning)
            {
                try
                {
                    if (GC.WaitForFullGCApproach() == GCNotificationStatus.Succeeded)
                    {
                        //将执行垃圾回收
                        if (GC.WaitForFullGCComplete() == GCNotificationStatus.Succeeded)
                        {
                            //本次垃圾回收完成
                        }
                    }
                }
                catch (Exception e)
                {
                    //记录日志
                }
            }
        }
    }
}
