using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ReflectionExt
{
    public static partial class ReflectionExtension
    {
        /// <summary>
        /// 获取指定类型的自定义特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, TAttribute> GetCustomAttributeMap<TAttribute>(this Type type) where TAttribute : Attribute
        {
            var properties = type.GetProperties();
            if (properties.IsNullOrEmpty())
            {
                return null;
            }

            var nameTagMap = new Dictionary<string, TAttribute>();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(TAttribute)) as TAttribute;
                if (attribute == null)
                {
                    continue;
                }

                nameTagMap.Add(property.Name, attribute);
            }
            return nameTagMap;
        }

        /// <summary>
        /// 获取每个枚举项的指定特性
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, TAttribute> LoadEnumAttributeMapFrom<TEnum, TAttribute>()
            where TAttribute : Attribute
        {
            var map = new Dictionary<int, TAttribute>();
            var fields = typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var field in fields)
            {
                var value = field.GetValue(null);
                //Console.WriteLine("值：{0} 类型：{1} 枚举背后类型：{2}", value, value.GetType(), Enum.GetUnderlyingType(value.GetType()));
                var attribute = Attribute.GetCustomAttribute(field, typeof(TAttribute)) as TAttribute;
                if (attribute == null)
                {
                    continue;
                }

                map.Add((int)value, attribute);
            }

            //另外一种方式
            //foreach (var value in Enum.GetValues(typeof(TEnum)))
            //{
            //    var field = value.GetType().GetField(value.ToString());
            //    if (field == null)
            //    {
            //        continue;
            //    }

            //    var attribute = Attribute.GetCustomAttribute(field, typeof(TAttribute)) as TAttribute;
            //    if (attribute == null)
            //    {
            //        continue;
            //    }

            //    map.Add((int)value, attribute);
            //}
            return map;
        }

        /// <summary>
        /// 获取枚举项的指定特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (null != field)
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(TAttribute)) as TAttribute;
                return attribute;
            }

            return null;
        }

        /// <summary>
        /// 获取枚举项描述特性的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (null != field)
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                return attribute.Description;
            }

            return null;
        }

        public static string GetTableName(this Type type)
        {
            if (!(type.GetCustomAttribute(typeof(TableAttribute)) is TableAttribute attribute))
            {
                throw new Exception($"类型{type.Name}未设置表名");
            }
            return attribute.Name;
        }
    }
}
