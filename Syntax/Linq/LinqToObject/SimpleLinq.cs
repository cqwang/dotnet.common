using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class SimpleLinq
    {
        public static void Test()
        {
            List<string> list = new List<string>() { "12", "123", "1234", "12345" };

            List<int> datas1 = list.ConvertAll<int>(temp => Int32.Parse(temp));//元素转型

            IEnumerable<string> datas2 = list.Select<string, string>(x => (x + x).ToString());//元素投影

            IEnumerable<string> datas3 = list.Where(temp => temp.Length < 3);//元素筛选

            Dictionary<string, string> dict = list.ToDictionary<string, string>(p => p);//转化成一对一的字典，但键重复时会抛出异常

            ILookup<string, string> lookup = list.ToLookup<string, string>(p => p);//转化成一对多的字典

            list.Sort((x, y) => { return x.Length - y.Length; });//按字符串长度正序排列

            if (list.Any(p => p.Length > 4)) //判断列表中是否存在满足条件的元素
            {

            }
        }
    }
}
