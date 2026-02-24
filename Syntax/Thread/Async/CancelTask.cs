using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class CancelTask
    {
        public static void CancelButGoOnDoing()
        {
            Action action = () =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(1000);
                }
                Console.WriteLine("I finnish");
            };

            var token = new CancellationTokenSource(2000);
            var task = Task.Run(action, token.Token);

            Console.WriteLine("Over");
            Console.Read();
        }

        public static void CancelButGoOnDoing2()
        {
            Action action = () =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(1000);
                }
                Console.WriteLine("I finnish");
            };

            var timeout = 2000;
            var token = new CancellationTokenSource(timeout);
            var task = Task.Run(action, token.Token);
            task.Wait(timeout);
            if (!task.IsCompleted)
            {
                Console.WriteLine("cancel");
                token.Cancel();
            }
            else
            {
                Console.WriteLine("finish");
            }
        }

        public static void CancelAndStopDoing()
        {
            Action<CancellationTokenSource> action = (token) =>
            {
                for (int i = 0; i < 3; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("cancel");
                        return;
                    }
                    Thread.Sleep(1000);
                }
                Console.WriteLine("finnish");
            };

            var timeout = 2000;
            var cancellationTokenSource = new CancellationTokenSource(timeout);
            var task = Task.Run(() => action(cancellationTokenSource));

            Console.WriteLine("Over");
            Console.Read();
        }
    }
}
