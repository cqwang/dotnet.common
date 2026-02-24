using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class CombinationMemberInitialSequence
    {
        public static void DoTest()
        {
            Console.WriteLine(string.Format("A.Age: {0}, B.Age: {1}", A.Age, B.Age));
        }
    }

    public class A
    {
        public static int Age = B.Age + 4;//第一步，需要计算B.Age；第四步，2+4=6

        static A()
        {
            B.Age = Age + 2; ;//第五步 ，6+2=8
        }
    }

    public class B
    {
        public static int Age = A.Age + 2;//第二步 0+2=2

        static B()
        {
            A.Age = Age + 2; ;//第三步 2+2=4
        }
    }
}
