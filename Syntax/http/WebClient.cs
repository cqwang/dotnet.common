using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    partial class Spider
    {
        public static void TestWebClient()
        {
            try
            {
                WebClient webClient = new WebClient()
                {
                    Credentials = CredentialCache.DefaultCredentials
                };

                Byte[] pageData = webClient.DownloadData("http://www.163.com");//从指定网站下载数据

                var pageEncoding = Encoding.UTF8;//Encoding.Default
                string pageHtml = pageEncoding.GetString(pageData);//根据网页编码，将下载的字节流转化为字符串

                //数据处理


                //数据存储
                using (StreamWriter streamWriter = new StreamWriter("c:\\test\\ouput.html"))//将获取的内容写入文本
                {
                    streamWriter.Write(pageHtml);
                }
            }
            catch (WebException webEx)
            {
                //异常处理
            }
        }
    }
}
