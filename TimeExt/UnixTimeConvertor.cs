using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TimeExt
{
    public class UnixTimeConvertor
    {
        private static readonly DateTime BeginTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        private const int Unit = 10000000;

        public static long ToUnix(DateTime dateTime)
        {
            return (dateTime - BeginTime).Ticks / Unit;
        }

        public static DateTime ToDateTime(long unixTicks)
        {
            return BeginTime.AddTicks(unixTicks * Unit);
        }
    }
}
