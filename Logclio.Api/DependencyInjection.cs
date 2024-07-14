using Asp.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Logclio.Api
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures api versioning and documentation.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder to add services to.</param>
        /// <returns>The WebApplicationBuilder with services configured.</returns>
        public static WebApplicationBuilder ConfigureApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            var apiVersion = int.Parse(builder.Configuration["ApiVersion"] ?? "1");
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(apiVersion);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            });

            builder.ConfigureApiDocumentation();

            return builder;
        }

        /// <summary>
        /// Configures api documentation
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder to add services to.</param>
        /// <returns>The WebApplicationBuilder with services configured.</returns>
        public static WebApplicationBuilder ConfigureApiDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var serviceVersion = Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString(3) ?? "1.0.0";

                var apiVersion = $"v{builder.Configuration?.GetValue("ApiVersion", "1")?.ToLower()}";

                c.SwaggerDoc($"{apiVersion}", new OpenApiInfo
                {
                    Title = $"Logclio {serviceVersion} log processing service - Franjo Topić",
                    Version = $"{apiVersion}"
                });

                c.CustomSchemaIds(x => x.FullName);

                var basePath = AppContext.BaseDirectory;
                var fileName = $"{Assembly.GetExecutingAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(basePath, fileName);

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath, true);
                }
            });

            return builder;
        }
    }
}
