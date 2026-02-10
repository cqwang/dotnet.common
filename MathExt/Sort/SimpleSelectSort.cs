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
        /// 简单选择排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void SimpleSelectSort(IList<DataItem> dataItems)
        {
            for (int traversalIndex = 0; traversalIndex < dataItems.Count; traversalIndex++)
            {
                //从未排序的后段中选择最小的数据项
                int minDataItemIndex = traversalIndex;
                for (int index = traversalIndex + 1; index < dataItems.Count; index++)
                {
                    if (dataItems[index].Value < dataItems[minDataItemIndex].Value)
                    {
                        minDataItemIndex = index;
                    }

                    logger.Increase(true, true);
                }

                //交换，保证前段有序
                if (minDataItemIndex != traversalIndex)
                {
                    Swap(dataItems, traversalIndex, minDataItemIndex);
                }
            }
        }
    }
}
