using System;

namespace DiplomaSurvive.Models
{
    public static class DateTimeExtension
    {
        public static int ToUnixTime(this DateTime date)
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}