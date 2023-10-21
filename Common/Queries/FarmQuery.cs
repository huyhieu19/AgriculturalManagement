namespace Common.Queries
{
    public static class FarmQuery
    {
        public const string UpdateFarmSQL = @"UPDATE Farm
         SET [Name] = @Name, Address = @Address, Description = @Description, Note = @Note
         WHERE Id = @Id";

        public const string GetFarmSQL = @"SELECT F.[Id]
      ,F.[Name]
      ,F.[Description]
      ,F.[Address]
      ,F.[Note]
      ,F.[CreatedDate]
      ,F.[UserId]
      ,F.[Area]
      ,COUNT(Z.[Id]) AS [CountZone]
    FROM [dbo].[Farm] F
    LEFT JOIN [dbo].[Zone] Z ON F.[Id] = Z.[FarmId]
    WHERE UserId = @UserID
    GROUP BY F.[Id], F.[Name], F.[Description], F.[Address], F.[Note], F.[CreatedDate], F.[UserId], F.[Area]
    ";
    }
}
