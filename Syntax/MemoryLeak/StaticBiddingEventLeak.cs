using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax.MemoryLeak
{
    partial class MemoryLeakTest
    {
        public static void TestStaticBiddingEventLeak()
        {
            DisplayMemory();
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("--- New Listener #{0} ---", i + 1);

                var listener = new TestStaticBiddingEventListener(new TestClassHasEvent());//静态订阅，没有手工退订，不会回收

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                DisplayMemory();
            }
            Console.Read();
        }
    }
}
