using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.FileExt
{
    public class FileProvidor
    {
        /// <summary>
        /// 清空目录
        /// </summary>
        /// <param name="path"></param>
        public static void ClearDir(string path)
        {
            string[] subDirs = Directory.GetDirectories(path);
            foreach (string dir in subDirs)
                Directory.Delete(dir, true);

            string[] subFiles = Directory.GetFiles(path);
            foreach (string file in subFiles)
                File.Delete(file);
        }

        /// <summary>
        /// 文件编码格式转换
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="searchFilePattern"></param>
        /// <param name="sourceEncoding"></param>
        /// <param name="targetEncoding"></param>
        public static bool Convert(string directory, string searchFilePattern, string sourceEncoding, string targetEncoding)
        {
            var files = Directory.GetFiles(directory, searchFilePattern, SearchOption.AllDirectories);
            if (files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    var content = Read(file, Encoding.GetEncoding(sourceEncoding));
                    Save(file, content, Encoding.GetEncoding(targetEncoding));
                }
                return true;
            }
            return false;
        }

        public static string Read(string file, Encoding encoding)
        {
            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream, encoding, true))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public static void Save(string file, string content, Encoding encoding)
        {
            using (var fileStream = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(fileStream, encoding))
                {
                    streamWriter.Write(content);
                }
            }
        }
    }
}
