using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ReferenceConvert
    {
        public static void Convert()
        {
            AClass a = new AClass() { Name = "a的名字" };
            BClass b = (BClass)a;//强制转化成功

            object obj = a;
            b = (BClass)obj;//编译成功，但运行时异常，提示无法转化

            Console.ReadKey();

        }
    }

    public class AClass
    {
        public string Name { get; set; }
    }

    public class BClass
    {
        public string Name { get; set; }

        /// <summary>
        /// 转换操作符
        /// </summary>
        /// <param name="a"></param>
        public static explicit operator BClass(AClass a)
        {
            BClass b = new BClass() { Name = a.Name };
            return b;
        }
    }
}
