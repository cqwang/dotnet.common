using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ReflectionModule
    {
        public static void TestLoadModule()
        {
            Assembly assembly = Assembly.Load("mscorlib");
            Module module = assembly.GetModule("CommonLanguageRuntimeLibrary");
        }

        public static void PrintInfo(Module module)
        {
            Console.WriteLine("模块名：" + module.Name);

            Type[] types = module.FindTypes(Module.FilterTypeName, "Assembly*");
            foreach (var item in types)
            {
                Console.WriteLine("类名：" + item.Name);
            }
        }
    }
}
