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
        /// 大根堆排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void BigRootHeapSort(IList<DataItem> dataItems)
        {
            AdjustBigRootHeap(dataItems);
            for (int sortedIndex = dataItems.Count - 1; sortedIndex > 0; sortedIndex--)
            {
                Swap(dataItems, sortedIndex, 0);
                AdjustBigRootHeapItem(dataItems, 0, sortedIndex - 1);
            }
        }

        /// <summary>
        /// 将数据序列调整为大根堆
        /// </summary>
        /// <param name="dataItems"></param>
        private static void AdjustBigRootHeap(IList<DataItem> dataItems)
        {
            for (int index = dataItems.Count / 2 - 1; index >= 0; index--)
            {
                AdjustBigRootHeapItem(dataItems, index, dataItems.Count - 1);
            }
        }

        /// <summary>
        /// 调整指定数据项，以符合大根堆要求
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="unAdjustIndex"></param>
        /// <param name="maxIndex"></param>
        private static void AdjustBigRootHeapItem(IList<DataItem> dataItems, int unAdjustIndex, int maxIndex)
        {
            int loggerIndex = unAdjustIndex;

            DataItem unAdjustDataItem = dataItems[unAdjustIndex];//O(1)空间复杂度
            bool hasSwap = false;
            int leftChildIndex = (unAdjustIndex + 1) * 2 - 1;//先取左孩子
            while (leftChildIndex <= maxIndex)
            {
                //取左右孩子中较大的
                if (leftChildIndex < maxIndex && dataItems[leftChildIndex].Value < dataItems[leftChildIndex + 1].Value)
                {
                    leftChildIndex++;
                }

                logger.Increase(true, true);

                if (dataItems[leftChildIndex].Value <= unAdjustDataItem.Value)
                {
                    break;//若左右孩子都不大于父节点，则当前节点已经满足大根堆要求
                }
                else
                {
                    //否则取左右孩子中较大的与根节点交换，并继续向下调整
                    dataItems[unAdjustIndex] = dataItems[leftChildIndex];
                    logger.Increase(false, false, true, new KeyValuePair<int, int>(leftChildIndex, unAdjustIndex));
                    unAdjustIndex = leftChildIndex;
                    leftChildIndex = (unAdjustIndex + 1) * 2 - 1;
                    hasSwap = true;
                }

                logger.Increase(false, true);
            }

            if (hasSwap)
            {
                dataItems[unAdjustIndex] = unAdjustDataItem;
                logger.Increase(false, false, true, new KeyValuePair<int, int>(loggerIndex, unAdjustIndex));
            }
        }
    }
}
