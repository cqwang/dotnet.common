
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
        /// 随机枚举项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T NextEnum<T>(this Random random)
            where T : struct
        {
            Type type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException();

            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }

        /// <summary>
        /// 随机布尔值
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static bool NetBool(this Random random)
        {
            return random.NextDouble() > 0.5;
        }

        /// <summary>
        /// 随机字节数组
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">字节数组长度</param>
        /// <returns></returns>
        public static byte[] NextBytes(this Random random, int length)
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);
            return bytes;
        }

        /// <summary>
        /// 随机整型
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static int NetInt32(this Random random)
        {
            return BitConverter.ToInt32(random.NextBytes(4), 0);
        }

        /// <summary>
        /// 获取随机高精度整型数
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Int64 NextLong(this Random random)
        {
            return BitConverter.ToInt64(random.NextBytes(8), 0);
        }

        /// <summary>
        /// 随机时间
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue">最小时间</param>
        /// <param name="maxValue">最大时间</param>
        /// <returns></returns>
        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
        {
            var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
            return new DateTime(ticks);
        }

        /// <summary>
        /// 随机时间
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static DateTime NextDateTime(this Random random)
        {
            return NextDateTime(random, DateTime.MinValue, DateTime.MaxValue);
        }
    }
}
