using Database;
using JobBackground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Repository;
using Repository.Contracts;
using Service;
using Service.BackgroundJob;
using Service.Contracts;
using Service.Extention;

namespace Startup
{
    public static class ServiceExtention
    {

        public static WebApplicationBuilder AddServicesBase(this WebApplicationBuilder builder)
        {


            // Add services to the container.
            builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();


            builder.Services.AddSingleton<DapperContext>();


            builder.Services.AddSingleton<IScheduler>(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                return schedulerFactory.GetScheduler().Result;
            });

            builder.Services.AddSingleton<JobSchedulerDeviceDriver>();

            builder.Services.AddHostedService<JobSchedulerHostedService>();



            //// job chay background 5s 1 lần
            builder.Services.AddHostedService<JobForDeviceDriverService>();


            builder.Services.AddControllers(config =>
            {
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
                {
                    Duration = 120
                });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddAuthentication();

            builder.Services.ConfigureIdentity();

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

            builder.Services.ConfigureJWT(builder.Configuration);

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
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Agricultural Management", Version = "v1" });

                //opt.MapType<QueryBaseModel>(() => new OpenApiSchema { Type = "object" });

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
