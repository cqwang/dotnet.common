using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax.MemoryLeak
{
    class TestClassHasEvent
    {
        public delegate void TestEventHandler(object sender, EventArgs e);
        public event TestEventHandler YourEvent;
        protected void OnYourEvent(EventArgs e)
        {
            if (YourEvent != null) YourEvent(this, e);
        }
    }
}
