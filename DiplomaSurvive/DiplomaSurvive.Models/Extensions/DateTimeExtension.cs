using System;

namespace DiplomaSurvive.Models
{
    public static class DateTimeExtension
    {
        public static int ToUnixTime(this DateTime date)
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        
        public static DateTime ToDateTime(this int date)
        {
            var d = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            d = d.AddMilliseconds(date);
            return d;
        }
    }
}