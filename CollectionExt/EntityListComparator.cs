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
        /// 约定集合中元素不重复，比较两个集合中元素是否完全相同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool EqualElements<T>(ICollection<T> x, ICollection<T> y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }

            return x.All(p => y.Contains(p));
        }
    }
}
