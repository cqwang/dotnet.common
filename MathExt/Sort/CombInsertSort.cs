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
        /// 梳排序
        /// </summary>
        /// <param name="dataItems"></param>
        public static void CombSort(IList<DataItem> dataItems)
        {
            for (int stepLength = (int)(dataItems.Count / 1.3); stepLength > 0; stepLength = (int)(stepLength / 1.3))
            {
                for (int startIndex = 0; startIndex < Math.Min(stepLength, dataItems.Count - stepLength); startIndex++)
                {
                    InsertSort(dataItems, startIndex, stepLength);
                }
            }
        }

    }
}
