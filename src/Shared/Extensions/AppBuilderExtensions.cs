using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ApiTemplate.Shared.Extensions;

internal static class AppBuilderExtensions
{
    internal static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.AddHealthChecks();
        builder.Services.AddApplicationSevices();
        builder.Services.AddValidation();
        builder.Services.AddAutoMapper();
        //builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services
           .AddApiVersioning(options =>
           {
               options.ApiVersionReader = new UrlSegmentApiVersionReader();
               options.DefaultApiVersion = new ApiVersion(1, 0);
               options.AssumeDefaultVersionWhenUnspecified = true;
               options.ReportApiVersions = true;
               options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
           })
           .AddApiExplorer(opts =>
           {
               opts.GroupNameFormat = "'v'VVV";
               opts.SubstituteApiVersionInUrl = true;
           })
           .AddApiExplorer(options => options.SubstituteApiVersionInUrl = true);
        //builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "My API" });
            c.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "My API" });
        });


        //builder.Services.AddSwaggerGen(c =>
        //{
        //    c.SwaggerDoc("ApiTemplate", new OpenApiInfo
        //    {
        //        Title = "ApiTemplate",
        //        Version = "V1",
        //        Description = "ApiTemplate",
        //        Contact = new OpenApiContact
        //        {
        //            Name = "Contact"
        //        }
        //    });
        //    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml"));
        //});

        builder.Host.UseSerilog((context, config)
            => config.ReadFrom.Configuration(context.Configuration));

        return builder;
    }

    private static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder) => builder;

}


