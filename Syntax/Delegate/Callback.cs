using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    public partial class DelegateSyntax
    {
        /// <summary>
        /// 模拟调用
        /// </summary>
        public static void TestDelegateCallback()
        {
            CalculatorNotifySource calc = new CalculatorNotifySource(CalculationListener.CalculationPrinter);
            calc.CalculateProduct(10, 20);//执行时向CalculationListener发送一个通知,导致执行CalculationListener中的CalculationPrinter方法
        }
    }






    /// <summary>
    /// 定义委托
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="result"></param>
    public delegate void NotifyCalculation(int x, int y, int result);

    /// <summary>
    /// 接收通知的类
    /// </summary>
    public class CalculationListener
    {
        /// <summary>
        /// 与委托匹配的方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="result"></param>
        public static void CalculationPrinter(int x, int y, int result)
        {
            Console.WriteLine("Calculation Notification: {0} x {1} = {2}",
                            x, y, result);
        }
    }

    /// <summary>
    /// 通知源
    /// </summary>
    public class CalculatorNotifySource
    {
        /// <summary>
        /// 委托字段
        /// </summary>
        NotifyCalculation calcListener;
        
        /// <summary>
        /// 绑定委托的构造函数
        /// </summary>
        /// <param name="listener"></param>
        public CalculatorNotifySource(NotifyCalculation listener)
        {
            calcListener = listener;
        }

        /// <summary>
        /// 执行委托的方法
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public int CalculateProduct(int num1, int num2)
        {
            int result = num1 * num2;
            calcListener(num1, num2, result); //向委托发送通知，说明已经执行了一个计算
            return result;
        }
    }

}
