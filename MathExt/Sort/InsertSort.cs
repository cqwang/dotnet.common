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
        /// 直接插入排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void InsertSort(IList<DataItem> dataItems)
        {
            InsertSort(dataItems, 0, 1);
        }

        /// <summary>
        /// 直接插入排序
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="unAdjustIndex"></param>
        /// <param name="stepLength"></param>
        private static void InsertSort(IList<DataItem> dataItems, int startIndex, int stepLength)
        {
            for (int index = startIndex + stepLength; index < dataItems.Count; index += stepLength)
            {
                InsertSortAdjust(dataItems, stepLength, index);
            }
        }

        /// <summary>
        /// 对于指定步长的子序列，调整指定位序的数据项，保证前段有序。
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="stepLength"></param>
        /// <param name="unsortedIndex"></param>
        private static void InsertSortAdjust(IList<DataItem> dataItems, int stepLength, int unsortedIndex)
        {
            if (dataItems[unsortedIndex - stepLength].Value > dataItems[unsortedIndex].Value)
            {
                DataItem temp = dataItems[unsortedIndex];//O(1)空间复杂度

                //逐个后移，查找合适的位置
                int j = unsortedIndex;
                while (j >= stepLength && dataItems[j - stepLength].Value > temp.Value)
                {
                    dataItems[j] = dataItems[j - stepLength];
                    logger.Increase(true, true, true, new KeyValuePair<int, int>(j - stepLength, j));
                    j -= stepLength;
                }
                dataItems[j] = temp;
                logger.Increase(false, true, true, new KeyValuePair<int, int>(unsortedIndex, j));
            }

            logger.Increase(true, true);
        }

    }
}
