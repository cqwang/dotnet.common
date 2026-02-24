using Metrics;
using Metrics.InfluxDB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.SDK.Metrics.TestCase
{
    class TestConfig
    {
        static void Test()
        {
            InitMetricsConfig();
            TestDefault.Test();

            Console.ReadKey();
        }


        private static void InitMetricsConfig()
        {
            var address = "http://localhost:5678/metrics/";
            Metric.Config.WithHttpEndpoint(address) //Web监视仪表板，提供Metrics.NET度量值图表，浏览器打开这个地址可以访问一个Metrics.NET内置的页面
                .WithAllCounters().WithInternalMetrics()
                .WithReporting((reportsConfig) => // 配置报表输出
                {
                    reportsConfig.WithConsoleReport(TimeSpan.FromSeconds(5)); //报表输出到控制台
                });
            Process.Start(address);
        }

        private void InitMetricsCSV()
        {
            Metric.Config
                .WithHttpEndpoint(ConfigurationManager.AppSettings["Metrics.HttpListener.HttpUriPrefix"])
                .WithAllCounters()
                .WithInternalMetrics()
                .WithReporting(config => config
                    .WithConsoleReport(TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["Metrics.Console.Interval.Seconds"])))
                    .WithCSVReports(ConfigurationManager.AppSettings["Metrics.CSV.Path"], TimeSpan.FromMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["Metrics.CSV.Interval.Seconds"])))
                    .WithTextFileReport(ConfigurationManager.AppSettings["Metrics.TextFile.Path"], TimeSpan.FromMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["Metrics.TextFile.Interval.Minutes"])))
            );
        }

        private static void InitMetricsWithInfluxDBConfig()
        {
            Metric.Config.WithReporting(report => report
                .WithInfluxDbHttp("localhost", 8086, "metrics", TimeSpan.FromSeconds(1)));
        }
    }
}
