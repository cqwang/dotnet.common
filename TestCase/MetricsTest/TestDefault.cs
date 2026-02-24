using Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cqwang.SDK.Metrics.TestCase
{
    class TestDefault
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        public static void Test()
        {
            while(true)
            {
                GaugeTest();
                CounterTest();
                HistogramTest();
                MeterTest();
                TimerTest();
                HealthCheckTest();
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// 最简单的度量，读取一个即时值。
        /// </summary>
        private static void GaugeTest()
        {
            Metric.Gauge("test.gauge", () => random.NextDouble() * 1000, Unit.None);

            var cities = new List<string>();
            Metric.Gauge("Service Cities Count", () => cities.Count, Unit.Custom("个"));
        }

        /// <summary>
        /// 计数器 从0开始
        /// </summary>
        static void CounterTest()
        {
            var counter = Metric.Counter("test.counter", Unit.Custom("并发"));

            Action doWork = () => { Thread.Sleep(random.Next(10, 300)); };
            Action idlesse = () => { Thread.Sleep(random.Next(0, 500)); };
            for (var i = 0; i < 20; i++)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        counter.Increment();
                        doWork();
                        counter.Decrement();
                        idlesse();
                    }
                });
            }
        }

        /// <summary>
        /// 直方图，采样计算最大/小值、平均值、方差、百分比
        /// </summary>
        static void HistogramTest()
        {
            var histogram = Metric.Histogram("test.histogram", Unit.Custom("岁"), SamplingType.LongTerm);
            var histogramOfData = Metric.Histogram("ResultsExample", Unit.Items);
            Task.Run(() =>
            {
                while (true)
                {
                    histogram.Update(random.Next(10, 80), random.Next(0, 2) > 0 ? "男" : "女");
                    histogramOfData.Update(random.Next(10, 80), "请求字节数");
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            });
        }

        /// <summary>
        /// 事件在一段时间内发生的次数，频率=总次数/总时间
        /// </summary>
        static void MeterTest()
        {
            var meter = Metric.Meter("test.meter", Unit.Calls, TimeUnit.Seconds);

            Action idlesse = () => { Thread.Sleep(random.Next(20, 50)); };
            Task.Run(() =>
            {
                while (true)
                {
                    meter.Mark();
                    idlesse();
                }
            });
        }

        /// <summary>
        /// 在meter的基础上，统计了每个业务处理的耗时的histogram度量信息，同时记录并发数。
        /// </summary>
        static void TimerTest()
        {
            var timer = Metric.Timer("test.meter", Unit.None, SamplingType.SlidingWindow, TimeUnit.Seconds, TimeUnit.Microseconds);
            var timer2 = Metric.Timer("BookingAPI.Request", Unit.Requests);
            Action doWork = () => { Thread.Sleep(random.Next(10, 300)); };
            Action idlesse = () => { Thread.Sleep(random.Next(0, 500)); };
            for (var i = 0; i < 20; i++)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        timer.Time(doWork);
                        idlesse();
                    }

                    using (timer2.NewContext(i.ToString()))
                    {

                    }
                });

            }
        }

        /// <summary>
        /// 服务健康检查
        /// </summary>
        static void HealthCheckTest()
        {
            HealthChecks.RegisterHealthCheck("test.healthcheck", () =>
            {
                return random.Next(100) < 5 ? HealthCheckResult.Unhealthy() : HealthCheckResult.Healthy();
            });
        }
    }
}
