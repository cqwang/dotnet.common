
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
        /// 判断数据是否在给定区间内
        /// </summary>
        /// <typeparam name="T">数据类型须实现接口IComparable</typeparam>
        /// <param name="t"></param>
        /// <param name="lowBound"></param>
        /// <param name="highBound"></param>
        /// <param name="includeLowBound"></param>
        /// <param name="includeHighBound"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T t, T lowBound, T highBound, bool includeLowBound, bool includeHighBound)
            where T : IComparable<T>
        {
            if (t == null)
                return false;

            return IsBetween(t.CompareTo(lowBound), t.CompareTo(highBound), includeLowBound, includeHighBound);
        }

        /// <summary>
        /// 判断数据是否在给定区间内
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="lowBound"></param>
        /// <param name="highBound"></param>
        /// <param name="includeLowBound"></param>
        /// <param name="includeHighBound"></param>
        /// <param name="comparer">实现IComparer<T>接口的比较器</param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T t, T lowBound, T highBound, bool includeLowBound, bool includeHighBound, IComparer<T> comparer)
        {
            if (comparer == null)
                return false;

            return IsBetween(comparer.Compare(t, lowBound), comparer.Compare(t, highBound), includeLowBound, includeHighBound);
        }

        /// <summary>
        /// 是否在给定区间内的判断逻辑
        /// </summary>
        /// <param name="compareToLow"></param>
        /// <param name="compareToHigh"></param>
        /// <param name="includeLowBound"></param>
        /// <param name="includeHighBound"></param>
        /// <returns></returns>
        private static bool IsBetween(int compareToLow, int compareToHigh, bool includeLowBound, bool includeHighBound)
        {
            return includeLowBound && compareToLow == 0
                || includeHighBound && compareToHigh == 0
                || compareToLow > 0 && compareToHigh < 0;
        }
    }
}
