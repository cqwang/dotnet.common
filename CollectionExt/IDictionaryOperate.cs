
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.CollectionExt
{
    public static partial class CollectionExtension
    {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : new()
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                value = new TValue();
                dict.Add(key, value);
            }
            return value;
        }

        /// <summary>
        /// 添加键值对，若键已经存在则不添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            bool flag = false;
            if (!dict.ContainsKey(key))
            {
                flag = true;
                dict.Add(key, value);
            }

            return flag;
        }

        public static void AddItem<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> dict, TKey key, TValue value)
        {
            var set = dict.GetOrAdd(key);
            set.Add(value);
        }

        public static void AddItem<TKey, TValue>(this IDictionary<TKey, List<TValue>> dict, TKey key, TValue value)
        {
            var list = dict.GetOrAdd(key);
            list.Add(value);
        }

        public static void AddItems<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> dict, TKey key, ICollection<TValue> values)
        {
            if (values.IsNullOrEmpty())
                return;

            var set = dict.GetOrAdd(key);
            foreach (TValue value in values)
            {
                set.Add(value);
            }
        }

        public static void AddItems<TOuterKey, TInnerKey, TValue>(this Dictionary<TOuterKey, Dictionary<TInnerKey, TValue>> dict, TOuterKey outerKey, Func<TValue, TInnerKey> getInnerKey, ICollection<TValue> values)
        {
            if (values.IsNullOrEmpty())
                return;

            var map = dict.GetOrAdd(outerKey);
            foreach (TValue value in values)
            {
                map.TryAdd(getInnerKey(value), value);
            }
        }

        public static Dictionary<TKey, List<TValue>> ToLookupMap<TKey, TValue>(this IEnumerable<TValue> values, Func<TValue, TKey> getKey)
        {
            if (values == null || !values.Any())
                return null;

            var map = new Dictionary<TKey, List<TValue>>();
            foreach (var value in values)
            {
                var key = getKey(value);
                map.AddItem(key, value);
            }
            return map;
        }
    }
}
