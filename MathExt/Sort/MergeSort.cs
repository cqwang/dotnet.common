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
        private static IList<DataItem> tempSortedDataItems;

        /// <summary>
        /// 归并排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void MergeSort(IList<DataItem> dataItems)
        {
            tempSortedDataItems = new DataItem[dataItems.Count];
            MergeSort(dataItems, 0, dataItems.Count);
        }

        /// <summary>
        /// 归并排序
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        private static void MergeSort(IList<DataItem> dataItems, int startIndex, int length)
        {
            if (startIndex + 1 < length)
            {
                int mid = (startIndex + length) / 2;

                Task task1 = null;
                if (mid - startIndex > ParallelMinDataCount)
                {
                    task1 = Task.Factory.StartNew(() => MergeSort(dataItems, startIndex, mid));
                }
                else
                {
                    MergeSort(dataItems, startIndex, mid);
                }

                Task task2 = null;
                if (length - mid > ParallelMinDataCount)
                {
                    task2 = Task.Factory.StartNew(() => MergeSort(dataItems, mid, length));
                }
                else
                {
                    MergeSort(dataItems, mid, length);
                }

                if (task1 != null)
                {
                    task1.Wait();
                }
                if (task2 != null)
                {
                    task2.Wait();
                }

                Merge(dataItems, startIndex, mid, length);
            }
        }

        /// <summary>
        /// 合并两个相邻的有序子序列
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="leftSubsequenceStartIndex"></param>
        /// <param name="rightSubsequenceStartIndex"></param>
        /// <param name="nextSubsequenceStartIndex"></param>
        private static void Merge(IList<DataItem> dataItems, int leftSubsequenceStartIndex, int rightSubsequenceStartIndex, int nextSubsequenceStartIndex)
        {
            //检查是否已经有序
            bool isSorted = dataItems[rightSubsequenceStartIndex - 1].Value < dataItems[rightSubsequenceStartIndex].Value;
            logger.Increase(true, true);
            if (isSorted)
            {
                return;
            }

            int leftStartIndex = leftSubsequenceStartIndex;
            int rightStartIndex = rightSubsequenceStartIndex;
            int index = leftSubsequenceStartIndex;

            //有序合并到临时序列
            while (leftStartIndex < rightSubsequenceStartIndex && rightStartIndex < nextSubsequenceStartIndex)
            {
                if (dataItems[leftStartIndex].Value < dataItems[rightStartIndex].Value)
                {
                    tempSortedDataItems[index++] = dataItems[leftStartIndex++];
                    logger.Increase(true, true, true, new KeyValuePair<int, int>(leftStartIndex - 1, index - 1));
                }
                else
                {
                    tempSortedDataItems[index++] = dataItems[rightStartIndex++];
                    logger.Increase(true, true, true, new KeyValuePair<int, int>(rightStartIndex - 1, index - 1));
                }
            }

            while (leftStartIndex < rightSubsequenceStartIndex)
            {
                tempSortedDataItems[index++] = dataItems[leftStartIndex++];
                logger.Increase(true, false, true, new KeyValuePair<int, int>(leftStartIndex - 1, index - 1));
            }

            while (rightStartIndex < nextSubsequenceStartIndex)
            {
                tempSortedDataItems[index++] = dataItems[rightStartIndex++];
                logger.Increase(true, false, true, new KeyValuePair<int, int>(rightStartIndex - 1, index - 1));
            }

            //复制到源序列
            for (int i = leftSubsequenceStartIndex; i < index; i++)
            {
                dataItems[i] = tempSortedDataItems[i];
                logger.Increase(true, false, true);
            }
        }

    }
}
