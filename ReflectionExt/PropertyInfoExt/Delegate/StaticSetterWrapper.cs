
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ReflectionExt
{
    public class StaticSetterWrapper<TValue> : ISetValue
    {
        private Action<TValue> _setter;

        public StaticSetterWrapper(PropertyInfo propertyInfo)
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
            this._setter = (Action<TValue>)Delegate.CreateDelegate(typeof(Action<TValue>), null, methodInfo);
        }

        public void SetValue(TValue val)
        {
            this._setter(val);
        }

        void ISetValue.Set(object target, object val)
        {
            this._setter((TValue)val);
        }
    }
}
