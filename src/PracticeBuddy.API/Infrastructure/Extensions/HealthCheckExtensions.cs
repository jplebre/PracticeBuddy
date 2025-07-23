using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PracticeBuddy.API.Infrastructure.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection RegisterHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks().AddMySql(
            healthQuery: "SELECT 1;",
            name: "MySQL",
            timeout: TimeSpan.FromSeconds(3),
            failureStatus: HealthStatus.Degraded,
            tags: ["db", "sql", "mysql", "readiness"]
        );

        return services;
    }

    public static IEndpointRouteBuilder UseHealthChecks(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapHealthChecks("/healthz/ready", new HealthCheckOptions
        {
            Predicate = healthCheck => healthCheck.Tags.Contains("readiness"),
            ResponseWriter = HealthCheckExtensions.WriteHealthCheckResponse
        });

        routeBuilder.MapHealthChecks("/healthz/live", new HealthCheckOptions
        {
            // Predicate = _ => false,
            ResponseWriter = HealthCheckExtensions.WriteHealthCheckResponse
        });
        return routeBuilder;
    }

    public static Task WriteHealthCheckResponse(HttpContext context, HealthReport healthReport)
    {
        context.Response.ContentType = "application/json; charset=utf-8";

        var options = new JsonWriterOptions { Indented = true };

        using var memoryStream = new MemoryStream();
        using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
        {
            jsonWriter.WriteStartObject();
            jsonWriter.WriteString("status", healthReport.Status.ToString());
            jsonWriter.WriteStartObject("results");

            foreach (var healthReportEntry in healthReport.Entries)
            {
                jsonWriter.WriteStartObject(healthReportEntry.Key);
                jsonWriter.WriteString("status",
                    healthReportEntry.Value.Status.ToString());
                jsonWriter.WriteString("description",
                    healthReportEntry.Value.Description);
                jsonWriter.WriteStartObject("data");

                foreach (var item in healthReportEntry.Value.Data)
                {
                    jsonWriter.WritePropertyName(item.Key);

                    JsonSerializer.Serialize(jsonWriter, item.Value,
                        item.Value?.GetType() ?? typeof(object));
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndObject();
        }

        return context.Response.WriteAsync(
            Encoding.UTF8.GetString(memoryStream.ToArray()));
    }
}