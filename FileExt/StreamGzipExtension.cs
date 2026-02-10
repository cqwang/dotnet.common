using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace dotnet.common.FileExt
{
    public static partial class StreamExtension
    {
        public static byte[] GZipCompress(this string str, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(str);
            using (var stream = new MemoryStream())
            {
                var gzip = new GZipStream(stream, CompressionMode.Compress);
                gzip.Write(bytes, 0, bytes.Length);
                gzip.Close();
                return stream.ToArray();
            }
        }

        public static string GZipDecompress(this byte[] bytes, Encoding encoding)
        {
            string str = null;
            using (var tempStream = new MemoryStream())
            {
                using (var stream = new MemoryStream(bytes))
                {
                    var gzip = new GZipStream(stream, CompressionMode.Decompress);
                    gzip.CopyTo(tempStream);
                    gzip.Close();

                    str = encoding.GetString(tempStream.ToArray());
                }
            }
            return str;
        }
    }
}
