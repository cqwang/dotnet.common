using Cqwang.BackEnd.CSharp.Algorithm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.MathExt
{
    public partial class FindAlgorithm
    {
        /// <summary>
        /// 通用二分查找
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="targetItem"></param>
        /// <returns></returns>
        public static int BinaryFind(IList<DataItem> dataItems, DataItem targetItem)
        {
            return BinaryFindIndex(dataItems, targetItem, 0, dataItems.Count - 1);
        }

        /// <summary>
        /// 二分查找目标数据项位置
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="targetItem"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        private static int BinaryFindIndex(IList<DataItem> dataItems, DataItem targetItem, int low, int high)
        {
            if (targetItem.Value < dataItems[low].Value)
            {
                return Invalid;
            }
            logger.Increase(false, true);

            if (targetItem.Value > dataItems[high].Value)
            {
                return Invalid;
            }
            logger.Increase(false, true);

            while (low <= high)
            {
                logger.Increase(true);

                int mid = (low + high) / 2;
                if (dataItems[mid].Value == targetItem.Value)
                {
                    logger.Increase(false, true);
                    return mid;
                }
                else if (dataItems[mid].Value > targetItem.Value)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }

                logger.Increase(false, true);
                logger.Increase(false, true);
            }

            return Invalid;
        }
    }
}
