namespace Common.Queries
{
    public static class ZoneQuery
    {
        public const string UpdateZoneSQL = @"UPDATE Zone
         SET [Name] = @Name, Description = @Description, Note = @Note, HarvestTime = @HarvestTime, TimeToStartPlanting = @TimeToStartPlanting, Function = @Function,
                TypeTreeId = @TypeTreeId, FarmId = @FarmId
         WHERE Id = @Id";
    }
}
