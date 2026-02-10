
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ReflectionExt
{
    public class GetterWrapper<TTarget, TValue> : IGetValue
    {
        private Func<TTarget, TValue> _getter;

        public GetterWrapper(PropertyInfo propertyInfo)
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
            this._getter = (Func<TTarget, TValue>)Delegate.CreateDelegate(typeof(Func<TTarget, TValue>), null, methodInfo);
        }

        public TValue GetValue(TTarget target)
        {
            return this._getter(target);
        }
        object IGetValue.Get(object target)
        {
            return this._getter((TTarget)target);
        }
    }
}
