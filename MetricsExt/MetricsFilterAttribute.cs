using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Threading;
using System.Web.Http.Controllers;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Metrics;
using System.Configuration;
using System.Net.Http;

namespace dotnet.common.MetricsExt
{
    /// <summary>
    /// WebAPI接口过滤器
    /// 
    /// 记录接口耗时、频次，记录到Metrics
    /// </summary>
    public class MetricsFilterAttribute : ActionFilterAttribute
    {
        private const string StopWatchKey = "StopWatchKey";
        private readonly string appName;
        private readonly string globalMetricsContextName;
        private readonly ConcurrentDictionary<string, Histogram> HistogramMap = new ConcurrentDictionary<string, Histogram>();
        private readonly ConcurrentDictionary<string, Meter> MeterMap = new ConcurrentDictionary<string, Meter>();
        private readonly Regex actionRegex = new Regex(ConfigurationManager.AppSettings["Metrics.Actions"]);

        public MetricsFilterAttribute()
        {
            appName = ConfigurationManager.AppSettings["AppName"] ?? string.Empty;
            globalMetricsContextName = ConfigurationManager.AppSettings["Metrics.GlobalContextName"] ?? string.Empty;
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var metricsName = FormatMetricsName(actionContext.ActionDescriptor);
            if (actionRegex.IsMatch(metricsName))
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                actionContext.Request.Properties[StopWatchKey] = stopWatch;
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            object obj;
            if(actionExecutedContext.Request.Properties.TryGetValue(StopWatchKey,out obj))
            {
                var stopWatch = obj as Stopwatch;
                if (stopWatch != null)
                {
                    stopWatch.Stop();

                    var tags = new string[] { $"method={actionExecutedContext.Request.Method.ToString()}" };
                    var metricsName = FormatMetricsName(actionExecutedContext.ActionContext.ActionDescriptor);
                    //build and update histogram
                    var histogram = GetOrAddHistogram(metricsName, tags);
                    histogram.Update(stopWatch.ElapsedMilliseconds);
                }
            }

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }

        private string FormatMetricsName(HttpActionDescriptor actionDescriptor)
        {
            return string.Join(".", appName, actionDescriptor.ControllerDescriptor.ControllerName, actionDescriptor.ActionName);
        }

        private Histogram GetOrAddHistogram(string actionFullName, string[] tags)
        {
            if (!HistogramMap.TryGetValue(actionFullName, out Histogram histogram))
            {
                histogram = MyMetrics.Histogram(actionFullName, Unit.Custom("ms"), tags);
                HistogramMap.TryAdd(actionFullName, histogram);
            }
            return histogram;
        }
    }
}
