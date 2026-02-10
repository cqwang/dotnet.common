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
        /// 顺序查找
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="targetItem"></param>
        /// <returns></returns>
        public static int SequenceFind(IList<DataItem> dataItems, DataItem targetItem)
        {
            return SequenceFindIndex(dataItems, targetItem, 0, dataItems.Count - 1);
        }

        /// <summary>
        /// 顺序查找目标数据项位置
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="targetItem"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        private static int SequenceFindIndex(IList<DataItem> dataItems, DataItem targetItem, int low, int high)
        {
            for (int i = low; i <= high; i++)
            {
                logger.Increase(true, true);
                if (dataItems[i].Value == targetItem.Value)
                {
                    return i;
                }
            }

            return Invalid;
        }
    }
}
