using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace dotnet.common.Tool
{
    public class FileUsingSort
    {
        private static readonly string[] ContentSeparator = new string[] { "\r\n" };
        private static readonly string[] LineSeparator = new string[] { "=", " " };

        /// <summary>
        /// 分析目录文件内容，获取所有的using key，有序显示
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Analyse(string path, string searchPattern, string encoding)
        {
            var results = new HashSet<string>();
            var files = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            foreach (string filePath in files)
            {
                if (filePath.EndsWith("AssemblyInfo.cs"))
                {
                    continue;
                }

                var content = File.ReadAllText(filePath, Encoding.GetEncoding(encoding));
                var lines = content.Split(ContentSeparator, StringSplitOptions.None);
                foreach (var line in lines)
                {
                    if (line.Trim().StartsWith("namespace "))
                    {
                        break;
                    }
                    if (line.Trim().StartsWith("using "))
                    {
                        var items = line.Split(LineSeparator, StringSplitOptions.RemoveEmptyEntries);
                        var item = items.Last();
                        results.Add(item);
                    }
                }
            }

            var resultList = results.ToList();
            resultList.Sort();
            return string.Join("\r\n", resultList);
        }


        /// <summary>
        /// 格式化using，更新文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <param name="encoding"></param>
        /// <param name="groupTags"></param>
        public static bool UpdateFiles(string path, string searchPattern, string encoding, string groupTagStr)
        {
            var groupTags = groupTagStr.Split(ContentSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (groupTags == null || groupTags.Length == 0)
            {
                return false;
            }

            var files = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
            {
                return false;
            }

            foreach (string filePath in files)
            {
                if (filePath.EndsWith("AssemblyInfo.cs"))
                {
                    continue;
                }

                var content = File.ReadAllText(filePath, Encoding.GetEncoding(encoding));
                try
                {
                    content = FormatContent(filePath, content, groupTags);
                }
                catch
                {

                }
                if (!string.IsNullOrEmpty(content))
                {
                    File.WriteAllText(filePath, content, Encoding.GetEncoding(encoding));
                }
            }

            return true;
        }

        private static string FormatContent(string filePath, string content, string[] groupTags)
        {
            string[] lines = content.Split(ContentSeparator, StringSplitOptions.None);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            Dictionary<string, string> repeatedDict = new Dictionary<string, string>();
            int nextIndex = 0;
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("namespace "))
                {
                    break;
                }

                if (line.Trim().StartsWith("using "))
                {
                    string[] items = line.Split(LineSeparator, StringSplitOptions.RemoveEmptyEntries);
                    var item = items.Last();
                    if (dict.ContainsKey(item))
                    {
                        if (!line.Trim().Equals(dict[item].Trim()))
                        {
                            repeatedDict.Add(item, line);
                        }
                    }
                    else
                    {
                        dict.Add(item, line);
                    }
                }

                nextIndex++;
            }

            if (dict.Count == 0)
            {
                return string.Empty;
            }

            int index = content.IndexOf(lines[nextIndex]);

            //分组
            Dictionary<string, HashSet<string>> groups = new Dictionary<string, HashSet<string>>();
            HashSet<string> others = new HashSet<string>();
            foreach (var tag in groupTags)
            {
                groups.Add(tag, new HashSet<string>());
            }
            groups.Add("for others", new HashSet<string>());//未枚举的其它using，放入一组。

            foreach (var key in dict.Keys)
            {
                bool find = false;
                foreach (var tag in groups.Keys)
                {
                    if (key.StartsWith(tag, StringComparison.CurrentCultureIgnoreCase))
                    {
                        groups[tag].Add(dict[key]);
                        if (repeatedDict.ContainsKey(key))
                        {
                            groups[tag].Add(repeatedDict[key]);
                        }
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    groups["for others"].Add(dict[key]);
                }
            }

            //排序
            var compareDelegate = new Comparison<string>((x, y) =>
            {
                return x.Length - y.Length;
            });

            StringBuilder sb = new StringBuilder();
            foreach (var itemSet in groups.Values)
            {
                var itemList = itemSet.ToList();
                itemList.Sort(compareDelegate);
                AppendItems(sb, itemList);
            }
            sb.Append(content.Substring(index));
            return sb.ToString();
        }

        private static void AppendItems(StringBuilder sb, List<string> items)
        {
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    sb.AppendFormat("{0}\r\n", item);
                }
                sb.Append("\r\n");
            }
        }
    }
}
