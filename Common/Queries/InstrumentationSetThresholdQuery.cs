namespace Common.Queries
{
    public static class InstrumentationSetThresholdQuery
    {
        public const string InstrumentationNotDelete = @"
        SELECT * FROM DeviceInstrumentThreshold WHERE IsDelete = 0;";


        public const string GetThresholdForUser = @"
                                                    SELECT
		A.DeviceName AS NameDeviceDriver,
    A.DeviceDriverId,
    A.Id,
    A.OnInUpperThreshold,
    A.InstrumentationId,
    A.DeviceName AS DeviceDriverName,
    A.IsActionDevice AS DeviceDriverAction,
    B.DeviceName AS DeviceInstrumentationName,
    A.ThresholdValueOff,
    A.ThresholdValueOn,
	A.IsAuto As AutoDevice
    --A.TypeDevice
    --COUNT(*) AS GroupCount  -- This will count the number of rows in each group
FROM
    (
        SELECT
            DTT.*,
            Ex.IsActionDevice,
            Ex.DeviceName AS DeviceName,
			Ex.IsAuto
        FROM
            [DeviceInstrumentThreshold] DTT
        INNER JOIN
            (
                SELECT
                    ModuleId,
                    M.UserId,
                    D.IsAction AS IsActionDevice,
                    D.Id AS DeviceId,
                    D.Name AS DeviceName,
					D.IsAuTo
                FROM
                    Device AS D
                INNER JOIN
                    Module AS M ON M.Id = D.ModuleId
					WHERE
    M.UserId = @UserId
            ) AS Ex ON Ex.DeviceId = DTT.DeviceDriverId
    ) AS A
INNER JOIN
    (
        SELECT
            DTT.*,
            Ex.IsActionDevice,
            Ex.DeviceName AS DeviceName
        FROM
            [DeviceInstrumentThreshold] DTT
        INNER JOIN
            (
                SELECT
                    ModuleId,
                    M.UserId,
                    D.IsAction AS IsActionDevice,
                    D.Id AS DeviceId,
                    D.Name AS DeviceName
                FROM
                    Device AS D
                INNER JOIN
                    Module AS M ON M.Id = D.ModuleId
					WHERE
    M.UserId = @UserId
            ) AS Ex ON Ex.DeviceId = DTT.InstrumentationId
    ) AS B ON A.DeviceDriverId = B.DeviceDriverId AND A.InstrumentationId = B.InstrumentationId
WHERE
    A.IsDelete = 0
GROUP BY
    A.Id,
    A.DeviceDriverId,
    A.OnInUpperThreshold,
    A.InstrumentationId,
    A.DeviceName,
    A.IsActionDevice,
    B.DeviceName,
    A.ThresholdValueOff,
    A.ThresholdValueOn,
	A.IsAuto";

        public const string GetThresholdByInstrumentationId = @"
                                                                SELECT
		A.DeviceName AS NameDeviceDriver,
    A.DeviceDriverId,
    A.Id,
    A.OnInUpperThreshold,
    A.InstrumentationId,
    A.DeviceName AS DeviceDriverName,
    A.IsActionDevice AS DeviceDriverAction,
    B.DeviceName AS DeviceInstrumentationName,
    A.ThresholdValueOff,
    A.ThresholdValueOn,
	A.IsAuto As AutoDevice
    --A.TypeDevice
    --COUNT(*) AS GroupCount  -- This will count the number of rows in each group
FROM
    (
        SELECT
            DTT.*,
            Ex.IsActionDevice,
            Ex.DeviceName AS DeviceName,
			Ex.IsAuto
        FROM
            [DeviceInstrumentThreshold] DTT
        INNER JOIN
            (
                SELECT
                    ModuleId,
                    M.UserId,
                    D.IsAction AS IsActionDevice,
                    D.Id AS DeviceId,
                    D.Name AS DeviceName,
					D.IsAuTo
                FROM
                    Device AS D
                INNER JOIN
                    Module AS M ON M.Id = D.ModuleId
            ) AS Ex ON Ex.DeviceId = DTT.DeviceDriverId
    ) AS A
INNER JOIN
    (
        SELECT
            DTT.*,
            Ex.IsActionDevice,
            Ex.DeviceName AS DeviceName
        FROM
            [DeviceInstrumentThreshold] DTT
        INNER JOIN
            (
                SELECT
                    ModuleId,
                    M.UserId,
                    D.IsAction AS IsActionDevice,
                    D.Id AS DeviceId,
                    D.Name AS DeviceName
                FROM
                    Device AS D
                INNER JOIN
                    Module AS M ON M.Id = D.ModuleId
            ) AS Ex ON Ex.DeviceId = DTT.InstrumentationId
    ) AS B ON A.DeviceDriverId = B.DeviceDriverId AND A.InstrumentationId = B.InstrumentationId
WHERE
    A.IsDelete = 0
GROUP BY
    A.Id,
    A.DeviceDriverId,
    A.OnInUpperThreshold,
    A.InstrumentationId,
    A.DeviceName,
    A.IsActionDevice,
    B.DeviceName,
    A.ThresholdValueOff,
    A.ThresholdValueOn,
	A.IsAuto";
    }
}