using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper.Impl;

namespace AutoMapper
{
    public static partial class Mapper
    {
        /// <summary>
        /// 创建类型间的映射关系
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        public static void CreateNestedMapper(Type sourceType, Type destinationType)
        {
            var mapperedTypes = new HashSet<TypePair>();//创建映射的类型，避免类型链表结构自我嵌套时无限递归
            CreateNestedMapper(sourceType, destinationType, mapperedTypes);
        }

        /// <summary>
        /// 递归创建类型间的映射关系
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <param name="mapperedTypes"></param>
        public static void CreateNestedMapper(Type sourceType, Type destinationType, HashSet<TypePair> mapperedTypes)
        {
            var typePair = new TypePair(sourceType, destinationType);
            if (!mapperedTypes.Add(typePair))
            {
                return;
            }

            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProperties = destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var destinationProperty in destinationProperties)
            {
                var sourceProperty = sourceProperties.FirstOrDefault(prop => NameMatches(prop.Name, destinationProperty.Name));
                if (sourceProperty == null)
                {
                    continue;
                }

                var sourcePropertyType = sourceProperty.PropertyType;
                var destinationPropertyType = destinationProperty.PropertyType;
                if (destinationPropertyType.IsGenericType)
                {
                    var destinationGenericType = destinationPropertyType.GetGenericArguments()[0];
                    if (IsSystemType(destinationGenericType))
                    {
                        continue;
                    }

                    var sourceGenericType = sourcePropertyType.GetGenericArguments()[0];
                    CreateNestedMapper(sourceGenericType, destinationGenericType, mapperedTypes);
                }
                else
                {
                    if (IsSystemType(destinationPropertyType))
                    {
                        continue;
                    }

                    CreateNestedMapper(sourcePropertyType, destinationPropertyType, mapperedTypes);
                }
            }

            Mapper.CreateMap(sourceType, destinationType);
        }

        /// <summary>
        /// 过滤不需要创建映射关系的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static bool IsSystemType(Type type)
        {
            return type.FullName.StartsWith("System.");
        }

        private static bool NameMatches(string memberName, string nameToMatch)
        {
            return String.Compare(memberName, nameToMatch, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}
