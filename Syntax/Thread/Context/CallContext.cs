using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ThreadCallContext
    {
        /// <summary>
        /// 调用上下文
        /// </summary>
        public static void TestCallContext()
        {
            //可以将值传播到辅助线程，远程对象跨域访问
            CallContext.LogicalSetData("name1", "value1");
            Task task1 = new Task(() =>
            {
                Console.WriteLine(CallContext.LogicalGetData("name1"));
            });

            task1.Start();
            task1.Wait();

            //不可以将值传播到辅助线程
            CallContext.SetData("name1", "value1");
            Task task2 = new Task(() =>
            {
                Console.WriteLine(CallContext.GetData("name1"));
            });

            task2.Start();
            task2.Wait();

            Console.WriteLine("Over");
            Console.Read();

        }
    }
}
