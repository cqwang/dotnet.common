using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.EnumExt{
    public class EnumConvertor
    {
        public TEnum ConvertNameToEnum<TEnum>(string enumName)
        {
            return (TEnum) Enum.Parse(typeof(TEnum), enumName);

        }
    }
}