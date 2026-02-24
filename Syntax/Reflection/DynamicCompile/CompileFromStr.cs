using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class CompileFromStr
    {
        public static void DoCompile()
        {
            ICodeCompiler compiler = new CSharpCodeProvider().CreateCompiler();//得到一个CSharp的编译器
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("system.dll");
            cp.ReferencedAssemblies.Add("system.data.dll");
            cp.ReferencedAssemblies.Add("system.xml.dll");
            cp.GenerateExecutable = false;//这是指示说我们输出的程序集是dll，而不是exe
            cp.GenerateInMemory = true; //这是指示在内存中创建该程序集 

            StringBuilder sb = new StringBuilder();
            sb.Append("using System; \n");
            sb.Append("public class MyClass{");
            sb.Append("public string HelloWorld(){");
            sb.AppendFormat("return {0};", "\"Hello,world\"");
            sb.Append("}}"); //这里为止，我们构造了一个动态的类型，它有一个方法是HelloWorld 

            CompilerResults result = compiler.CompileAssemblyFromSource(cp, sb.ToString()); //执行编译

            object _compilerobject = result.CompiledAssembly.CreateInstance("MyClass");

            MethodInfo method = _compilerobject.GetType().GetMethod("HelloWorld");
            Console.WriteLine(method.Invoke(_compilerobject, null).ToString());

            Console.Read();
        }
    }
}
