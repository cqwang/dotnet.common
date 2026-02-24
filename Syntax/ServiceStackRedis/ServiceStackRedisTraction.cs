
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax.ServiceStackRedis
{
    class ServiceStackRedisTransaction
    {
        public static void Test()
        {
            var provider = ServiceStackRedisProviderFactory.CreateProvider();
            try
            {
                using (var client = provider.ClientManager.GetClient())
                {
                    using (var transaction = client.CreateTransaction())
                    {
                        transaction.QueueCommand(p => p.Set("test", 1));
                        transaction.QueueCommand(p => p.Increment("test", 1));
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                //log Error
            }
        }
    }
}
