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
    /// <summary>
    /// 功能：根据输入的用户名，输出用户信息
    /// 
    /// 启动程序后，将dynamicFiles文件夹拷贝到启动目录，执行compile name=boy.cs动态编译。
    /// </summary>
    class CompileFromFile
    {
        //可通过配置文件设置
        public static readonly string dynamicFilePath = @".\dynamicFiles";
        public const string compileCommandHeader = "compile name=";
        public static Dictionary<string, string> compileProviderParams = new Dictionary<string, string>();
        public static readonly char[] Separator_Array = new char[] { ',' };

        /// <summary>
        /// 用户信息列表
        /// </summary>
        public static Dictionary<string, Cqwang.BackEnd.CSharp.Syntax.DynamicCompileBaseLibrary.Person> userInfoDict = new Dictionary<string, Cqwang.BackEnd.CSharp.Syntax.DynamicCompileBaseLibrary.Person>();

        public static void DoTest()
        {
            compileProviderParams.Add("CompilerVersion", "v4.0");//supportedRuntime version

            while (true)
            {
                Console.WriteLine("请输入指令：");
                string input = Console.ReadLine();
                if (input.StartsWith(compileCommandHeader, StringComparison.CurrentCultureIgnoreCase))
                {
                    DynamicCompile(input);
                }
                else
                {
                    Cqwang.BackEnd.CSharp.Syntax.DynamicCompileBaseLibrary.Person person;
                    if (userInfoDict.TryGetValue(input, out person))
                        Console.WriteLine(person.ToString());
                    else
                        Console.WriteLine("无该用户信息");
                }
            }
        }

        static void DynamicCompile(string command)
        {
            string[] fileNames = command.Substring(compileCommandHeader.Length).Trim().Split(Separator_Array);
            if (fileNames == null || fileNames.Length == 0)
            {
                Console.WriteLine("没有找到文件");
                return;
            }

            //修改为完整路径
            for (int i = 0; i < fileNames.Length; i++)
                fileNames[i] = Path.Combine(dynamicFilePath, fileNames[i]);

            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.GenerateExecutable = false; //生成exe还是dll
            compilerParameters.GenerateInMemory = true; //只留在内存还是写入文件，若留在内存则在%temp%目录下面生成且使用完之后就删除。
            compilerParameters.IncludeDebugInformation = false;
            //添加引用的组件列表，system32或当前目录的组件可不指定路径
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("Cqwang.Note.Library.dll");

            CSharpCodeProvider provider = new CSharpCodeProvider(compileProviderParams);
            CompilerResults results = provider.CompileAssemblyFromFile(compilerParameters, fileNames);
            if (results.Errors.HasErrors)
            {
                Console.WriteLine(string.Concat("编译错误: ", results.Errors.ToString()));
            }
            else
            {
                //通过反射获取用户，并添加到列表中
                Assembly assembly = results.CompiledAssembly;
                Type[] types = assembly.GetTypes();
                if (types == null || types.Length == 0)
                    return;

                foreach (Type type in types)
                {
                    if (type.IsAbstract)
                        continue;

                    ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
                    var person = constructorInfo.Invoke(null) as Cqwang.BackEnd.CSharp.Syntax.DynamicCompileBaseLibrary.Person;
                    if (person == null)
                        continue;

                    Console.WriteLine("Input: name, age");
                    string[] valus = Console.ReadLine().Trim().Split(Separator_Array);
                    if (valus == null || valus.Length == 0 || valus.Length != 2)
                        continue;

                    int age;
                    if (!int.TryParse(valus[1], out age))
                        continue;

                    person.name = valus[0];
                    person.age = age;
                    userInfoDict[person.name] = person;
                }
            }

        }
    }
}
