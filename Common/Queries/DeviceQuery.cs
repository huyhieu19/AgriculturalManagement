﻿namespace Common.Queries
{
    public static class DeviceQuery
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

        public const string GetTimerAvailableOfUserSQL =
        @"SELECT 
                TDD.*,
		        Ex.IsActionDevice
            FROM 
                TimerDeviceDriver TDD
            INNER JOIN
               (Select ModuleId, M.UserId, D.IsAction AS IsActionDevice, D.Id AS DeviceId From Device AS D
			   INNER JOIN
			   Module AS M
			   ON M.Id = D.ModuleId
			   WHERE UserId = @UserId
			   ) AS Ex
            ON 
                Ex.DeviceId = TDD.DeviceId

            WHERE TDD.IsSuccess = 0 AND IsRemove = 0";

        public const string GetAllTimerAvailable =
            @"SELECT 
                TDD.*,
		        D.IsAction,
				D.IsAuto,
				D.DeviceType,
				D.NameRef

            FROM 
                TimerDeviceDriver TDD
            INNER JOIN
               Device AS D
            ON 
                D.Id = TDD.DeviceId

            WHERE TDD.IsSuccess = 0 AND IsRemove = 0";


        public const string GetAllHistorySQL = @"SELECT * FROM TimerDeviceDriver WHERE IsRemove = 0";

        public const string GetAllHistoryByDeviceIdSQL = @"SELECT * FROM TimerDeviceDriver WHERE IsRemove = 0 AND DeviceDriverId = @DeviceDriverId";

        public const string RemoveTimerSQL = @"UPDATE TimerDeviceDriver SET IsRemove = 1 WHERE Id = @Id AND DeviceDriverId = @DeviceId";

        public const string SuccessTimerSQL = @"UPDATE TimerDeviceDriver SET IsSuccess = 1, IsRemove = 1 WHERE Id = @Id AND DeviceDriverId = @DeviceId";

        public const string UpdateTimerSQL = @"UPDATE TimerDeviceDriver SET IsDaily = @IsDaily,IsAuto = @IsAuto, ShutDownTimer = @ShutDownTimer, OpenTimer = @OpenTimer  WHERE Id = @Id";



        public const string UpdateTurnOnSQL = @"Update [Device]
                                            Set IsAction = 1
                                            Where Id = @Id";

        public const string UpdateTurnOffSQL = @"Update [Device]
                                            Set IsAction = 0
                                            Where Id = @Id";

    }
}