using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class PartialMethod
    {
        public static void Test()
        {
            new PartialPerson().Say();
        }
    }

    public partial class PartialPerson
    {
        //必须定义在分部类，是隐式私有的，只能在内部调用
        partial void Say1(); //方法签名


        partial void Say1()
        {
            Console.WriteLine("你好");
        }

        public void Say()
        {
            Say1();
        }
    }
}
