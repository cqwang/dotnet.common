using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TestCase
{
    class TestXmlConfigModifier
    {
        public static void TestModifer()
        {
            var key = "123";
            var value = Console.ReadLine();

            Console.WriteLine("AppSettings: " + ConfigurationManager.AppSettings[key]);
            XmlConfigModifier.AddOrUpdateAppSetting(key, value);
            Console.WriteLine("AppSettings: " + ConfigurationManager.AppSettings[key]);

            Console.WriteLine("ConnectionStrings: " + ConfigurationManager.ConnectionStrings[key]);
            XmlConfigModifier.AddOrUpdateConnectionString(key, value);
            Console.WriteLine("ConnectionStrings: " + ConfigurationManager.ConnectionStrings[key]);
        }
    }
}
