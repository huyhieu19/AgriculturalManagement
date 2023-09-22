namespace Common.Queries
{
    public static class FarmQuery
    {
        public const string UpdateFarm = @"UPDATE Farm
         SET [Name] = @name, Address = @address, Description = @description
         WHERE Id = @Id";
    }
}
