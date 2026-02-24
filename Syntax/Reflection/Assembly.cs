using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ReflectionAssembly
    {
        public static void TestLoadAssembly()
        {
            //加载系统程序集
            Assembly assem1 = Assembly.Load("mscorlib");

            //加载非系统程序集
            string assemblyFilePath = @"D:\Tools\Test\AutoMapper\bin\Debug\AutoMapper.dll";
            Assembly assem2 = Assembly.LoadFrom(assemblyFilePath);
            Assembly assem22 = Assembly.LoadFile(assemblyFilePath);

            //获取当前执行代码的程序集
            Assembly assem3 = Assembly.GetExecutingAssembly();

            //获取指定类型所在的程序集
            var ass4 = typeof(ReflectionAssembly).Assembly;
        }

        public static void PrintInfo(Assembly assem)
        {
            Console.WriteLine("程序集全名:" + assem.FullName);
            Console.WriteLine("程序集的版本:" + assem.GetName().Version);
            Console.WriteLine("程序集初始位置:" + assem.CodeBase);
            Console.WriteLine("程序集位置:" + assem.Location);
            Console.WriteLine("程序集入口:" + assem.EntryPoint);

            Type[] types = assem.GetTypes();
            foreach (var item in types)
            {
                Console.WriteLine("类" + item.Name);
            }
        }
    }
}
