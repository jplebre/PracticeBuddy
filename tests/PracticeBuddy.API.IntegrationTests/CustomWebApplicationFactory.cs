using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using MySqlConnector;

namespace PracticeBuddy.API.IntegrationTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly string _connectionString;

    public CustomWebApplicationFactory(PracticeBuddyDbFixture fixture)
    {
        _connectionString = fixture.Container.GetConnectionString();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType)!);
            services.Remove(services.SingleOrDefault(service => typeof(DbDataSource) == service.ServiceType)!);
            services.Remove(services.SingleOrDefault(service => typeof(MySqlDataSource) == service.ServiceType)!);
            services.Remove(services.SingleOrDefault(service => typeof(MySqlConnection) == service.ServiceType)!);
            services.AddMySqlDataSource(_connectionString);
        });
        builder.UseEnvironment("Development");
    }
}