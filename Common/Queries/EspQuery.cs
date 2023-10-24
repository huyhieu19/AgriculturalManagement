namespace Common.Queries
{
    public static class EspQuery
    {
        public const string GetAll = @"SELECT TOP (1000)
            E.[Id],
            E.[Name],
            E.[Note],
            E.[ClientId],
            E.[MqttPort],
            E.[MqttServer],
            E.[Password],
            E.[UserName],
            I.[Topic] AS TopicDevice,
            I.[Id] AS InstrumentationId
        FROM [dbo].[Esp] AS E
        LEFT JOIN [dbo].[Instrumentation] AS I ON I.EspId = E.[Id];";
    }
}
