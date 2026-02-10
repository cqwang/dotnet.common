using Cqwang.BackEnd.CSharp.Algorithm.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TestCase
{
    public partial class FindAlgorithm
    {
        private const int Invalid = -1;

        /// <summary>
        /// 计数器列表
        /// </summary>
        private static Dictionary<string, Counter> counterMap = new Dictionary<string, Counter>();
        /// <summary>
        /// 当前计数器
        /// </summary>
        private static Counter logger;

        /// <summary>
        /// 测试入口
        /// </summary>
        public static void DoFindTest()
        {
            HashSet<int> excludeKeys = new HashSet<int>() { 3, 6, 9 };
            IList<DataItem> dataItems = new List<DataItem>();
            for (int value = 100; value < 10000000; value++)
            {
                if (excludeKeys.Contains(value % 10))
                {
                    continue;
                }

                dataItems.Add(new DataItem() { Value = value });
            }

            //指定关键字的查找
            DataItem[] testData = new DataItem[] { new DataItem() { Value = 100 }, new DataItem() { Value = 725 }, new DataItem() { Value = 7830215 } };
            foreach (var item in testData)
            {
                DoFindIndex(SequenceFind, dataItems, item);
                DoFindIndex(BinaryFind, dataItems, item);
                DoFindIndex(BinaryOffsetFind, dataItems, item);
                ShowMessage(dataItems);
                counterMap.Clear();
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        private static void DoFindIndex(Func<IList<DataItem>, DataItem, int> action, IList<DataItem> dataItems, DataItem targetItem)
        {
            logger = new Counter(action.Method.Name);
            counterMap.Add(logger.Name, logger);

            Stopwatch w = new Stopwatch();
            w.Start();
            int position = action(dataItems, targetItem);
            w.Stop();

            logger.Remark.AppendFormat("{0}, Pos: {1},Time: {2}", targetItem.Value.ToString(), position.ToString(), w.ElapsedMilliseconds.ToString());
        }

        private static void ShowMessage(IList<DataItem> dataItems)
        {
            int maxLength = counterMap.Max(p => p.Key.Length);
            string header = string.Format("{0}\t{1}{2}", FormatLength("Count:", maxLength), FormatLength("Traversal", maxLength), FormatLength("Compare", maxLength));
            Console.WriteLine(header);
            foreach (var counter in counterMap.Values)
            {
                Console.WriteLine(string.Format("{0}\t{1}{2}{3}",
                    FormatLength(counter.Name, maxLength), FormatLength(counter.TraversalCount, maxLength), FormatLength(counter.CompareCount, maxLength), counter.Remark));
            }
        }

        private static string FormatLength(string str, int targetLength)
        {
            if (str.Length < targetLength)
            {
                str += new string(new char[targetLength - str.Length]);
            }

            return str;
        }

        private static string FormatLength(int count, int targetLength)
        {
            return FormatLength(count.ToString(), targetLength);
        }
    }
}
