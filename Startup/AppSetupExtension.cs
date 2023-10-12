﻿using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Startup
{
    public static class AppSetupExtension
    {

        public static WebApplication UseService(this WebApplication app)
        {
            app.UseCors("AllowAllHeaders");
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI(options =>
            //    {
            //        options.DocExpansion(DocExpansion.None);
            //    });
            //}
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocExpansion(DocExpansion.None);
            });
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMiddleware<ApiResponseMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.UseRouting();
            ////startupTask.StartAsync(CancellationToken.None).Wait();
            ///app.UseResponseCaching();
            ///app.UseHttpCacheHeaders();

            return app;
        }
    }
}