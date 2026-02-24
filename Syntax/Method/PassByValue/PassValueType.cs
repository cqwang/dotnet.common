using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class PassValueType
    {
        public static unsafe void Test()
        {
            ValueTypeEntity entity1 = new ValueTypeEntity() { Id = 1, Age = 22 };
            ValueTypeEntity entity2 = new ValueTypeEntity() { Id = 1, Age = 22 };
            ValueTypeEntity entity3 = new ValueTypeEntity() { Id = 1, Age = 22 };

            Console.WriteLine("EntityMain1: 0x{0:x}", (int)&entity1);
            Console.WriteLine("EntityMain2: 0x{0:x}", (int)&entity2);
            Console.WriteLine("EntityMain3: 0x{0:x}", (int)&entity3);
            DoTest(entity1, entity2, entity3);

            ValueTypeEntity entity5 = new ValueTypeEntity() { Id = 1, Age = 22 };
            Console.WriteLine("EntityMain5: 0x{0:x}", (int)&entity5);

            Console.ReadKey();
        }

        private static unsafe void DoTest(ValueTypeEntity entity1, ValueTypeEntity entity2, ValueTypeEntity entity3)
        {
            Console.WriteLine("EntityTest1: 0x{0:x}", (int)&entity1);
            Console.WriteLine("EntityTest2: 0x{0:x}", (int)&entity2);
            Console.WriteLine("EntityTest3: 0x{0:x}", (int)&entity3);
            ValueTypeEntity entity4 = new ValueTypeEntity() { Id = 1, Age = 22 };
            Console.WriteLine("EntityTest4: 0x{0:x}", (int)&entity4);
        }

    }

    public struct ValueTypeEntity
    {
        public int Id;
        public int Age;
    }
}
