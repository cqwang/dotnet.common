using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax.Serialization
{
    [Serializable]
    public class Person1
    {
        public string name;
        public string sex;
        public Person1() { }
        public Person1(string name, string sex)
        {
            this.name = name;
            this.sex = sex;
        }
    }
}
