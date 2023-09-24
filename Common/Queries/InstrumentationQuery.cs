namespace Common.Queries
{
    public static class InstrumentationQuery
    {
        public const string ZoneIdToNull = @"Update Instrumentation
                                            Set ZoneId = null
                                            Where Id = @Id";
        public const string UpdateInfo = @"Update Instrumentation
                        Set Name = @Name, Note = @Note, Description = @Description,  IsActive = @IsActive, DateOfPurchanse = @DateOfPurchanse, ZoneId = @ZoneId
                        Where Id = @Id";

    }
}
