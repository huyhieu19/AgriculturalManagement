using Database;
using JobBackground.DeviceAuto;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Models.Config.Mongo;
using MQTTProcess;
using Repository;
using Repository.Contracts;
using Service;
using Service.Contracts;
using Service.Contracts.DeviceThreshold;
using Service.Contracts.Logger;
using Service.DeviceThreshold;
using Service.Extensions;
using Service.Logger;

namespace Startup
{
    public static class ServiceExtention
    {
        public static WebApplicationBuilder AddServicesBase(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
            builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddSingleton<IDataStatisticsService, DataStatisticsService>();
            builder.Services.AddSingleton<IDeviceControlService, DeviceControlService>();
            builder.Services.AddSingleton<IDeviceJobMqtt, ProcessJobMqtt>();
            builder.Services.AddSingleton<IDeviceJobInstrumentationService, DeviceJobInstrumentationService>();
            builder.Services.AddSingleton<IProcessJobControlDevice, ProcessJobControlDevice>();

            builder.Services.AddHostedService<ProcessJobMqtt>();
            builder.Services.AddHostedService<TimerJobDevice>();


            // Inject background service
            //builder.Services.AddHostedService<ProcessDataReceivedFromMQTT>();
            //builder.Services.AddHostedService<JobForDeviceDriverService>();
            //builder.Services.AddHostedService<JobThresholdService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication();
            builder.Services.AddHttpContextAccessor();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // add config to model
            builder.Services.Configure<MongoDbConfigModel>(builder.Configuration.GetSection("MongoDbConfig"));
            //builder.Services.Configure<MqttConnectionConfigModel>(builder.Configuration.GetSection("MqttConfig"));

            // add caching
            ///builder.Services.ConfigureResponseCaching();
            ///builder.Services.ConfigureHttpCacheHeaders();

            return builder;
        }
        public static WebApplicationBuilder AddServicesContext(this WebApplicationBuilder builder)
        {
            // SQL Server dependency Injection
            builder.Services.AddDbContext<FactDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("AgriculturalManagement")));


            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Agricultural Management API", Version = "v1" });

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: \"Bearer 1safsfsdfdfd\"",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            return builder;
        }
    }
}