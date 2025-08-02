using FluentMigrator.Runner;
using PracticeBuddy.DbMigrations.Migrations;
using Testcontainers.MySql;

namespace PracticeBuddy.API.IntegrationTests;

public class PracticeBuddyDbFixture : IAsyncLifetime
{
    public MySqlContainer Container { get; internal set; }
    private string _connectionString => Container.GetConnectionString();

    public PracticeBuddyDbFixture()
    {
        Container = new MySqlBuilder()
            .WithImage("mysql:8.0")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await Container.StartAsync();
        MigrateDatabase();
        // SeedDatabase();
    }

    public async Task DisposeAsync()
    {
        await Container.StopAsync();
        await Container.DisposeAsync();
    }

    private void MigrateDatabase()
    {
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddMySql8()
                .WithGlobalConnectionString(_connectionString)
                .ScanIn(typeof(MigrationTest).Assembly).For.All())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private void SeedDatabase()
    {
        throw new NotImplementedException();
    }
}