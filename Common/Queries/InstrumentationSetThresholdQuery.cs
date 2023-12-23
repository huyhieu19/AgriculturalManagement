namespace Common.Queries
{
    public static class InstrumentationSetThresholdQuery
    {
        public const string InstrumentationNotDelete = @"
        SELECT * FROM DeviceInstrumentThreshold WHERE IsDelete = 0;";


        public const string GetThresholdForUser = @"
        Select A.DeviceDriverId, A.Id, A.OnInUpperThreshold, A.InstrumentationId, A.DeviceName AS DeviceDriverName, A.IsActionDevice AS DeviceDriverAction, B.DeviceName AS DeviceInstrumentationName, A.ThresholdValueOff, A.ThresholdValueOn, A.TypeDevice From (SELECT
                        DTT.*,
                        Ex.IsActionDevice,
                        Ex.DeviceName As DeviceName
                    FROM
                        [DeviceInstrumentThreshold] DTT
                    INNER JOIN
                       (Select ModuleId, M.UserId, D.IsAction AS IsActionDevice, D.Id AS DeviceId, D.Name AS DeviceName From Device AS D
                       INNER JOIN
                       Module AS M
                       ON M.Id = D.ModuleId
                       WHERE M.UserId = @UserId
                       ) AS Ex
                    ON
                        Ex.DeviceId = DTT.DeviceDriverId) as A
                         INNER JOIN(SELECT
                        DTT.*,
                        Ex.IsActionDevice,
                        Ex.DeviceName As DeviceName
                    FROM
                        [DeviceInstrumentThreshold] DTT
                    INNER JOIN
                       (Select ModuleId, M.UserId, D.IsAction AS IsActionDevice, D.Id AS DeviceId, D.Name AS DeviceName From Device AS D
                       INNER JOIN
                       Module AS M
                       ON M.Id = D.ModuleId
                       WHERE M.UserId = @UserId
                       ) AS Ex

                    ON
                        Ex.DeviceId = DTT.InstrumentationId) As B

                        ON A.DeviceDriverId = B.DeviceDriverId";

        public const string GetThresholdByInstrumentationId = @"
        Select A.DeviceDriverId, A.Id, A.OnInUpperThreshold, A.InstrumentationId, A.ThresholdValueOff, A.ThresholdValueOn, A.TypeDevice, A.IsActionDevice AS DeviceDriverAction From (SELECT
                DTT.*,
                Ex.IsActionDevice,
                Ex.DeviceName As DeviceName
            FROM
                [DeviceInstrumentThreshold] DTT
            INNER JOIN
               (Select ModuleId, M.UserId, D.IsAction AS IsActionDevice, D.Id AS DeviceId, D.Name AS DeviceName From Device AS D
               INNER JOIN
               Module AS M

               ON M.Id = D.ModuleId

               ) AS Ex

            ON
                Ex.DeviceId = DTT.DeviceDriverId) as A
                 INNER JOIN(SELECT
                DTT.*,
                Ex.IsActionDevice,
                Ex.DeviceName As DeviceName
            FROM
                [DeviceInstrumentThreshold] DTT
            INNER JOIN
               (Select ModuleId, M.UserId, D.IsAction AS IsActionDevice, D.Id AS DeviceId, D.Name AS DeviceName From Device AS D
               INNER JOIN
               Module AS M
               ON M.Id = D.ModuleId
               ) AS Ex
            ON
                Ex.DeviceId = DTT.InstrumentationId) As B

                ON A.DeviceDriverId = B.DeviceDriverId
				WHERE A.TypeDevice =@TypeDevice";
    }
}