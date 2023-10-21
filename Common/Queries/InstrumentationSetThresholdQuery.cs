namespace Common.Queries
{
    public static class InstrumentationSetThresholdQuery
    {
        public const string InstrumentationNotDelete = @"
        SELECT * FROM DeviceInstrumentThreshold WHERE IsDelete = 0;";
    }
}
