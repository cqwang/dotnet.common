using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.FileExt
{
    /// <summary>
    /// 文件数据仓库
    /// 应用场景：个股数据分析，便于携带
    /// </summary>
    public class TxtRepository<TValue>
    {
        private string _directory;
        private string _title;
        private Func<string, TValue> _adapt;

        public TxtRepository(string directory, string title, Func<string, TValue> adapt)
        {
            this._directory = directory;
            this._title = title;
            this._adapt = adapt;
        }

        /// <summary>
        /// 文件所属目录
        /// </summary>
        public static string FileDirectory;

        private static readonly char[] CommaSeparator = new char[] { ',' };
        private static readonly string[] NewLine = new string[] { "\r\n" };

        public List<TValue> Read(string fileKey)
        {
            var filePath = GetFile(fileKey);
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            string content;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    content = streamReader.ReadToEnd();
                }
            }

            var lines = content.Split(NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (lines == null || lines.Length == 0)
            {
                return null;
            }

            var poList = new List<TValue>();
            for (int i = 1; i < lines.Length; i++)
            {
                var po = this._adapt(lines[i]);
                if (po == null)
                {
                    continue;
                }
                poList.Add(po);
            }

            return poList;
        }

        public void Save(string fileKey, List<TValue> poList, bool createNew)
        {
            if (poList == null || poList.Count == 0)
            {
                return;
            }

            //格式化
            StringBuilder sb = new StringBuilder();
            if (createNew)
            {
                sb.AppendFormat("{0}\r\n", this._title);
            }

            foreach (var po in poList)
            {
                sb.AppendFormat("{0}\r\n", po.ToString());
            }

            //持久化
            var filePathName = GetFile(fileKey, createNew);
            using (FileStream fileStream = new FileStream(filePathName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(sb);
                }
            }
        }

        public TValue GetLast(string fileKey)
        {
            var filePath = GetFile(fileKey);
            if (string.IsNullOrEmpty(filePath))
            {
                return default(TValue);
            }

            TValue po = default(TValue);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                long positon = fileStream.Length - 1;
                int count = 0;
                while (positon >= 0)
                {
                    fileStream.Position = positon;
                    if (fileStream.ReadByte() == '\n')
                    {
                        count++;
                        if (count == 2)
                        {
                            break;
                        }
                    }
                    positon--;
                }

                fileStream.Seek(positon + 1, SeekOrigin.Begin);
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    var line = streamReader.ReadLine();
                    po = this._adapt(line);
                }
            }

            return po;
        }

        private string GetFile(string fileKey, bool notExistsThenCreateNew = false)
        {
            //创建目录
            if (!Directory.Exists(this._directory))
            {
                Directory.CreateDirectory(this._directory);
            }

            //创建文件
            string filePathName = Path.Combine(this._directory, fileKey + ".txt");
            if (File.Exists(filePathName))
            {
                return filePathName;
            }
            else
            {
                if (notExistsThenCreateNew)
                {
                    File.Create(filePathName).Close();
                    return filePathName;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
