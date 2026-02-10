using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TimeExt
{
    public class APIParamDateTypeAnalyzer
    {
        private static readonly char[] Separators = new char[] { '_' };



        /**
        * 约定：通过参数 指定接口中的时间区间。
        * 参数名：dateType
        * 参数类型：字符串
        * 参数值：分为约定话术和约定规则
        **/
        public static KeyValuePair<DateTime, DateTime> GetDates(string dateRemark)
        {
            var date = DateTime.Now;
            dateRemark = dateRemark.ToLower();
            switch (dateRemark)
            {
                case "today":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1));
                case "yesterday":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddSeconds(-1));
                case "day_before_yesterday":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-2), DateTime.Now.Date.AddDays(-1).AddSeconds(-1));
                case "the_day_so_far":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date, DateTime.Now);
                case "yesterday_to_date":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-1), DateTime.Now);

                case "this_week":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-(int)(DateTime.Now.DayOfWeek)), DateTime.Now.Date.AddDays(7 - (int)(DateTime.Now.DayOfWeek)).AddSeconds(-1));
                case "this_month":
                    return new KeyValuePair<DateTime, DateTime>(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1));
                case "this_year":
                    return new KeyValuePair<DateTime, DateTime>(new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year, 1, 1).AddYears(1).AddSeconds(-1));
                case "this_day_last_week":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-7), DateTime.Now.Date.AddDays(-6).AddSeconds(-1));

                case "week_to_date":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-(int)(DateTime.Now.DayOfWeek)), DateTime.Now);
                case "month_to_date":
                    return new KeyValuePair<DateTime, DateTime>(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateTime.Now);
                case "year_to_date":
                    return new KeyValuePair<DateTime, DateTime>(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);
               
                case "previous_week":
                    return new KeyValuePair<DateTime, DateTime>(DateTime.Now.Date.AddDays(-7 - (int)(DateTime.Now.DayOfWeek)), DateTime.Now.Date.AddDays(-(int)(DateTime.Now.DayOfWeek)).AddSeconds(-1));
                case "previous_month":
                    date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    return new KeyValuePair<DateTime, DateTime>(date.AddMonths(-1), date.AddSeconds(-1));
                case "previous_year":
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
                    //规则格式：Last_15_minutes
                    if (participles.Length != 3 || !double.TryParse(participles[1], out value))
                    {
                        return null;
                    }
                    var startDate = GetRegularLastTime(value, participles[2]);
                    return new KeyValuePair<DateTime, DateTime>(startDate, DateTime.Now);
                default:
                    return null;
            }
        }

        public static DateTime GetRegularLastTime(double value, string dateUnit)
        {
            switch (dateUnit)
            {
                case "minute":
                case "minutes":
                    return DateTime.Now.AddMinutes(-value);
                case "hour":
                case "hours":
                    return DateTime.Now.AddHours(-value);
                case "day":
                case "days":
                    return DateTime.Now.AddDays(-value + 1).Date;
                case "month":
                case "months":
                    var lastNMonth = DateTime.Now.AddMonths(-(int)value + 1).Date;
                    return new DateTime(lastNMonth.Year, lastNMonth.Month, 1);
                case "year":
                case "years":
                    var lastNYear = DateTime.Now.AddYears(-(int)value + 1);
                    return new DateTime(lastNYear.Year, 1, 1);
                default:
                    return DateTime.Now.Date;
            }
        }
    }
}
