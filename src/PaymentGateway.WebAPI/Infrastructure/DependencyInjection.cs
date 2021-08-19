using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace PaymentGateway.WebAPI.Infrastructure
{
    public static class DependencyInjection
    {
        private static IConfiguration _configuration;

        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;

            ConfigureSwagger(services);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentGateway.WebAPI", Version = "v1" });

                SetXMLCommentsForSwaggerUI(options);

            });
        }

        private static void SetXMLCommentsForSwaggerUI(SwaggerGenOptions swaggerGenOptions)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            swaggerGenOptions.IncludeXmlComments(xmlPath);
            swaggerGenOptions.CustomSchemaIds(s => s.FullName);
        }
    }
}
