using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    public partial class HttpIPC
    {
        public void TestHttpGet()
        {
            Uri httpURL = new Uri("http://baidu.com");
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
            Stream respStream = httpResp.GetResponseStream();
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);

            char[] cbuffer = new char[256];
            int byteRead = respStreamReader.Read(cbuffer, 0, 256);

            StringBuilder sb = new StringBuilder();
            while (byteRead != 0)
            {
                string strResp = new string(cbuffer, 0, byteRead);
                sb.Append(strResp);
                byteRead = respStreamReader.Read(cbuffer, 0, 256);
            }
            respStream.Close();
        }
    }
}
