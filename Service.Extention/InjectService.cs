using Microsoft.Extensions.DependencyInjection;
using Service.Contracts;

namespace Service.Extention
{
    public static class InjectService
    {
        public static void ConfigureLoggerService(this IServiceCollection service) => service.AddSingleton<ILoggerManager, LoggerManager>();
    }
}