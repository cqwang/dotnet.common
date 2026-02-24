using Metrics;
using Metrics.InfluxDB;
using Metrics.InfluxDB.Model;
using Metrics.MetricData;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.MetricsExt
{
    public class InfluxdbMyHttpReport : InfluxdbHttpReport
    {
        /// <summary>
        /// 记录上次的埋点数据
        /// </summary>
        private readonly ConcurrentDictionary<string, HistogramValue> lastHistogramQueue = new ConcurrentDictionary<string, HistogramValue>();


        public InfluxdbMyHttpReport(InfluxConfig config = null)
            : base(config)
        {
        }

        protected override void ReportHistogram(String name, HistogramValue value, Unit unit, MetricTags tags)
        {
            if (value.Count == 0)
            {
                return;
            }

            if (!lastHistogramQueue.TryGetValue(name, out HistogramValue last) || !last.IsValueEqual(value))
            {
                lastHistogramQueue[name] = value;
                base.ReportHistogram(name, value, unit, tags);
            }
        }
    }
}
