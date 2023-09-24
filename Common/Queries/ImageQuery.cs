namespace Common.Queries
{
    public static class ImageQuery
    {
        public const string CreateImage = @"INSERT INTO Image (Url,IsDefault, Name, FarmId, StaffId, UserId, ZoneId, ZoneHarvestId) " +
                   "VALUES (@Url, @IsDefault, @Name, @FarmId, @StaffId, @UserId, @ZoneId, @ZoneHarvestId);";
    }
}
