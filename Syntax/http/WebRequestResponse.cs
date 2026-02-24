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
        public static void TestWebRequestResponse()
        {
            Uri fileUri = new Uri("https://www.baidu.com/");
            WebRequest request = WebRequest.Create(fileUri);

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[1024];
                        int actual;
                        while ((actual = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            memoryStream.Write(buffer, 0, actual);
                        }

                        using (StreamReader reader = new StreamReader(memoryStream))
                        {
                            reader.BaseStream.Seek(0, SeekOrigin.Begin);
                            string str = reader.ReadToEnd();
                            Console.WriteLine(str);
                        }
                    }
                }
            }
        }
    }
}
