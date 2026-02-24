using Metrics.MetricData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.MetricsExt
{
    public static class MeterValueExt
    {
        public static bool IsValueEqual(this MeterValue x, MeterValue y)
        {
            return x.Count == y.Count
                && x.FifteenMinuteRate == y.FifteenMinuteRate
                && x.FiveMinuteRate == y.FiveMinuteRate
                && x.MeanRate == y.MeanRate
                && x.OneMinuteRate == y.OneMinuteRate
                && x.RateUnit == y.RateUnit;
        }
    }
}
