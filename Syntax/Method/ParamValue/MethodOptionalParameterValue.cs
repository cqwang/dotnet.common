using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class MethodOptionalParameterValue
    {
        public static void Test()
        {
            DoTest(18);
            DoTest(18, "LiLei");
            DoTest(18, "LiLei", "ZhangMing");
        }

        private static void DoTest(int age, params string[] names)
        {
            if (names == null || names.Length == 0)
            {
                Console.WriteLine($"age={age}, names=");
            }
            else
            {
                Console.WriteLine($"age={age}, names.Length={names.Length}");
            }
        }

        
    }
}
