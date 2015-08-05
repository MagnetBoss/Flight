using System;

namespace FlyLight.Tools.Date
{
    public static class UnixTimeToDateTime
    {
        public static DateTime ConvertUnixToDateTime(long unixTime)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            date = date.AddSeconds(unixTime);
            return date;
        }
    }
}
