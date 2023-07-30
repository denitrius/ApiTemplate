using ApiTemplate.Shared.Middleware;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

namespace ApiTemplate.Shared.Extensions;

internal static class WebApplicationExtensions
{
    internal static WebApplication ConfigureApp(this WebApplication app)
    {
        //Custom modules injection
        app.UseModules();
        app.UseMiddleware<ExceptionMiddleware>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.MapControllers();
        //app.UseAuthorization();
        return app;
    }
}
