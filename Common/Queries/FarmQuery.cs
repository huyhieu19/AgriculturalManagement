namespace Common.Queries
{
    public static class FarmQuery
    {
        public const string UpdateFarmSQL = @"UPDATE Farm
         SET [Name] = @Name, Address = @Address, Description = @Description, Note = @Note
         WHERE Id = @Id";
    }
}
