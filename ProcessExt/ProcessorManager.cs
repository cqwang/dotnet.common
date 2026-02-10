using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.ProcessExt
{
    public class ProcessManager
    {
        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool CloseProces(string processName)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.Equals(processName))
                {
                    if (!process.CloseMainWindow())
                        process.Kill();
                    return true;
                }
            }

            return false;
        }
    }
}
