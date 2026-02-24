using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.SDK.Metrics.TestCase
{
    class TestInfluxDb
    {
        public static void dd(string table)
        {
            string address = string.Format("http://localhost:8086/query?q=select+*+from+\"{0}\"&db=db_metrics", (object)table);
            string str = string.Empty;
            using (WebClient webClient = new WebClient())
                str = webClient.DownloadString(address);
        }
    }
}
