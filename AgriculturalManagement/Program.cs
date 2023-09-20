using NLog;


using Startup;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args)
          .AddServicesContext()
          .AddServicesBase()
          ;

        LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        var app = builder.Build();
        app.UseService();
        app.Run();


    }
}