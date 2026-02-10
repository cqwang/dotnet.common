
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ReflectionExt
{
    public class GetterSetter
    {
        public IGetValue GetValue { get; set; }

        public ISetValue SetValue { get; set; }
    }

    public class GetterSetterFactory
    {
        /// <summary>
        /// 存在两个问题：1.对键的哈希耗费较大，导致哈希查找太慢。2.键太多，且无法公用
        /// </summary>
        private static readonly ConcurrentDictionary<PropertyInfo, GetterSetter> map = new ConcurrentDictionary<PropertyInfo, GetterSetter>();

        public static IGetValue CreateGetter(PropertyInfo propertyInfo)
        {
            GetterSetter getterSetter;
            if (!map.TryGetValue(propertyInfo, out getterSetter))
            {
                getterSetter = new GetterSetter();
                map.TryAdd(propertyInfo, getterSetter);
            }
            if (getterSetter.GetValue == null)
            {
                getterSetter.GetValue = CreatePropertyGetterWrapper(propertyInfo);
            }
            return getterSetter.GetValue;
        }

        public static ISetValue CreateSetter(PropertyInfo propertyInfo)
        {
            GetterSetter getterSetter;
            if (!map.TryGetValue(propertyInfo, out getterSetter))
            {
                getterSetter = new GetterSetter();
                map.TryAdd(propertyInfo, getterSetter);
            }
            if (getterSetter.SetValue == null)
            {
                getterSetter.SetValue = CreatePropertySetterWrapper(propertyInfo);
            }
            return getterSetter.SetValue;
        }

        public static IGetValue CreatePropertyGetterWrapper(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            if (propertyInfo.CanRead == false)
            {
                throw new InvalidOperationException("属性不支持读操作。");
            }

            var methodInfo = propertyInfo.GetGetMethod(true);
            if (methodInfo.GetParameters().Length > 0)
            {
                throw new NotSupportedException("不支持构造索引器属性的委托。");
            }

            if (methodInfo.IsStatic)
            {
                var instanceType = typeof(StaticGetterWrapper<>).MakeGenericType(propertyInfo.PropertyType);
                return (IGetValue)Activator.CreateInstance(instanceType, propertyInfo);
            }
            else
            {
                var instanceType = typeof(GetterWrapper<,>).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType);
                return (IGetValue)Activator.CreateInstance(instanceType, propertyInfo);
            }
        }

        public static ISetValue CreatePropertySetterWrapper(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            if (propertyInfo.CanWrite == false)
            {
                throw new NotSupportedException("属性不支持写操作。");
            }

            var methodInfo = propertyInfo.GetSetMethod(true);
            if (methodInfo.GetParameters().Length > 1)
            {
                throw new NotSupportedException("不支持构造索引器属性的委托。");
            }

            if (methodInfo.IsStatic)
            {
                var instanceType = typeof(StaticSetterWrapper<>).MakeGenericType(propertyInfo.PropertyType);
                return (ISetValue)Activator.CreateInstance(instanceType, propertyInfo);
            }
            else
            {
                var instanceType = typeof(SetterWrapper<,>).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType);
                return (ISetValue)Activator.CreateInstance(instanceType, propertyInfo);
            }
        }
    }

    public class GetterSetterFactory2
    {
        /// <summary>
        /// 存在两个问题：1.对键的哈希耗费较大，导致哈希查找太慢。2.键太多，且无法公用
        /// </summary>
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, GetterSetter>> map = new ConcurrentDictionary<string, ConcurrentDictionary<string, GetterSetter>>();

        public static IGetValue CreateGetter(PropertyInfo propertyInfo)
        {
            var getterSetter = map.GetOrAdd(propertyInfo.DeclaringType.FullName).GetOrAdd(propertyInfo.Name);
            if (getterSetter.GetValue == null)
            {
                getterSetter.GetValue = CreatePropertyGetterWrapper(propertyInfo);
            }
            return getterSetter.GetValue;
        }

        public static ISetValue CreateSetter(PropertyInfo propertyInfo)
        {
            var getterSetter = map.GetOrAdd(propertyInfo.DeclaringType.FullName).GetOrAdd(propertyInfo.Name);
            if (getterSetter.SetValue == null)
            {
                getterSetter.SetValue = CreatePropertySetterWrapper(propertyInfo);
            }
            return getterSetter.SetValue;
        }

        public static IGetValue CreatePropertyGetterWrapper(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            if (propertyInfo.CanRead == false)
            {
                throw new InvalidOperationException("属性不支持读操作。");
            }

            var methodInfo = propertyInfo.GetGetMethod(true);
            if (methodInfo.GetParameters().Length > 0)
            {
                throw new NotSupportedException("不支持构造索引器属性的委托。");
            }

            if (methodInfo.IsStatic)
            {
                var instanceType = typeof(StaticGetterWrapper<>).MakeGenericType(propertyInfo.PropertyType);
                return (IGetValue)Activator.CreateInstance(instanceType, propertyInfo);
            }
            else
            {
                var instanceType = typeof(GetterWrapper<,>).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType);
                return (IGetValue)Activator.CreateInstance(instanceType, propertyInfo);
            }
        }

        public static ISetValue CreatePropertySetterWrapper(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            if (propertyInfo.CanWrite == false)
            {
                throw new NotSupportedException("属性不支持写操作。");
            }

            var methodInfo = propertyInfo.GetSetMethod(true);
            if (methodInfo.GetParameters().Length > 1)
            {
                throw new NotSupportedException("不支持构造索引器属性的委托。");
            }

            if (methodInfo.IsStatic)
            {
                var instanceType = typeof(StaticSetterWrapper<>).MakeGenericType(propertyInfo.PropertyType);
                return (ISetValue)Activator.CreateInstance(instanceType, propertyInfo);
            }
            else
            {
                var instanceType = typeof(SetterWrapper<,>).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType);
                return (ISetValue)Activator.CreateInstance(instanceType, propertyInfo);
            }
        }
    }
}
