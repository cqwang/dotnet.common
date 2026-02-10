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
        /// 快速排序
        /// </summary>
        /// <param name="dataItems"></param>
        private static void QuickSort(IList<DataItem> dataItems)
        {
            RecursionPartition(dataItems, 0, dataItems.Count - 1);
        }

        /// <summary>
        /// 递归划分序列
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        private static void RecursionPartition(IList<DataItem> dataItems, int low, int high)
        {
            int partitionIndex = Partition(dataItems, low, high);

            Task task1 = null;
            Task task2 = null;

            if (low < partitionIndex - 1)
            {
                if (partitionIndex - low - 1 > ParallelMinDataCount)
                {
                    task1 = Task.Factory.StartNew(() => RecursionPartition(dataItems, low, partitionIndex - 1));
                }
                else
                {
                    RecursionPartition(dataItems, low, partitionIndex - 1);
                }
            }
            if (partitionIndex + 1 < high)
            {
                if (high - partitionIndex - 1 > ParallelMinDataCount)
                {
                    task2 = Task.Factory.StartNew(() => RecursionPartition(dataItems, partitionIndex + 1, high));
                }
                else
                {
                    RecursionPartition(dataItems, partitionIndex + 1, high);
                }
            }

            if (task1 != null)
            {
                task1.Wait();
            }
            if (task2 != null)
            {
                task2.Wait();
            }
        }

        /// <summary>
        /// 划分序列
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        private static int Partition(IList<DataItem> dataItems, int low, int high)
        {
            int loggerIndex = low;

            //本例采用序列第一个元素为主元
            DataItem primaryDataItem = dataItems[low];
            bool hasSwap = false;
            while (low < high)
            {
                //在后段中查找较小的数据项，移动到前段
                while (low < high && dataItems[high].Value >= primaryDataItem.Value)
                {
                    high--;
                    logger.Increase(true, true);
                }

                if (low != high)
                {
                    dataItems[low] = dataItems[high];
                    hasSwap = true;
                    logger.Increase(false, false, true, new KeyValuePair<int, int>(high, low));
                }

                //在前段中查找较大的数据项，移动到后段
                while (low < high && dataItems[low].Value <= primaryDataItem.Value)
                {
                    low++;
                    logger.Increase(true, true);
                }

                if (low != high)
                {
                    dataItems[high] = dataItems[low];
                    hasSwap = true;
                    logger.Increase(false, false, true, new KeyValuePair<int, int>(low, high));
                }
            }

            if (hasSwap)
            {
                dataItems[low] = primaryDataItem;
                logger.Increase(false, false, true, new KeyValuePair<int, int>(loggerIndex, low));
            }

            return low;
        }
    }
}
