
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet.common.AppPoolExt
{
    public static class TaskExtension
    {
        /// <summary>
        /// 执行超时则返回主线程（CancelButGoOnDoing）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="timeout">毫秒</param>
        /// <returns></returns>
        public static T Invoke<T>(this Func<T> func, int timeout)
        {
            var token = new CancellationTokenSource(timeout);
            var task = Task.Run<T>(func, token.Token);
            task.Wait(timeout);
            if (task.IsCompleted)
            {
                return task.Result;
            }
            else
            {
                token.Cancel();
                return default(T);
            }
        }
    }
}
