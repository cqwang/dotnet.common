using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.SerialiserExt
{
    public class BinaryFileSerializer
    {
        public static void WriteString(string filePath, string value, Encoding encoding)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                using (var binaryWriter = new BinaryWriter(fileStream, encoding))
                {
                    var bytes = encoding.GetBytes(value);
                    binaryWriter.Write(bytes.Length);
                    binaryWriter.Write(bytes);
                }
            }
        }

        public static string ReadString(string filePath, Encoding encoding)
        {
            var value = string.Empty;
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var binaryReader = new BinaryReader(fileStream, encoding))
                {
                    var length = binaryReader.ReadInt32();
                    var bytes = binaryReader.ReadBytes(length);
                    value = encoding.GetString(bytes);
                }
            }
            return value;
        }
    }
}
