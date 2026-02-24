using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public enum WatchModeEnum
    {
        /// <summary>
        /// 所有超限才拦截
        /// </summary>
        All,
        /// <summary>
        /// 存在超限则拦截
        /// </summary>
        Any
    }
}
