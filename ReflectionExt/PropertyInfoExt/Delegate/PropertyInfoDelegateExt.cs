
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ReflectionExt
{
    public static class PropertyInfoDelegateExt
    {
        public static object CachedGetValue(this PropertyInfo propertyInfo, object obj)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            return GetterSetterFactory.CreateGetter(propertyInfo).Get(obj);
        }

        public static void CachedSetValue(this PropertyInfo propertyInfo, object obj, object value)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            GetterSetterFactory.CreateSetter(propertyInfo).Set(obj, value);
        }

        public static object CachedGetValue2(this PropertyInfo propertyInfo, object obj)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            return GetterSetterFactory2.CreateGetter(propertyInfo).Get(obj);
        }

        public static void CachedSetValue2(this PropertyInfo propertyInfo, object obj, object value)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            GetterSetterFactory2.CreateSetter(propertyInfo).Set(obj, value);
        }
    }
}
