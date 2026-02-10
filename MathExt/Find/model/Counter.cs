using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet.common.MathExt
{
    /// <summary>
	/// 计数器
	/// </summary>
	public class Counter
    {
        public string Name { get; private set; }

        private int _traversalCount;
        /// <summary>
        /// 数据项遍历次数
        /// </summary>
        public int TraversalCount
        {
            get { return _traversalCount; }
        }

        private int _compareCount;
        /// <summary>
        /// 数据项比较次数
        /// </summary>
        public int CompareCount
        {
            get { return _compareCount; }
        }

        private int _moveCount;
        /// <summary>
        /// 数据项移动次数
        /// </summary>
        public int MoveCount
        {
            get { return _moveCount; }
        }

        public StringBuilder Remark { get; private set; }

        /// <summary>
        /// 数据项移动轨迹
        /// </summary>
        public List<KeyValuePair<int, int>> Trace
        {
            get;
            private set;
        }

        public Counter(string name)
        {
            this.Name = name;
            this.Trace = new List<KeyValuePair<int, int>>();
            this.Remark = new StringBuilder();
        }

        /// <summary>
        /// 递增计数，记录变动痕迹（仅供参考）
        /// </summary>
        /// <param name="isTraversal"></param>
        /// <param name="isCompare"></param>
        /// <param name="isSwap"></param>
        /// <param name="moveStep"></param>
        public void Increase(bool isTraversal = false, bool isCompare = false, bool isSwap = false, KeyValuePair<int, int>? moveStep = null)
        {
            if (isTraversal)
            {
                Interlocked.Increment(ref this._traversalCount);
            }
            if (isCompare)
            {
                Interlocked.Increment(ref this._compareCount);
            }
            if (isSwap)
            {
                Interlocked.Increment(ref this._moveCount);
            }
            if (moveStep != null)
            {
                lock (((ICollection)this.Trace).SyncRoot)
                {
                    this.Trace.Add(moveStep.Value);
                }
            }
        }

        /// <summary>
        /// 演示数据序列变化
        /// </summary>
        public string GenerateMoveSteps(IList<DataItem> dataItems, IList<DataItem> bucketSortTempDataItems = null)
        {
            if (this.Trace.Count == 0)
            {
                return string.Empty;
            }

            Dictionary<int, DataItem> unstoredDataItemMap = new Dictionary<int, DataItem>();//尚未存储的数据项
            HashSet<int> storedDataItems = new HashSet<int>();//已经存储的数据项

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\r\n", this.Name);
            sb.AppendFormat("Init:\t{0}\r\n", string.Join(" ", dataItems.Select(p => p.Value.ToString())));

            bool isSpecial = false;
            if (this.Name.Equals("MergeSort"))
            {
                isSpecial = true;
            }
            else if (this.Name.Equals("BucketSort"))
            {
                isSpecial = true;
                if (bucketSortTempDataItems != null && bucketSortTempDataItems.Count > 0)
                {
                    sb.AppendFormat("桶:\t{0}\r\n", string.Join(" ", bucketSortTempDataItems.Select(p => (p == null) ? " " : p.Value.ToString())));
                }
            }

            for (int index = 0; index < this.Trace.Count; index++)
            {
                var step = this.Trace[index];
                DataItem swappingDataItem;

                if (isSpecial)
                {
                    if (storedDataItems.Add(step.Key))
                    {
                        unstoredDataItemMap.Add(step.Value, dataItems[step.Value]);
                    }
                    else
                    {
                        storedDataItems.Clear();
                        storedDataItems.Add(step.Key);
                    }

                    if (!unstoredDataItemMap.TryGetValue(step.Key, out swappingDataItem))
                    {
                        swappingDataItem = dataItems[step.Key];
                        unstoredDataItemMap[step.Value] = dataItems[step.Value];
                    }

                    foreach (var item in storedDataItems)
                    {
                        unstoredDataItemMap.Remove(item);
                    }
                }
                else
                {
                    if (unstoredDataItemMap.TryGetValue(step.Key, out swappingDataItem))
                    {
                        unstoredDataItemMap.Clear();
                    }
                    else
                    {
                        swappingDataItem = dataItems[step.Key];
                        unstoredDataItemMap.Add(step.Value, dataItems[step.Value]);
                    }
                }

                dataItems[step.Value] = swappingDataItem;
                sb.AppendFormat("{0}:\t{1}\r\n", (index + 1).ToString(), string.Join(" ", dataItems.Select(p => p.Value.ToString())));
            }

            return sb.ToString();
        }
    }
}
