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
		/// 珠排序
		/// </summary>
		/// <param name="items"></param>
		public static void BeadSort(IList<DataItem> dataItems, int singleBeadValue = 1)
        {
            int x = dataItems.Max(p => p.Value);
            int y = dataItems.Count;
            int[,] beads = new int[x, y];

            //将数据项拆分成若干个珠子，附加到坐标系中
            for (int i = 0; i < dataItems.Count; i++)
            {
                int beadXCount = dataItems[i].Value / singleBeadValue;
                for (int beadIndex = 0; beadIndex < beadXCount; beadIndex++)
                {
                    beads[beadIndex, i] = 1;//标识为珠子
                }
            }

            //相同x坐标，沿y轴自由落体
            for (int xIndex = 0; xIndex < x; xIndex++)
            {
                for (int yIndex = 1; yIndex < y; yIndex++)
                {
                    if (beads[xIndex, yIndex] == 1)
                    {
                        int index = yIndex - 1;
                        while (index >= 0 && beads[xIndex, index] == 0)
                        {
                            index--;
                        }

                        beads[xIndex, yIndex] = 0;
                        beads[xIndex, index + 1] = 1;
                    }
                }
            }

            //取值
            for (int yIndex = 0; yIndex < y; yIndex++)
            {
                int count = 0;
                for (int xIndex = 0; xIndex < x; xIndex++)
                {
                    if (beads[xIndex, yIndex] == 1)
                    {
                        count++;
                    }
                }

                dataItems[dataItems.Count - 1 - yIndex].Value = count * singleBeadValue;
            }
        }

    }
}
