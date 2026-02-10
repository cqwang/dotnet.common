using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TimeExt
{
    /// <summary>
    /// ELK快捷日期解析器
    /// 
    /// 
    /// this year
    /// 
    /// </summary>
    public class ElkDateParticipleAnalyser
    {
        private static readonly char[] Separators = new char[] { ' ' };


        public static KeyValuePair<DateTime, DateTime> GetDates(string dateRemark)
        {
            var date = DateTime.Now;
            switch (dateRemark)
            {
                case "today":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1));
                case "this week":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-(int)(DateTime.Now.DayOfWeek)), DateTime.Now.Date.AddDays(7 - (int)(DateTime.Now.DayOfWeek)).AddSeconds(-1));
                case "this month":
                    return new KeyValuePair<DateTime, DateTime>(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1));
                case "this year":
                    return new KeyValuePair<DateTime, DateTime>(new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year, 1, 1).AddYears(1).AddSeconds(-1));
                case "the day so far":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date, DateTime.Now);
                case "yesterday to date":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-1), DateTime.Now);
                case "week to date":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-(int)(DateTime.Now.DayOfWeek)), DateTime.Now);
                case "month to date":
                    return new KeyValuePair<DateTime, DateTime>(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateTime.Now);
                case "year to date":
                    return new KeyValuePair<DateTime, DateTime>(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);
                case "yesterday":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddSeconds(-1));
                case "day before yesterday":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-2), DateTime.Now.Date.AddDays(-1).AddSeconds(-1));
                case "this day last week":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-7), DateTime.Now.Date.AddDays(-6).AddSeconds(-1));
                case "previous week":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-7 - (int)(DateTime.Now.DayOfWeek)), DateTime.Now.Date.AddDays(-(int)(DateTime.Now.DayOfWeek)).AddSeconds(-1));
                case "previous month":
                    date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    return new KeyValuePair<DateTime, DateTime>(date.AddMonths(-1), date.AddSeconds(-1));
                case "previous year":
                    date = new DateTime(DateTime.Now.Year, 1, 1);
                    return new KeyValuePair<DateTime, DateTime>(date.AddYears(-1), date.AddSeconds(-1));
                default:
                    var dates = GetRegularDates(dateRemark);
                    if (dates.HasValue)
                    {
                        return dates.Value;
                    }
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1));
            }
        }

        public static KeyValuePair<DateTime, DateTime>? GetRegularDates(string dateRemark)
        {
            if (string.IsNullOrEmpty(dateRemark))
            {
                return null;
            }

            var participles = dateRemark.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            if (participles == null || participles.Length == 0)
            {
                return null;
            }

            double value;
            switch (participles[0])
            {
                case "last":
                    //规则格式：Last 15 minutes
                    if (participles.Length != 3 || !double.TryParse(participles[1], out value))
                    {
                        return null;
                    }
                    var func = GetDateOperateFunc(participles[2]);
                    return new KeyValuePair<DateTime, DateTime>(func(-value), DateTime.Now);
                default:
                    return null;
            }
        }

        private static Func<double, DateTime> GetDateOperateFunc(string dateUnit)
        {
            switch (dateUnit)
            {
                case "minute":
                case "minutes":
                    return DateTime.Now.AddMinutes;
                case "hour":
                case "hours":
                    return DateTime.Now.AddHours;
                case "day":
                case "days":
                    return DateTime.Now.AddDays;
                case "month":
                case "months":
                    return (value) => DateTime.Now.AddMonths((int)value);
                case "year":
                case "years":
                    return (value) => DateTime.Now.AddYears((int)value);
                default:
                    return null;
            }
        }
    }
}
