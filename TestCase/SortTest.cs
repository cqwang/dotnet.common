using Cqwang.BackEnd.CSharp.Algorithm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TestCase
{ 
    public partial class SortAlgorithm
    {
        /// <summary>
        /// 计数器列表
        /// </summary>
        private static Dictionary<string, Counter> counterMap = new Dictionary<string, Counter>();
        /// <summary>
        /// 当前计数器
        /// </summary>
        private static Counter logger;

        /// <summary>
        /// 交换数据项（这里是直接交换引用，也可以直接交换值）
        /// </summary>
        /// <param name="dataItems"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        private static void Swap(IList<DataItem> dataItems, int index1, int index2)
        {
            int loggerIndex = index1;

            DataItem temp = dataItems[index1];
            dataItems[index1] = dataItems[index2];
            logger.Increase(false, false, true, new KeyValuePair<int, int>(index2, index1));
            dataItems[index2] = temp;
            logger.Increase(false, false, true, new KeyValuePair<int, int>(loggerIndex, index2));
        }

        /// <summary>
        /// 开启并发时数据序列最小长度
        /// </summary>
        private const int ParallelMinDataCount = 2;


        /// <summary>
        /// 测试入口
        /// </summary>
        public static void DoRankTest()
        {
            //三组测试数据，正序、乱序、倒序
            int[][] testData = new int[3][];
            testData[0] = new int[] { 0, 1, 2, 3, 4, 5, 6, 9 };
            testData[1] = new int[] { 6, 2, 4, 1, 5, 9, 0, 3 };
            testData[2] = new int[] { 9, 6, 5, 4, 3, 2, 1, 0 };

            for (int i = 0; i < testData.Length; i++)
            {
                IList<DataItem> sourceDataItems = new List<DataItem>();
                foreach (var value in testData[i])
                {
                    sourceDataItems.Add(new DataItem() { Value = value });
                }

                DoRank(InsertSort, sourceDataItems);
                DoRank(BinaryInsertSort, sourceDataItems);
                DoRank(ShellSort, sourceDataItems);
                DoRank(CombSort, sourceDataItems);
                DoRank(BubbleSort, sourceDataItems);
                DoRank(CocktailSort, sourceDataItems);
                DoRank(GnomeSort, sourceDataItems);
                DoRank(OddEvenSort, sourceDataItems);
                DoRank(QuickSort, sourceDataItems);
                DoRank(SimpleSelectSort, sourceDataItems);
                DoRank(BigRootHeapSort, sourceDataItems);
                DoRank(MergeSort, sourceDataItems);
                DoRank(BucketSort, sourceDataItems);

                ShowMessage(sourceDataItems);
                counterMap.Clear();
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        private static void DoRank(Action<IList<DataItem>> action, IList<DataItem> sourceDataItems)
        {
            IList<DataItem> dataItems = new List<DataItem>();
            foreach (var item in sourceDataItems)
            {
                dataItems.Add(item);
            }

            logger = new Counter(action.Method.Name);
            counterMap.Add(logger.Name, logger);
            action(dataItems);
        }

        private static void ShowMessage(IList<DataItem> sourceDataItems)
        {
            foreach (var methodName in counterMap.Keys)
            {
                var counter = counterMap[methodName];
                IList<DataItem> dataItems = new List<DataItem>();
                foreach (var item in sourceDataItems)
                {
                    dataItems.Add(item);
                }
                var additional = counter.Name.Equals("BucketSort") ? bucketSortTempDataItems : null;
                Console.Write(counter.GenerateMoveSteps(dataItems, additional));
            }

            int maxLength = counterMap.Max(p => p.Key.Length);
            string header = string.Format("{0}\t{1}{2}{3}", FormatLength("Count:", maxLength), FormatLength("Traversal", maxLength), FormatLength("Compare", maxLength), FormatLength("Move", maxLength));
            Console.WriteLine(header);
            foreach (var counter in counterMap.Values)
            {
                Console.WriteLine(string.Format("{0}\t{1}{2}{3}",
                    FormatLength(counter.Name, maxLength), FormatLength(counter.TraversalCount, maxLength), FormatLength(counter.CompareCount, maxLength), FormatLength(counter.MoveCount, maxLength)));
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
