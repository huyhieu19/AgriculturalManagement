namespace Common
{
    public static class SetTimeZone
    {
        private static readonly DateTime TimeVN = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "SE Asia Standard Time");
        private static readonly DateTime TimeZone = DateTime.Now.AddHours(7);
        public static DateTime GetTimeZone() { return TimeZone; }
        public static DateTime GetDateTimeVN() { return TimeVN; }
    }
}