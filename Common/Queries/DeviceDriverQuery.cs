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
                    DDT.Manufacturer AS DeviceDriverTypeManufacturer,
                    DDT.ImageUrl AS DeviceDriverTypeImageUrl

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


        public const string GetTurnOnAndTurnOff = @"SELECT [Id]
                                                  ,[IsProblem]
                                                  ,[IsAction]
                                                  ,[IsAuto]
                                                  ,[IsDaily]
                                                  ,[OpenTimer]
                                                  ,[ShutDownTime]
                                              FROM [AgriculturalManagement].[dbo].[DeviceDriver]";
        public const string UpdateTurnOn = @"Update [DeviceDriver]
                                            Set IsAction = 1
                                            Where Id = @Id";
        public const string UpdateTurnOff = @"Update [DeviceDriver]
                                            Set IsAction = 0
                                            Where Id = @Id";
    }
}
