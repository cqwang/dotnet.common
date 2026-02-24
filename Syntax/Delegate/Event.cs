using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    public partial class DelegateSyntax
    {
        public static void TestEvent()
        {
            Calculator calc = new Calculator();
            calc.CalculationPerformedEvent += HandleEventListener.HandleEvent; //订阅事件
            calc.CalculateProduct(20, 72);
        }
    }


    /// <summary>
    /// 接收通知的类
    /// </summary>
    public class HandleEventListener
    {
        /// <summary>
        ///事件处理方法：作为侦听器可以被注入到事件源中去，并在接收到事件源发出的通知时，对该事件进行处理（响应）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void HandleEvent(object sender, CalculationEventArgs e)
        {
            Console.WriteLine("Good Class: {0} x {1} = {2}", e.X, e.Y, e.Result);
        }
    }


    /// <summary>
    /// 事件参数类
    /// </summary>
    public class CalculationEventArgs : EventArgs
    {
        //定义事件委托的所有参数字段，这些字段都是private的
        private int x, y, result;
        //构造器
        public CalculationEventArgs(int num1, int num2, int resultVal)
        {
            x = num1;
            y = num2;
            result = resultVal;
        }
        //以下是与字段对应的属性，这些属性都只有getter块，即都是只读的
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public int Result
        {
            get { return result; }
        }
    }

    /// <summary>
    /// 事件源
    /// </summary>
    public class Calculator
    {
        public event EventHandler<CalculationEventArgs> CalculationPerformedEvent; //事件委托字段
        public int CalculateProduct(int num1, int num2)
        {
            int result = num1 * num2;
            //执行事件委托，向外部发出事件通知
            OnCalculationPerformed(new CalculationEventArgs(num1, num2, result));
            return result;
        }
        //事件触发时调用的方法
        private void OnCalculationPerformed(CalculationEventArgs args)
        {
            //创建一份事件的地址拷贝，以避免并行编程中的竞争条件
            EventHandler<CalculationEventArgs> handler = CalculationPerformedEvent;
            //查看事件订阅
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }

}
