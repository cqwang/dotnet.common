using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class AsyncAwait
    {
        /// <summary>
        /// 异步
        /// </summary>
        public async void TestAsync()
        {
            double result = await GetValue(1234.5, 1.01);
            Console.WriteLine("Value is : " + result);
        }

        private Task<double> GetValue(double num1, double num2)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    num1 = num1 / num2;
                }
                return num1;
            });
        }

        /// <summary>
        /// 用async标记一个同步方法为异步的
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        private async Task ToAsync1(long goodsId)
        {
            Console.WriteLine("OK");
        }

        /// <summary>
        /// 使用Task.FromResult(0)创建一个返回指定值的异步线程，将同步方法转为异步
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        private async Task ToAsync2(long goodsId)
        {
            Console.WriteLine("OK");
            await Task.FromResult(0);
        }

        /// <summary>
        /// 任务链&并行测试
        /// </summary>
        private async void MultTask()
        {
            var pageTask = GetValue2(0,1).ContinueWith(async pageDataTask =>
            {
                if (pageDataTask.Result > 0)
                {
                    return await GetValue2(pageDataTask.Result, 1);
                }
                return 0;
            });

            var staticsTask = GetValue2(1,2).ContinueWith(staticsDataTask => staticsDataTask.Result);

            Task.WaitAll(pageTask, staticsTask);
            var stockLogDetails = pageTask.Result;
            var dailyStatics = staticsTask.Result;
        }

        private async Task<double> GetValue2(double num1, double num2)
        {
            return await Task.Run(() =>
            {
                for (var i = 0; i < 1000000; i++)
                {
                    num1 = num1 / num2;
                }
                return num1;
            });
        }
    }
}
