using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ByteExtension
{
    public static partial class BitOperationExtension
    {
        /// <summary>
        /// 指定位序的二进制位取反
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index">从低位到高位的位序</param>
        /// <returns></returns>
        public static byte ReverseBitAt(this byte b, int index)
        {
            b ^= (byte)(1 << index);
            return b;
        }

        /// <summary>
        /// 指定位序的二进制位置零
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index">从低位到高位的位序</param>
        /// <returns></returns>
        public static byte ClearBitAt(this byte b, int index)
        {
            b &= (byte)((1 << 8) - 1 - (1 << index));
            return b;
        }

        /// <summary>
        /// 指定位序的二进制位置1
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index">从低位到高位的位序</param>
        /// <returns></returns>
        public static byte SetByteAt(this byte b, int index)
        {
            b |= (byte)(1 << index);
            return b;
        }

        /// <summary>
        /// 指定位序的二进制位是否为1
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index">从低位到高位的位序</param>
        /// <returns></returns>
        public static bool IsBitAt(this byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }
    }
}
