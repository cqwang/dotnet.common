using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax.MemoryLeak
{
    class TestListener
    {
        byte[] m_ExtraMemory = new byte[1000000];

        private TestClassHasEvent _inject;

        public TestListener(TestClassHasEvent inject)
        {
            _inject = inject;
            _inject.YourEvent += new TestClassHasEvent.TestEventHandler(_inject_YourEvent);
        }

        void _inject_YourEvent(object sender, EventArgs e)
        {

        }
    }
}
