namespace Common.Queries
{
    public static class ZoneQuery
    {
        public const string UpdateZoneSQL = @"UPDATE Zone
         SET [Name] = @Name, Description = @Description, Note = @Note, HarvestTime = @HarvestTime, TimeToStartPlanting = @TimeToStartPlanting, Function = @Function,
                TypeTreeId = @TypeTreeId, FarmId = @FarmId
         WHERE Id = @Id";

        public const string GetZoneSQL = @"SELECT Z.[Id]
              ,Z.[ZoneName]
              ,Z.[Description]
              ,Z.[Note]
              ,Z.[TimeToStartPlanting]
              ,Z.[Function]
              ,Z.[DateCreateFarm]
              ,Z.[FarmId]
              ,Z.[TypeTreeId]
              ,Z.[Area]
	          ,(SELECT COUNT(*) FROM [dbo].[DeviceDriver] DD WHERE DD.[ZoneId] = Z.[Id]) AS [CountDeviceDriver]
	          ,(SELECT COUNT(*) FROM [dbo].[Instrumentation] I WHERE I.[ZoneId] = Z.[Id]) AS [CountInstrumentation]
          FROM [dbo].[Zone] Z
          WHERE [FarmId] =@FarmId";
    }
}