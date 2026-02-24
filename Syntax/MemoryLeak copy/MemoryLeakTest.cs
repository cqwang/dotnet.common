using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax.MemoryLeak
{
    partial class MemoryLeakTest
    {
        static void DisplayMemory()
        {
            Console.WriteLine("Total memory: {0:###,###,###,##0} bytes", GC.GetTotalMemory(true));
        }
    }
}
