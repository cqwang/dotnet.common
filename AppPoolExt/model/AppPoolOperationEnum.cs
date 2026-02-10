using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.AppPoolExt
{
    public enum AppPoolOperationEnum
    {
        /// <summary>
        /// 启动
        /// </summary>
        Start,

        /// <summary>
        /// 回收
        /// </summary>
        Recycle,

        /// <summary>
        /// 停止
        /// </summary>
        Stop
    }
}
