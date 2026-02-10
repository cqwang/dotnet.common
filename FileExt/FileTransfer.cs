using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.FileExt
{
    public class FileTransfer
    {
        public static string Download(string uri, string savePath)
        {
            var fileName = Path.GetFileName(uri);
            savePath = Path.Combine(savePath, fileName);
            using (var client = new WebClient())
            {
                client.DownloadFile(uri, savePath);
            }
            return savePath;
        }

        public static string Download(string uri, Stream stream, string savePath)
        {
            var fileName = Path.GetFileName(uri);
            savePath = Path.Combine(savePath, fileName);
            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                stream.CopyTo(fs);
            }
            return savePath;
        }
    }
}
