using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PracticeBuddy.DbMigrations.Migrations;

namespace PracticeBuddy.DbMigrations;

class Program
{
    static void Main(string[] args)
    {
        // There is a bug in dotnet user-secrets and IHost Configs.
        // It'll only pull from secrets if it's in dev. So, force dev
        using IHost host = Host.CreateDefaultBuilder(args).UseEnvironment("Development").Build();
        
        using (var serviceProvider = CreateServices(host))
        using (var scope = serviceProvider.CreateScope())
        {
            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            UpdateDatabase(scope.ServiceProvider);
        }
    }

    /// <summary>
    /// Configure the dependency injection services
    /// </summary>
    private static ServiceProvider CreateServices(IHost host)
    {
        IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
        var connString = config.GetSection("PracticeBuddyApi:MySql").GetValue<string>("ConnectionString");

        return new ServiceCollection()
            // Add common FluentMigrator services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add MySql v8 support to FluentMigrator
                .AddMySql8()
                // Set the connection string
                .WithGlobalConnectionString(connString)
                // Define the assembly containing the migrations, maintenance migrations and other customizations
                .ScanIn(typeof(MigrationTest).Assembly).For.All())
            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            // Build the service provider
            .BuildServiceProvider(false);
    }

    /// <summary>
    /// Update the database
    /// </summary>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
    }
}