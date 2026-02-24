using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    public partial class DelegateSyntax
    {
        public static void TestInterfaceCallBack()
        {
            //创建控制器对象,将提供给它的回调对象传入
            Controller obj = new Controller(new CallBackClass());
            obj.Begin();
        }
    }


    /// <summary>
    /// 接口
    /// </summary>
    public interface ICallBack
    {
        void Run();
    }

    /// <summary>
    /// 实现类
    /// </summary>
    public class CallBackClass : ICallBack
    {
        public void Run()
        {
            Console.WriteLine("收到");
        }
    }

    /// <summary>
    /// 通知源
    /// </summary>
    class Controller
    {
        public ICallBack CallBackObject = null;

        public Controller(ICallBack obj)
        {
            this.CallBackObject = obj;
        }

        public void Begin()
        {
            Console.WriteLine("敲击任意键显示当前时间,ESC键退出...");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                CallBackObject.Run(); //Run为回调的方法,外部传入
            }
        }
    }
}
