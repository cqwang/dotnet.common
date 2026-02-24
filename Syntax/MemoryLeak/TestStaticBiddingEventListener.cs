using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax.MemoryLeak
{
    class TestStaticBiddingEventListener : IDisposable
    {
        byte[] m_ExtraMemory = new byte[1000000];

        private TestClassHasEvent _inject;

        public TestStaticBiddingEventListener(TestClassHasEvent inject)
        {
            SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
        }

        public void Dispose()
        {
            SystemEvents.DisplaySettingsChanged -= new EventHandler(SystemEvents_DisplaySettingsChanged);
        }
    }
}
