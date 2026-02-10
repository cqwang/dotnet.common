using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace dotnet.common.DistributionExt
{
    public class ActionsTransaction
    {
        /// <summary>
        /// 事务处理
        /// </summary>
        /// <param name="actions"></param>
        public static void DoTransaction(params Action[] actions)
        {
            using (var transaction = new TransactionScope())
            {
                foreach (var action in actions)
                {
                    if (action != null)
                    {
                        action();//如果抛出异常，则事务不会提交，而会回滚
                    }
                }
                transaction.Complete();//提交事务
            }
        }
    }
}
