using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ParallConcurrentBag
    {
        /// <summary>
        /// 并行
        /// </summary>
        public static void TestParallel()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            Parallel.ForEach(Partitioner.Create(0, 3000000, Environment.ProcessorCount), (i, state) =>
            {
                for (int m = i.Item1; m < i.Item2; m++)
                {
                    bag.Add(m);
                    if (bag.Count == 100)
                    {
                        state.Stop();//立即停止，100个直接退出。
                        //state.Break();//通知并行计算尽快的退出循环，break后程序还会迭代所有小于100的。
                        return;
                    }
                }
            });
        }
    }
}
