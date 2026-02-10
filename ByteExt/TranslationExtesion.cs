using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ByteExt
{
    public static partial class ByteExt
    {
        /// <summary>
        /// X表示十六进制格式，2表示输出两位
        /// </summary>
        private const string HexFormat = "X2";

        /// <summary>
        /// 转型为十六进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHex(this IEnumerable<byte> bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString(HexFormat));
            }

            return sb.ToString();
        }
    }
}
