namespace Common.Queries
{
    public static class DeviceDriverQuery
    {
        public const string GetDeviceDriverByZoneSQL = @"SELECT
                    DD.*,

                    F.Name AS FarmName,

                    Z.ZoneName,
                    Z.Description AS ZoneDescription,

                    DDT.Name AS DeviceDriverTypeName,
                    DDT.Manufacturer AS DeviceDriverTypeManufacturer

                FROM
                    DeviceDriver DD
                INNER JOIN
                    Zone Z
                ON
                    DD.ZoneId = Z.Id
                INNER JOIN
                    Farm F
                ON
                    F.Id = Z.FarmId
                LEFT JOIN
                    DeviceDriverType DDT
                ON
                    DD.DeviceDriverTypeId = DDT.Id
                WHERE
                    Z.Id = @ZoneId";


        public const string GetTurnOnAndTurnOffSQL = @"SELECT [Id]
                          ,[IsDaily]
                          ,[IsAuto]
                          ,[ShutDownTimer]
                          ,[OpenTimer]
                          ,[DeviceDriverId]
                      FROM [AgriculturalManagement].[dbo].[TimerDeviceDriver]";

    }
}
