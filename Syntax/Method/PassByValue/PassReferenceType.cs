using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class PassReferenceType
    {
        public static void Test()
        {
            ReferenceEntity entity = new ReferenceEntity() { Id = 1, Age = 22 };
            DoTest(entity);
            Console.ReadKey();
        }

        private static void DoTest(ReferenceEntity entity)
        {
            entity = new ReferenceEntity() { Id = 2, Age = 44 };
        }
    }

    public class ReferenceEntity
    {
        public int Id;
        public int Age;
    }
}
