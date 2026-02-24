using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax.Serialization
{
    [Serializable]
    public class Person3
    {
        public string name;
        private string sex;

        private double Salary { get; set; }

        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public Person3() { }
        public Person3(string name, string sex, int age, double salary)
        {
            this.name = name;
            this.sex = sex;
            this.age = age;
            this.Salary = salary;
        }
    }
}
