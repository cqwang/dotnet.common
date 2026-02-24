using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    class BeginEndHttpPost
    {
        static void PostRequestAsync()
        {
            //OnThreadActiveEvent("Start");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5700/Handler.ashx");
            webRequest.Method = "post";
            webRequest.BeginGetRequestStream(new AsyncCallback(EndGetRequestStream), webRequest);//开始异步请求
        }

        static void EndGetRequestStream(IAsyncResult result)
        {
            HttpWebRequest webRequest = result.AsyncState as HttpWebRequest;
            if (webRequest == null)
                return;

            //OnThreadActiveEvent("RequestStream Complete");
            Stream stream = webRequest.EndGetRequestStream(result);//结束异步请求

            byte[] condition = Encoding.Default.GetBytes("Id:1");//写入请求条件
            stream.Write(condition, 0, condition.Length);

            webRequest.BeginGetResponse(new AsyncCallback(EndGetResponse), webRequest);//异步接收回传信息
        }

        static void EndGetResponse(IAsyncResult result)
        {
            HttpWebRequest webRequest = result.AsyncState as HttpWebRequest;
            if (webRequest == null)
                return;

            //OnThreadActiveEvent("GetResponse Complete");
            //结束异步请求，获取结果
            WebResponse webResponse = webRequest.EndGetResponse(result);
            Stream stream = webResponse.GetResponseStream();

            //把输出结果转化为Person对象
            BinaryFormatter formatter = new BinaryFormatter();
            var person = (Person)formatter.Deserialize(stream);
            Console.WriteLine(string.Format("Person    Id:{0} Name:{1} Age:{2}", person.ID, person.Name, person.Age));
        }
    }
}
