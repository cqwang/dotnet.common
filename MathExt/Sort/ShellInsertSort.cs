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
		/// 希尔排序
		/// </summary>
		/// <param name="dataItems"></param>
		public static void ShellSort(IList<DataItem> dataItems)
        {
            //本例的步长序列的取法：第一个步长为数据序列的长度的一半，之后的步长为前一步长的一半，直至为1
            for (int stepLength = dataItems.Count / 2; stepLength > 0; stepLength /= 2)
            {
                //根据步长划分为多个子序列，分别执行直接插入排序
                for (int startIndex = 0; startIndex < Math.Min(stepLength, dataItems.Count - stepLength); startIndex++)
                {
                    InsertSort(dataItems, startIndex, stepLength);
                }
            }
        }

    }
}
