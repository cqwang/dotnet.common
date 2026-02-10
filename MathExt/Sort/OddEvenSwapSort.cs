using Cqwang.BackEnd.CSharp.Algorithm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.MathExt
{
    public partial class SortAlgorithm
    {
        /// <summary>
        /// 奇偶排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void OddEvenSort(IList<DataItem> dataItems)
        {
            bool hasSwapOdd = true;
            bool hasSwapEven = true;
            while (hasSwapOdd || hasSwapEven)
            {
                hasSwapOdd = AdjustNext(dataItems, 1);
                if (!hasSwapOdd && !hasSwapEven)
                {
                    break;
                }

                hasSwapEven = AdjustNext(dataItems, 0);
            }
        }

        /// <summary>
        /// 和相邻的下一个数据项比较，非序则交换。
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="unAdjustIndex"></param>
        /// <returns></returns>
        private static bool AdjustNext(IList<DataItem> dataItems, int startIndex)
        {
            bool hasSwap = false;
            while (startIndex < dataItems.Count - 1)
            {
                if (dataItems[startIndex].Value > dataItems[startIndex + 1].Value)
                {
                    Swap(dataItems, startIndex + 1, startIndex);
                    hasSwap = true;
                }

                startIndex += 2;

                logger.Increase(true, true);
            }

            return hasSwap;
        }

    }
}
