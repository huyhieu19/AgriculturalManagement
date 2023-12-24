namespace Common.Queries
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
                          ,[DeviceId]
                      FROM [AgriculturalManagement].[dbo].[TimerDeviceDriver]";

        public const string GetTimerAvailableOfUserSQL =
        @"SELECT 
                TDD.*,
		        Ex.IsActionDevice,
				Ex.DeviceName As DeviceName,
				Ex.ModuleId
            FROM 
                TimerDeviceDriver TDD
            INNER JOIN
               (Select ModuleId, M.UserId, D.IsAction AS IsActionDevice, D.Id AS DeviceId, D.Name AS DeviceName From Device AS D
			   INNER JOIN
			   Module AS M
			   ON M.Id = D.ModuleId
			   WHERE UserId = @UserId
			   ) AS Ex
            ON 
                Ex.DeviceId = TDD.DeviceId

            WHERE (TDD.IsSuccessON = 0 OR TDD.IsSuccessOFF = 0) AND IsRemove = 0";

        public const string GetAllTimerAvailable =
            @"SELECT 
                TDD.*,
		        D.IsAction,
				D.IsAuto,
				D.DeviceType,
				D.NameRef,
                D.ModuleId
            FROM 
                TimerDeviceDriver TDD
            INNER JOIN
               Device AS D
            ON 
                D.Id = TDD.DeviceId

            WHERE (TDD.IsSuccessON = 0 OR TDD.IsSuccessOFF = 0) AND IsRemove = 0";

        public const string GetTimerHistoryOfUserSQL =
        @"SELECT 
                TDD.*,
		        Ex.IsActionDevice,
				Ex.DeviceName As DeviceName,
				Ex.ModuleId
            FROM 
                TimerDeviceDriver TDD
            INNER JOIN
               (Select ModuleId, M.UserId, D.IsAction AS IsActionDevice, D.Id AS DeviceId, D.Name AS DeviceName From Device AS D
			   INNER JOIN
			   Module AS M
			   ON M.Id = D.ModuleId
			   WHERE UserId = @UserId
			   ) AS Ex
            ON 
                Ex.DeviceId = TDD.DeviceId";

        public const string GetAllHistorySQL = @"SELECT * FROM TimerDeviceDriver WHERE IsRemove = 0";

        public const string GetAllHistoryByDeviceIdSQL = @"SELECT * FROM TimerDeviceDriver WHERE IsRemove = 0 AND [DeviceId] = @DeviceDriverId";

        public const string RemoveTimerSQL = @"UPDATE TimerDeviceDriver SET IsRemove = 1 WHERE Id = @Id AND [DeviceId] = @DeviceId";

        public const string SuccessJobTurnOnDeviceTimerSQL = @"UPDATE TimerDeviceDriver SET IsSuccessON = 1 WHERE Id = @Id AND [DeviceId] = @DeviceId";
        public const string SuccessJobTurnOffDeviceTimerSQL = @"UPDATE TimerDeviceDriver SET IsSuccessOFF = 1 WHERE Id = @Id AND [DeviceId] = @DeviceId";

        public const string UpdateTimerSQL = @"UPDATE TimerDeviceDriver SET IsDaily = @IsDaily,IsAuto = @IsAuto, ShutDownTimer = @ShutDownTimer, OpenTimer = @OpenTimer  WHERE Id = @Id";



        public const string UpdateTurnOnOffSQL = @"Update [Device]
                                            Set IsAction = @IsAction
                                            Where Id = @Id";

        public const string UpdateIsAutoSQL = @"Update [Device]
                                            Set IsAuto = @IsAuto
                                            Where Id = @Id";


        public const string AsyncDeviceIsActionSQL = @"SELECT ModuleId, Id, IsAction, IsUsed FROM [Device] WHERE IsUsed = 1 AND [DeviceType] = 'W'";

        // Information of Device
        public const string InformationDeviceSQL = @"Select D.Name AS DeviceName, Z.ZoneName, F.Name AS FarmName
            From Device AS D
            INNER JOIN
            Zone AS Z
            ON Z.Id = D.ZoneId
            INNER JOIN
            Farms AS F
            ON F.Id = Z.FarmId
            WHERE D.Id = @Id";

    }
}