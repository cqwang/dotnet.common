using Cqwang.BackEnd.CSharp.Syntax.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class Formatter
    {
        static string filePath = @".\test.txt";
        static string xmlFilePath = @".\test.xml";

        /// <summary>
        /// 通过反序列化，可以查看那些字段和属性可以被序列化
        /// </summary>
        /// <param name="args"></param>
        static void Test()
        {
            TestBinaryFormatter();
            TestSoapFormatter();
            TestXmlSerializer();
        }

        static void TestBinaryFormatter()
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var person = new Person3("张三", "男", 15, 10000);
                formatter.Serialize(fileStream, person);

                fileStream.Position = 0;
                var p = formatter.Deserialize(fileStream) as Person3;
            }
        }

        static void TestSoapFormatter()
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                SoapFormatter formatter = new SoapFormatter();
                var person = new Person3("张三", "男", 15, 10000);
                formatter.Serialize(fileStream, person);

                fileStream.Position = 0;
                var p = formatter.Deserialize(fileStream) as Person3;
            }
        }

        static void TestXmlSerializer()
        {
            using (FileStream fileStream = new FileStream(xmlFilePath, FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Person3));
                var person = new Person3("张三", "男", 15, 10000);
                formatter.Serialize(fileStream, person);

                fileStream.Position = 0;
                var p = formatter.Deserialize(fileStream) as Person3;
            }
        }
    }
}
