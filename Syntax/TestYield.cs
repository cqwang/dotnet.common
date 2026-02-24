using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    class TestYield
    {
        public static IEnumerable<Model> GetModels()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return new Model() { Page = i + 1000 };
            }
            yield break;
        }

        public static IEnumerable<int> GetEnums()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return i + 1000;
            }
            yield break;
        }

        public static void Test()
        {
            var data = GetModels();

            foreach (var d in data)
            {
                d.Page = 0;
            }
            foreach (var dd in data.ToList<Model>())
            {
                Console.WriteLine(dd.Page);
            }

            Console.Read();
        }
    }

    public class Model
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page { get; set; }
    }
}
