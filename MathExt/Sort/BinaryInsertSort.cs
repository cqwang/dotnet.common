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
        /// 折半插入排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void BinaryInsertSort(IList<DataItem> dataItems)
        {
            for (int index = 1; index < dataItems.Count; index++)
            {
                if (dataItems[index - 1].Value > dataItems[index].Value)
                {
                    DataItem temp = dataItems[index];//O(1)空间复杂度

                    //折半查找合适的位置，逐个后移，保证前段有序
                    int position = BinaryFindPosition(dataItems, temp, 0, index - 1);
                    for (int j = index; j > position && j > 0; j--)
                    {
                        dataItems[j] = dataItems[j - 1];
                        logger.Increase(true, false, true, new KeyValuePair<int, int>(j - 1, j));
                    }
                    dataItems[position] = temp;
                    logger.Increase(false, false, true, new KeyValuePair<int, int>(index, position));
                }

                logger.Increase(true, true);
            }
        }

        /// <summary>
        /// 折半查找数据项要插入的位置
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="dataItem"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        private static int BinaryFindPosition(IList<DataItem> dataItems, DataItem dataItem, int low, int high)
        {
            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (dataItems[mid].Value > dataItem.Value)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }

                logger.Increase(true, true);
            }

            return low;
        }

    }
}
