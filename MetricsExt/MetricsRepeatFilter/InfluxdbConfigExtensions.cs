using Metrics.InfluxDB;
using Metrics.InfluxDB.Model;
using Metrics.MetricData;
using Metrics.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.MetricsExt
{
    public static class InfluxdbConfigExtensions
    {
        public static MetricsReports WithInfluxDbMyHttp(this MetricsReports reports, String host, UInt16? port, String database, String username, String password, String retentionPolicy, InfluxPrecision? precision, TimeSpan interval, MetricsFilter filter = null, Action<InfluxConfig> configFunc = null)
        {
            return reports.WithInfluxDbMyHttp(new InfluxConfig(host, port, database, username, password, retentionPolicy, precision), interval, filter, configFunc);
        }

        public static MetricsReports WithInfluxDbMyHttp(this MetricsReports reports, String host, UInt16? port, String database, TimeSpan interval, MetricsFilter filter = null, Action<InfluxConfig> configFunc = null)
        {
            return reports.WithInfluxDbMyHttp(new InfluxConfig(host, port, database), interval, filter, configFunc);
        }

        public static MetricsReports WithInfluxDbMyHttp(this MetricsReports reports, InfluxConfig config, TimeSpan interval, MetricsFilter filter = null, Action<InfluxConfig> configFunc = null)
        {
            InfluxConfig conf = config ?? new InfluxConfig();
            configFunc?.Invoke(conf);
            return reports.WithReport(new InfluxdbMyHttpReport(conf), interval, filter);
        }
    }
}
