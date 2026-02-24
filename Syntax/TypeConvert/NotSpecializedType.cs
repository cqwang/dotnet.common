using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class NotSpecializedType
    {
        public static void TestNotSpecializedType()
        {
            var a = 1;
            //a = "Test"; //编译错误

            object obj = 1;
            obj = "Test"; //object类型可以分配其任何子类型的值


            dynamic dynamic = "test";
            a++; //运行时报错
            a = 1;
            a++; //执行正常
        }
    }
}
