using NLog;
using Startup;

var builder = WebApplication.CreateBuilder(args)
  .AddServicesContext()
  .AddServicesBase()
  ;

builder.Services.AddAutoMapper(typeof(Program));

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
var app = builder.Build();

// Add global error handling
///var logger = app.Services.GetRequiredService<ILoggerManager>();
///app.ConfigureExceptionHandler(logger);

app.UseService();
app.Run();

//internal class Program
//{
//    private static void Main(string[] args)
//    {




//    }
//}