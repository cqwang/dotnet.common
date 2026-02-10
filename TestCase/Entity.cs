using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TestCase
{
    public class A
    {
        public int Value { get; set; }

        public List<int> ValueList { get; set; }

        public AA CustomObj { get; set; }

        public List<AA> CustomObjList { get; set; }

        public A Next { get; set; }
    }

    public class AA
    {
        public int Value { get; set; }
    }


    public class B
    {
        public int Value { get; set; }

        public List<int> ValueList { get; set; }

        public BB CustomObj { get; set; }

        public List<BB> CustomObjList { get; set; }

        public B Next { get; set; }
    }

    public class BB
    {
        public int Value { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}
