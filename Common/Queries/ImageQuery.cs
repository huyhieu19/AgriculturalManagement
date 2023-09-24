namespace Common.Queries
{
    public static class ImageQuery
    {
        public const string CreateImage = @"INSERT INTO Image (Url,IsDefault, Name, FarmId, StaffId, UserId, ZoneId, ZoneHarvestId) " +
                   "VALUES (@Url, @IsDefault, @Name, @FarmId, @StaffId, @UserId, @ZoneId, @ZoneHarvestId);";
        public const string SetImageDefault = @"Update Image
                                                Set IsDefault = 0
                                                Where IsDefault = 1

                                                Update Image
                                                Set IsDefault = 1
                                                Where Id = @Id";
        public const string SetAllDefaultToFalse = @"Update Image
                                                Set IsDefault = 0";
    }
}
