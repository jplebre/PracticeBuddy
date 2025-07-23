using Testcontainers.MySql;

namespace PracticeBuddy.API.IntegrationTests;

public class PracticeBuddyDbFixture : IAsyncLifetime
{
    public MySqlContainer Container { get; internal set; }

    public PracticeBuddyDbFixture()
    {
        Container = new MySqlBuilder()
            .WithImage("mysql:8.0")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await Container.StartAsync();
        // MigrateDatabase();
    }

    public async Task DisposeAsync()
    {
        await Container.StopAsync();
        await Container.DisposeAsync();
    }

    // private void MigrateDatabase()
    // {
    //     var serviceProvider = new ServiceCollection()
    //         .AddFluentMigratorCore()
    //         .ConfigureRunner(rb => rb
    //             .AddSqlServer()
    //             .WithGlobalConnectionString(ConnectionString)
    //             .ScanIn(typeof(AddBookTable).Assembly).For.Migrations())
    //         .BuildServiceProvider();
    //     var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    //     runner.MigrateUp();
    // }
}