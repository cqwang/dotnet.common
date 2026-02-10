using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.MathExt
{
    public static partial class MathExtension
    {
        /// <summary>
        /// 划分
        /// </summary>
        /// <param name="length"></param>
        /// <param name="subLength"></param>
        /// <returns></returns>
        public static int Partition(this int length, int subLength)
        {
            int subCount = length / subLength;
            return (length % subLength == 0) ? subCount : subCount + 1;
        }
    }
}
