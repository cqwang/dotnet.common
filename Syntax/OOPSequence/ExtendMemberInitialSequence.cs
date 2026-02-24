using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ExtendMemberInitialSequence
    {
        public static void DoTest()
        {
            var person = new Baby();
            person.Grow();
        }
    }

    public class Person
    {
        protected static int Age = 1;
        protected static int Weight = 1;

        static Person()
        {
            Console.WriteLine(string.Format("【基类静态成员初始化】 Age: {0}, Weight: {1}", Age, Weight));

            Age++;
            Weight++;
            Console.WriteLine(string.Format("【基类静态构造函数】 Age: {0}, Weight: {1}", Age, Weight));
        }

        public Person()
        {
            Age++;
            Weight++;
            Console.WriteLine(string.Format("【基类普通构造函数】 Age: {0}, Weight: {1}", Age, Weight));
        }

        public virtual void Grow()
        {
            Age++;
            Weight++;
            Console.WriteLine(string.Format("【基类虚方法】 Age: {0}, Weight: {1}", Age, Weight));
        }
    }

    public class Baby : Person
    {
        protected static string Name = "小薇";

        static Baby()
        {
            Console.WriteLine(string.Format("【派生类静态成员初始化】 Name: {0}", Name)); ;

            Age++;
            Weight++;
            Console.WriteLine(string.Format("【派生类静态构造函数】 Age: {0}, Weight: {1}", Age, Weight)); ;
        }

        public Baby()
        {
            Age++;
            Weight++;
            Console.WriteLine(string.Format("【派生类普通构造函数】 Age: {0}, Weight: {1}", Age, Weight));
        }

        public override void Grow()
        {
            Age++;
            Weight++;
            Console.WriteLine(string.Format("【派生类重写后的方法】 Age: {0}, Weight: {1}", Age, Weight));
        }
    }
}
