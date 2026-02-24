using Metrics.MetricData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.MetricsExt
{
    public static class HistogramValueExt
    {
        public static bool IsValueEqual(this HistogramValue x, HistogramValue y)
        {
            return x.Count == y.Count
                && string.Equals(x.LastUserValue, y.LastUserValue)
                && x.LastValue == y.LastValue
                && x.Max == y.Max
                && string.Equals(x.MaxUserValue, y.MaxUserValue)
                && x.Mean == y.Mean
                && x.Median == y.Median
                && x.Min == y.Min
                && string.Equals(x.MinUserValue, y.MinUserValue)
                && x.Percentile75 == y.Percentile75
                && x.Percentile95 == y.Percentile95
                && x.Percentile98 == y.Percentile98
                && x.Percentile99 == y.Percentile99
                && x.Percentile999 == y.Percentile999
                && x.SampleSize == y.SampleSize
                && x.StdDev == y.StdDev;
        }
    }
}
