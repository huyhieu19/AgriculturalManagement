namespace Common.Queries
{
    public static class InstrumentationQuery
    {
        // gỡ máy khỏi 1 zone cụ thể và chuyển sang zone khác
        public const string ZoneIdToNullSQL = @"Update Instrumentation
                                            Set ZoneId = null
                                            Where Id = @Id";
        // Cập nhật thông tin của thiết bị đo (only infomation)
        public const string UpdateInfoSQL = @"Update Instrumentation
                        Set Name = @Name, Note = @Note, Description = @Description,  IsActive = @IsActive, DateStartedUsing = @dateStartedUsing, ZoneId = @ZoneId, InstrumentationTypeId = @InstrumentationTypeId, EspId = @EspId, Gpio = @Gpio
                        Where Id = @Id";

        // Lấy ra bảng liên quan tới zone đã được gán vào zone cụ thể
        public const string GetInstrumentationByZoneSQL = @"SELECT
                        Ins.*,

                        InsT.Name AS InstrumentationTypeName,
                        InsT.Manufacturer AS InstrumentationTypeManufacturer,
                        InsT.Unit AS InstrumentationTypeUnit,
                        Z.ZoneName,
                        Z.Description AS ZoneDescription,

                        F.Name AS FarmName

                        FROM
                            Instrumentation Ins
                        INNER JOIN
                            Zone Z
                        ON
                            Ins.ZoneId = Z.Id
                        INNER JOIN
                            Farm F
                        ON
                            Z.FarmId = F.Id
                        LEFT JOIN
                            InstrumentationType InsT
                        ON
                            Ins.InstrumentationTypeId = InsT.Id
                        WHERE
                            Z.Id = @ZoneId";
    }
}