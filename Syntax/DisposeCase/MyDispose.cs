using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    /// <summary>
    /// 非托管资源的清理方案
    /// </summary>
    public class MyDispose : IDisposable
    {
        private IntPtr _handler;//非托管资源句柄

        protected bool disposed = false;//是否已被清理

        /// <summary>
        /// 清理对象资源，供子类重写
        /// </summary>
        /// <param name="disposing">是否需要一并清理托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    #region 清理托管资源
                    #endregion
                }

                if (_handler != IntPtr.Zero)
                {
                    #region 清理非托管资源
                    #endregion
                }

                _handler = IntPtr.Zero;
                disposed = true;
            }
        }

        /// <summary>
        /// 实现接口方法，在程序调用时清理资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);//因为已经清理完毕，故请求系统不要调用析构函数
        }

        /// <summary>
        /// 基于编程习惯，增加该方法
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// 析构函数，在垃圾回收前自动调用
        /// </summary>
        ~MyDispose()
        {
            Dispose(false);
        }
    }
}
