using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace dotnet.common.SerialiserExt
{
    public class DataContactSerizer
    {
        private static DataContractSerializerSettings _settings;

        public static DataContractSerializerSettings DefaultSettings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new DataContractSerializerSettings();
                    var xmlDictionary = new XmlDictionary();
                    _settings.RootName = xmlDictionary.Add("request");
                }
                return _settings;
            }
        }

        public static string Serialize<T>(object obj, Encoding encoding, DataContractSerializerSettings settings = null)
        {
            DataContractSerializer serializer = null;
            if (settings == null)
            {
                serializer = new DataContractSerializer(typeof(T));
            }
            else
            {
                //settings.KnownTypes = new List<Type>() { typeof(T) };
                serializer = new DataContractSerializer(typeof(T), settings);
            }

            var value = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, obj);
                var bytes = memoryStream.ToArray();
                value = encoding.GetString(bytes);
            }

            return value;
        }

        public static T Deserialize<T>(string value, Encoding encoding, DataContractSerializerSettings settings = null)
        {
            DataContractSerializer serializer = null;
            if (settings == null)
            {
                serializer = new DataContractSerializer(typeof(T));
            }
            else
            {
                //settings.KnownTypes = new List<Type>() { typeof(T) };
                serializer = new DataContractSerializer(typeof(T), settings);//非空时反序列化失败，待研究
            }

            using (var memoryStream = new MemoryStream(encoding.GetBytes(value)))
            {
                return (T)serializer.ReadObject(memoryStream);
            }
        }

        /// <summary>
        /// 序列化简单类型的参数
        /// </summary>
        /// <param name="paramValueList"></param>
        /// <returns></returns>
        public static string SerializeSimpleType(List<KeyValuePair<string, string>> paramValueList)
        {
            if (paramValueList == null || !paramValueList.Any())
            {
                return string.Empty;
            }

            var value = new StringBuilder();
            foreach (var item in paramValueList)
            {
                value.Append($"<{item.Key}>{item.Value}</{item.Key}>");
            }
            return value.ToString();
        }
    }
}
