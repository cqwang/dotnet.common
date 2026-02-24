using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ThreadSynchronizationContext
    {
        private static ListBox listBox = new ListBox();

        /// <summary>
        /// 同步上下文
        /// </summary>
        public void TestSynchronizationContext()
        {
            Thread thread = new Thread(DoTestSynchronizationContext);
            thread.Start(SynchronizationContext.Current);
        }

        private static void DoTestSynchronizationContext(object state)
        {
            SynchronizationContext uiContext = state as SynchronizationContext;
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(10);
                uiContext.Post(UpdateUI, "line " + i.ToString());
            }
        }

        private static void UpdateUI(object state)
        {
            string text = state as string;
            listBox.Items.Add(text);
        }
    }
}
