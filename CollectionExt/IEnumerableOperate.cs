
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.CollectionExt
{
    public static partial class CollectionExtension
    {
        /// <summary>
        /// 在无序的对象列表中，求某个值类型属性最小的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T Min<T>(IEnumerable<T> items, Func<T, int> func) where T : class
        {
            if (items.IsNullOrEmpty())
            {
                return null;
            }

            int minValue = int.MaxValue;
            T minItem = default(T);
            foreach (var item in items)
            {
                var currentValue = func(item);
                if (minValue > currentValue)
                {
                    minItem = item;
                    minValue = currentValue;
                }
            }
            return minItem;
        }

        /// <summary>
        /// 在无序的对象列表中，求某个值类型属性最大的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T Max<T>(this IEnumerable<T> items, Func<T, decimal> func) where T : class
        {
            if (items.IsNullOrEmpty())
            {
                return null;
            }

            decimal maxValue = decimal.MinValue;
            T maxItem = default(T);
            foreach (var item in items)
            {
                var currentValue = func(item);
                if (maxValue < currentValue)
                {
                    maxItem = item;
                    maxValue = currentValue;
                }
            }
            return maxItem;
        }
    }
}
