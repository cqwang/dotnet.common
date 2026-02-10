
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
        /// 偏移二分查找
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="targetItem"></param>
        /// <returns></returns>
        public static int BinaryOffsetFind(IList<DataItem> dataItems, DataItem targetItem)
        {
            var interval = BinaryFindOffsetInterval(dataItems, targetItem, 0, dataItems.Count - 1);
            if (interval == null)
            {
                return Invalid;
            }

            return BinaryFindIndex(dataItems, targetItem, interval.Value.Key, interval.Value.Value);
        }

        /// <summary>
        /// 查找偏移区间
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="targetItem"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        private static KeyValuePair<int, int>? BinaryFindOffsetInterval(IList<DataItem> dataItems, DataItem targetItem, int low, int high)
        {
            if (targetItem.Value < dataItems[low].Value)
            {
                return null;
            }
            logger.Increase(false, true);

            if (targetItem.Value > dataItems[high].Value)
            {
                return null;
            }
            logger.Increase(false, true);


            KeyValuePair<int, int>? interval = new KeyValuePair<int, int>(low, high);

            int possiblePosition = (int)(1.0 * dataItems.Count * (targetItem.Value - dataItems[low].Value) / (dataItems[high].Value - dataItems[low].Value)) + low;
            if (possiblePosition < 0 || possiblePosition > high)
            {
                return interval;
            }

            if (dataItems[possiblePosition].Value == targetItem.Value)
            {
                logger.Increase(false, true);
                interval = new KeyValuePair<int, int>(possiblePosition, possiblePosition);
            }
            else
            {
                double weight = 1.0 * dataItems.Count / (dataItems[high].Value - dataItems[low].Value);
                if (dataItems[possiblePosition].Value > targetItem.Value)
                {
                    int left = possiblePosition - (int)Math.Ceiling(Math.Abs(dataItems[possiblePosition].Value - targetItem.Value) * weight * 2);
                    if (left < 0 || left > high)
                    {
                        left = 0;
                    }

                    if (dataItems[left].Value <= targetItem.Value)
                    {
                        interval = new KeyValuePair<int, int>(left, possiblePosition);
                    }
                    else
                    {
                        interval = new KeyValuePair<int, int>(low, left);
                    }

                    logger.Increase(false, true);
                }
                else
                {
                    int right = possiblePosition + (int)Math.Ceiling(Math.Abs(dataItems[possiblePosition].Value - targetItem.Value) * weight * 2);
                    if (right < 0 || right > high)
                    {
                        right = high;
                    }

                    if (dataItems[right].Value >= targetItem.Value)
                    {
                        interval = new KeyValuePair<int, int>(possiblePosition, right);
                    }
                    else
                    {
                        interval = new KeyValuePair<int, int>(right, high);
                    }

                    logger.Increase(false, true);
                }

                logger.Increase(false, true);
            }

            return interval;
        }
    }
}
