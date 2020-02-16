using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalRegimentManager.Util
{
    public static class DateTimeHelper
    {
        public static DateTime getWeekStart()
        {
            int weekday = (int)DateTime.Now.DayOfWeek;
            if (weekday == 0) weekday = 7;
            return DateTime.Now.AddDays((weekday - 1) * -1);
        }
        public static DateTime getWeekEnd()
        {
            int weekday = (int)DateTime.Now.DayOfWeek;
            if (weekday == 0) weekday = 7;
            return DateTime.Now.AddDays(7 - weekday);
        }

        public static string getWeekStartStr()
        {
            return getWeekStart().ToString("yyyy-MM-dd") + " 00:00:00";
        }
        public static string getWeekEndStr()
        {
            return getWeekEnd().ToString("yyyy-MM-dd") + " 23:59:59";
        }

    }
}
