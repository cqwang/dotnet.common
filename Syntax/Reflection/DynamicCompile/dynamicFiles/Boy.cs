using System;
namespace Cqwang.BackEnd.CSharp.Syntax
{
    /// <summary>
    /// 创建或修改用于动态编译的类，放在指定目录中
    /// </summary>
    public class Boy : Cqwang.BackEnd.CSharp.Syntax.DynamicCompileBaseLibrary.Person
    {
        public override string ToString()
        {
            return age > 18 ? string.Concat("[Boy]name:", name, "; age:", age, "已经是个大男孩了")
                : string.Concat("[Boy]name:", name, "; age:", age);
        }
    }
}
