using System.Reflection;
using Microsoft.OpenApi.Models;

namespace PracticeBuddy.API.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }

    public static IServiceCollection ConfigureCache(this IServiceCollection services)
    {
        // Initially, cache for OpenAPI only
        services.AddOutputCache(options =>
        {
            // options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromSeconds(10))); // Global cache
            options.AddPolicy("documentation", policy => policy.Expire(TimeSpan.FromMinutes(10)));
        });

        return services;
    }

    public static IServiceCollection AddApiExplorerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "PracticeBuddy API",
                Description = "An API to help manage practice sessions for musicians",
                // TermsOfService = new Uri("https://example.com/terms"),
                // Contact = new OpenApiContact
                // {
                //     Name = "Example Contact",
                //     Url = new Uri("https://example.com/contact")
                // },
                // License = new OpenApiLicense
                // {
                //     Name = "Example License",
                //     Url = new Uri("https://example.com/license")
                // }
            });

            // For most features, namely method summaries and the descriptions of parameters and response codes, the use of an XML file is mandatory.
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        services.AddOpenApi();

        return services;
    }
}