using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class MyDisposeSub : MyDispose
    {
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                try
                {
                    #region 清理子类对象的资源
                    #endregion
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }
    }
}
