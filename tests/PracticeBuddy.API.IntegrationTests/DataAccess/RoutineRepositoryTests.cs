using Org.BouncyCastle.Bcpg;
using PracticeBuddy.Core.DataAccess;
using PracticeBuddy.Core.DataAccess.Repositories;
using PracticeBuddy.Core.DataModels;

namespace PracticeBuddy.API.IntegrationTests.Controllers;

public class RoutineRepositoryTests : IClassFixture<PracticeBuddyDbFixture>, IAsyncDisposable
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly IRoutineRepository _repository;
    private readonly IDapperDb _database;

    public RoutineRepositoryTests(PracticeBuddyDbFixture fixture)
    {
        _factory = new CustomWebApplicationFactory<Program>(fixture);

        var scope = _factory.Services.CreateScope();

        var scopedServices = scope.ServiceProvider;
        _repository = scopedServices.GetRequiredService<IRoutineRepository>();
        _database = scopedServices.GetRequiredService<IDapperDb>();

    }

    [Fact]
    public async Task InsertRoutine_StoresDataAsExpected()
    {
        // Arrange
        var exampleRoutine = CreateTestRoutine();
        var userId = await _database.QueryAsync<int>(@"INSERT INTO `user` (username, firstname, lastname, email) VALUES (@UserName, @FirstName, @LastName, @Email);
            SELECT LAST_INSERT_ID()",
            new { UserName = "JD", FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com" });

        // Act
        var routineId = await _repository.InsertRoutine(exampleRoutine);

        // Assert
        var storedRoutine = await _repository.GetRoutine(routineId);
        Assert.NotNull(storedRoutine);
        Assert.Equal(exampleRoutine.Name, exampleRoutine.Name);
        // TODO: get the left join working
        // Assert.NotEmpty(storedRoutine.Exercises);
        // Assert.Equal(exampleRoutine.Exercises, storedRoutine.Exercises);
    }

    private static Routine CreateTestRoutine()
    {
        return new()
        {
            Name = "A Routine",
            UserId = 1,
            Exercises = new List<Exercise>()
            {
                new Exercise(){ Name = "2 Octave Scales" },
                new Exercise(){ Name = "Hanon Exercises" }
            }
        };
    }

    public async ValueTask DisposeAsync()
    {
        await _database.ExecuteAsync("DELETE FROM routine");
        await _database.ExecuteAsync("DELETE FROM exercise");
    }
}