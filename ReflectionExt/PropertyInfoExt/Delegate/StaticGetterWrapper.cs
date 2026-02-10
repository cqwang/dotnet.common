
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ReflectionExt
{
    public class StaticGetterWrapper<TValue> : IGetValue
    {
        private Func<TValue> _getter;

        public StaticGetterWrapper(PropertyInfo propertyInfo)
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
            this._getter = (Func<TValue>)Delegate.CreateDelegate(typeof(Func<TValue>), null, methodInfo);
        }

        public TValue GetValue()
        {
            return this._getter();
        }

        object IGetValue.Get(object target)
        {
            return this._getter();
        }
    }
}
