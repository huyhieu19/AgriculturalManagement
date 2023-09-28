namespace Common.Queries
{
    public static class TimerDeviceDriverQuery
    {
        public const string GetAllTimerSQL = @"SELECT
                    *
                    FROM TimerDeviceDriver";
        public const string GetAllTimerByDeviceDriverSQL = @"SELECT * FROM TimerDeviceDriver WHERE DeviceDriverId = @DeviceDriverId";
        public const string GetAllHistorySQL = @"SELECT * FROM TimerDeviceDriver WHERE IsRemove = 0";
        public const string GetAllHistoryByDeviceIdSQL = @"SELECT * FROM TimerDeviceDriver WHERE IsRemove = 0 AND DeviceDriverId = @DeviceDriverId";
        public const string RemoveTimerSQL = @"UPDATE TimerDeviceDriver SET IsRemove = 1 WHERE Id = @Id";
        public const string UpdateTimerSQL = @"UPDATE TimerDeviceDriver SET IsDaily = @IsDaily,IsAuto = @IsAuto, ShutDownTimer = @ShutDownTimer, OpenTimer = @OpenTimer  WHERE Id = @Id";

        public const string UpdateTurnOnSQL = @"Update [DeviceDriver]
                                            Set IsAction = 1
                                            Where Id = @Id";
        public const string UpdateTurnOffSQL = @"Update [DeviceDriver]
                                            Set IsAction = 0
                                            Where Id = @Id";
    }
}
