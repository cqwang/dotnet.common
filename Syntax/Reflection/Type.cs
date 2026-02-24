using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ReflectionType
    {
        public static void TestLoadType()
        {
            Type type = typeof(Directory);
            type = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory).GetType();
            type = Type.GetType("System.IO.Directory");
        }

        public static void PrintInfo(Type type)
        {
            Console.WriteLine("类型名:" + type.Name);
            Console.WriteLine("类全名：" + type.FullName);
            Console.WriteLine("命名空间名:" + type.Namespace);
            Console.WriteLine("程序集名：" + type.Assembly);
            Console.WriteLine("模块名:" + type.Module);
            Console.WriteLine("基类名：" + type.BaseType);
            Console.WriteLine("是否类：" + type.IsClass);

            Console.WriteLine("类的公共成员：");
            MemberInfo[] memberInfos = type.GetMembers();//得到所有公共成员
            foreach (var item in memberInfos)
            {
                Console.WriteLine("成员类型：" + item.MemberType + "\t成员" + item);
            }

        }
    }
}
