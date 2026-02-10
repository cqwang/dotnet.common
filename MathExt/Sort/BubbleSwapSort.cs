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
        /// 冒泡排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void BubbleSort(IList<DataItem> dataItems)
        {
            for (int i = 0; i < dataItems.Count; i++)
            {
                if (!AdjustBackward(dataItems, i))
                {
                    return;//没有发生交换，则已经有序。
                }

                logger.Increase(true, false, false);
            }
        }

        /// <summary>
        /// 向后遍历，相邻数据项非序则直接交换。
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="traversalIndex"></param>
        /// <returns></returns>
        private static bool AdjustBackward(IList<DataItem> dataItems, int traversalIndex)
        {
            bool hasSwap = false;
            int startIndexOfSortedBackward = dataItems.Count - traversalIndex;//后段有序，不必再比较
            for (int backwardIndex = 0; backwardIndex < startIndexOfSortedBackward - 1; backwardIndex++)
            {
                if (dataItems[backwardIndex + 1].Value < dataItems[backwardIndex].Value)
                {
                    Swap(dataItems, backwardIndex + 1, backwardIndex);
                    hasSwap = true;
                }

                logger.Increase(true, true);
            }

            return hasSwap;
        }
    }
}
