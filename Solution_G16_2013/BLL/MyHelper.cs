using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G16_2013.BLL
{
    public class MyHelper
    {
        public static DateTime GetTaipeiTimeFromUtc(DateTime utcTime)
        {
            string taipeiTimeZoneId = "Taipei Standard Time";
            TimeZoneInfo taipeiTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(taipeiTimeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, taipeiTimeZoneInfo);

        }
    }
}
