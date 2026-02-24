using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class MethodParameterDefaultValue
    {
        public static void Test()
        {
            DoTest();//所有参数使用默认值
            DoTest(8, "b");//隐式指定部分参数，没有指定的使用默认值
            DoTest(6, "v", DateTime.Now);//显示指定所有参数
            DoTest(6, dt: DateTime.Now);//隐式+显式指定参数
        }

        private static void DoTest(int x = 9, string s = "a", DateTime dt = default(DateTime))
        {
            Console.WriteLine($"x={ x}, s={s}, dt={dt}");
        }
    }
}
