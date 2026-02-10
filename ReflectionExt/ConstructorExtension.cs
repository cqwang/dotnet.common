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
        /// 通过无参构造函数创建对象，支持单利类的私有构造函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
            var type = typeof(T);
            var constructorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            ConstructorInfo noParameterConstructorInfo = null;
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                var parameterInfos = constructorInfo.GetParameters();
                if (0 == parameterInfos.Length)
                {
                    noParameterConstructorInfo = constructorInfo;
                    break;
                }
            }
            if (null == noParameterConstructorInfo)
            {
                throw new NotSupportedException("No constructor without 0 parameter");
            }
            var instance = (T)noParameterConstructorInfo.Invoke(null);
            return instance;
        }
    }
}
