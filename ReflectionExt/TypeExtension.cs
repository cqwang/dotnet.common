using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ReflectionExt
{
    public static partial class ReflectionExtension
    {
        /// <summary>
        /// 缓存所有的数值类型
        /// </summary>
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>()
        {
            typeof(SByte),typeof(Int16),typeof(Int32),typeof(Int64),
            typeof(UInt16),typeof(UInt32),typeof(UInt64),
            typeof(Single),typeof(Double),typeof(Decimal)
        };

        /// <summary>
        /// 是否是值类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static bool IsNumericType(this Type dataType)
        {
            if (dataType == null)
            {
                throw new ArgumentNullException("dataType");
            }

            return NumericTypes.Contains(dataType);
        }


        /// <summary>
        /// 获取程序集中的类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        /// <summary>
        /// 获取某一基类的所有实现类名称和类型映射
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="excepts">要排除的类型名称</param>
        /// <returns></returns>
        public static Dictionary<string, Type> GetTypeMap(this Type baseType, params string[] excepts)
        {
            var map = new Dictionary<string, Type>();

            var types = baseType.Assembly.GetTypes();
            foreach (var type in types)
            {
                if (baseType.IsAssignableFrom(type) && type.Name != baseType.Name)
                {
                    if (!excepts.IsNullOrEmpty() && excepts.Contains(type.Name))
                    {
                        continue;
                    }
                    map.Add(type.Name, type);
                }
            }

            return map;
        }
    }
}
