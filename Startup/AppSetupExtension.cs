using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Startup
{
    public static class AppSetupExtension
    {

        public static WebApplication UseService(this WebApplication app)
        {
            app.UseCors();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMiddleware<ApiResponseMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}