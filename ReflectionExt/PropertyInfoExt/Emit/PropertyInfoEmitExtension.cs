using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ReflectionExt
{
    public static class PropertyInfoEmitExtension
    {
        public delegate void SetValueDelegate(object target, object arg);

        /// <summary>
        /// Emit创建弱类型委托
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static SetValueDelegate CreateSetter(this PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            if (!property.CanWrite)
                return null;

            var setMethod = property.GetSetMethod(true);

            DynamicMethod dynamicMethod = new DynamicMethod("PropertySetter", null, new Type[] { typeof(object), typeof(object) }, property.DeclaringType, true);
            ILGenerator generator = dynamicMethod.GetILGenerator();
            if (!setMethod.IsStatic)
            {
                generator.Emit(OpCodes.Ldarg_0);
            }
            generator.Emit(OpCodes.Ldarg_1);

            if (property.PropertyType.IsValueType)
            {
                generator.Emit(OpCodes.Unbox_Any, property.PropertyType);
            }
            else
            {
                generator.Emit(OpCodes.Castclass, property.PropertyType);
            }

            if (!setMethod.IsStatic && !property.DeclaringType.IsValueType)
            {
                generator.EmitCall(OpCodes.Callvirt, setMethod, null);
            }
            else
            {
                generator.EmitCall(OpCodes.Call, setMethod, null);
            }

            generator.Emit(OpCodes.Ret);
            return (SetValueDelegate)dynamicMethod.CreateDelegate(typeof(SetValueDelegate));
        }
    }
}
