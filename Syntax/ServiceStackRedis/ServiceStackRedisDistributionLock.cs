using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax.ServiceStackRedis
{
    class ServiceStackRedisDistributionLock
    {
        public static void lock(string key)
        {
            var provider = ServiceStackRedisProviderFactory.CreateProvider();
            try
            {
                using (var client = provider.ClientManager.GetClient())
                {
                    using (client.AcquireLock(key))
                    {
                        //
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
