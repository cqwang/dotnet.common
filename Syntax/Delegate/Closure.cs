using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    public partial class DelegateSyntax
    {
        public static void TestClosure()
        {
            // 定义一个局部变量
            int calculationCount = 0;
            // 使用系统委托和匿名方法
            Func<int, int, int> productFunc = delegate (int x, int y) {
                // 递增外部变量
                calculationCount++;
                return x * y;
            };


            //闭包释疑
            Action[] tmp = new Action[3];
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = () => Console.WriteLine(i);
            }
            Array.ForEach(tmp, m => m()); //执行时都输出循环结束后的i值，为3 3 3 原因是，委托参数是在循环结束后才传进去。

            tmp = new Action[3];
            for (int i = 0; i < tmp.Length; i++)
            {
                int j = i;
                tmp[i] = () => Console.WriteLine(j);
            }
            Array.ForEach(tmp, m => m());//执行时输出每次循环的临时变量j值，为0 1 2
        }
    }
}
