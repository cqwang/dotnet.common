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
        internal static IList<DataItem> bucketSortTempDataItems;

        /// <summary>
        /// 桶排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void BucketSort(IList<DataItem> dataItems)
        {
            //最大数据项
            DataItem maxDataItem = null;
            foreach (var item in dataItems)
            {
                if (maxDataItem == null || maxDataItem.Value < item.Value)
                {
                    maxDataItem = item;
                }
            }

            //值为索引，将数据项放入桶中
            bucketSortTempDataItems = new DataItem[maxDataItem.Value + 1];
            Dictionary<DataItem, int> loggerMap = new Dictionary<DataItem, int>();
            for (int i = 0; i < dataItems.Count; i++)
            {
                bucketSortTempDataItems[dataItems[i].Value] = dataItems[i];
                logger.Increase(true, false, true);
                loggerMap[dataItems[i]] = i;
            }

            int index = 0;
            foreach (var dataItem in bucketSortTempDataItems)
            {
                if (dataItem != null)
                {
                    logger.Increase(true, false, true, new KeyValuePair<int, int>(loggerMap[dataItem], index));
                    dataItems[index++] = dataItem;
                }
            }
        }

    }
}
