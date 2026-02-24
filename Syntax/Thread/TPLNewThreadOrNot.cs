using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class TPLNewThreadOrNot
    {
        /// <summary>
        /// TPL是否开辟新线程
        /// </summary>
        /// <param name="dataList"></param>
        public static void TestTPLNewThreadOrNot(List<int> dataList)
        {
            Console.WriteLine("Main: " + Thread.CurrentThread.ManagedThreadId);
            ParallelOptions parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = -1
            };
            try
            {
                Parallel.ForEach(dataList, parallelOptions, data =>
                {
                    Thread.Sleep(100);//模拟耗时
                Console.WriteLine(data.ToString() + "Sub: " + Thread.CurrentThread.ManagedThreadId.ToString());
                });
            }
            catch(AggregateException ex)
            {
                foreach(var innerEx in ex.Flatten().InnerExceptions)
                {
                    //
                }
            }

            Console.ReadKey();
        }
    }
}
