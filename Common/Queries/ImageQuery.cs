namespace Common.Queries
{
    public static class ImageQuery
    {
        public const string CreateImageSQL = @"INSERT INTO Image (Url,IsDefault, Name, FarmId, StaffId, UserId, ZoneId, ZoneHarvestId) " +
                   "VALUES (@Url, @IsDefault, @Name, @FarmId, @StaffId, @UserId, @ZoneId, @ZoneHarvestId);";
        public const string SetImageDefaultSQl = @"Update Image
                                                Set IsDefault = 0
                                                Where IsDefault = 1

                                                Update Image
                                                Set IsDefault = 1
                                                Where Id = @Id";
        public const string SetAllDefaultToFalseSQL = @"Update Image
                                                Set IsDefault = 0";
    }
}
