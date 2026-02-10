
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ReflectionExt
{
    public interface ISetValue
    {
        void Set(object target, object val);
    }
}
