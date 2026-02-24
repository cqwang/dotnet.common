using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class PassValueInterfaceType
    {
        public static void Test()
        {
            Entity entity = new Entity() { Age = 1 };
            DoAdd(entity);
            Console.WriteLine(entity.Age);//1

            IAdd ia = new Entity() { Age = 1 };
            DoAdd(ia);
            Console.WriteLine(ia.Age);//6

            Console.ReadKey();
        }

        private static void DoAdd(IAdd a)
        {
            a.Add(5);
        }
    }

    public interface IAdd
    {
        int Age { get; set; }
        void Add(int i);
    }

    public struct Entity : IAdd
    {
        public int Age { get; set; }
        public void Add(int i)
        {
            this.Age += i;
        }
    }
}
