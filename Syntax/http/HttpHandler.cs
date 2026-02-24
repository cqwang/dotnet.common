using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace dotnet.common.Syntax
{
    class HttpHandlerServer : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            int bufferSize = 1024;
            //把信息转换为String，找出输入条件Id
            byte[] bytes = new byte[bufferSize];
            int length = context.Request.InputStream.Read(bytes, 0, bufferSize);
            string condition = Encoding.Default.GetString(bytes);
            int id = int.Parse(condition.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1]);

            var person = GetPersonList().Where(x => x.ID == id).First();//根据Id查找对应Person对象

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(context.Response.OutputStream, person); //格式化为二进制数据写入OutputStream
        }


        //模拟源数据
        private IList<Cqwang.BackEnd.CSharp.Syntax.HttpHandler.Person> GetPersonList()
        {
            var personList = new List<Cqwang.BackEnd.CSharp.Syntax.HttpHandler.Person>();

            var person1 = new Cqwang.BackEnd.CSharp.Syntax.HttpHandler.Person();
            person1.ID = 1;
            person1.Name = "Leslie";
            person1.Age = 30;
            personList.Add(person1);
            return personList;
        }
    }
}
