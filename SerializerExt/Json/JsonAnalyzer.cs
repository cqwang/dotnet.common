using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.SerialiserExt
{
    public class JsonAnalyzer
    {
        /// <summary>
        /// 格式化json字符串
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static string ParseJson(string jsonStr)
        {
            var stringReader = new StringReader(jsonStr);
            var jsonTextReader = new JsonTextReader(stringReader);
            var serializer = new JsonSerializer();
            var obj = serializer.Deserialize(jsonTextReader);
            if (obj != null)
            {
                var stringWriter = new StringWriter();
                var jsonTextWriter = new JsonTextWriter(stringWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonTextWriter, obj);
                return stringWriter.ToString();
            }
            else
            {
                return jsonStr;
            }
        }
    }
}
