using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.CmdExt
{
    public class CmdExecutor
    {
        /// <summary>
        /// exe程序执行命令行
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Run(string exePath, string args)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo(exePath, args);
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.CreateNoWindow = true;

            Process process = Process.Start(processInfo);
            string message = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return message;
        }

        /// <summary>
        /// 执行命令行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string RunCmd(string arg)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.AutoFlush = true;

            process.StandardInput.WriteLine(arg);
            process.StandardInput.WriteLine("exit");

            string message = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return message;
        }
    }
}
