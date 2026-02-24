using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax.MemoryLeak
{
    partial class MemoryLeakTest
    {
        public static void TestStaticBiddingEventNormal()
        {
            DisplayMemory();
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("--- New Listener #{0} ---", i + 1);

                using (var listener = new TestStaticBiddingEventListener(new TestClassHasEvent())) //
                {

                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                DisplayMemory();
            }
            Console.Read();
        }
    }
}
