namespace Common.Queries
{
    public static class ZoneQuery
    {
        public const string UpdateZone = @"UPDATE Zone
         SET [Name] = @Name, Address = @Address, Description = @Description, Note = @Note, HarvestTime = @HarvestTime, TimeToStartPlanting = @TimeToStartPlanting, Function = @Function,
                TypeTreeId = @TypeTreeId, FarmId = @FarmId
         WHERE Id = @Id";
    }
}
