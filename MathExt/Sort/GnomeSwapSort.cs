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
        /// 地精排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void GnomeSort(IList<DataItem> dataItems)
        {
            for (int i = 0; i < dataItems.Count; i++)
            {
                if (!AdjustBackwardForward(dataItems, i))
                {
                    return;//没有发生交换，则已经有序。感觉有问题，应该不需要外面的循环
                }

                logger.Increase(true);
            }
        }

        /// <summary>
        /// 向后+向前遍历，相邻数据项非序则直接交换。
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="traversalIndex"></param>
        /// <returns></returns>
        private static bool AdjustBackwardForward(IList<DataItem> dataItems, int traversalIndex)
        {
            bool hasSwap = false;
            int endIndexOfSortedForward = traversalIndex - 1;//前段有序，不必再比较
            int startIndexOfSortedBackward = dataItems.Count - traversalIndex;//后段有序，不必再比较

            for (int backwardIndex = endIndexOfSortedForward + 1; backwardIndex < startIndexOfSortedBackward - 1; backwardIndex++)
            {
                if (dataItems[backwardIndex + 1].Value < dataItems[backwardIndex].Value)
                {
                    Swap(dataItems, backwardIndex + 1, backwardIndex);
                    for (int forwardIndex = backwardIndex; forwardIndex > endIndexOfSortedForward + 1; forwardIndex--)
                    {
                        if (dataItems[forwardIndex].Value < dataItems[forwardIndex - 1].Value)
                        {
                            Swap(dataItems, forwardIndex, forwardIndex - 1);
                        }

                        logger.Increase(true, true);
                    }

                    hasSwap = true;
                }

                logger.Increase(true, true);
            }

            return hasSwap;
        }
    }
}
