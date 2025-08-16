using PracticeBuddy.Core.DataAccess;
using PracticeBuddy.Core.DataAccess.Repositories;
using PracticeBuddy.Core.DataAccess.DataModels;

namespace PracticeBuddy.API.IntegrationTests.DataAccess;

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
        var userId = await _database.QueryAsync<int>(
            @"INSERT INTO `user` (username, firstname, lastname, email) VALUES (@UserName, @FirstName, @LastName, @Email);
            SELECT LAST_INSERT_ID()",
            new { UserName = "JD", FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com" });

        // Act
        var routineId = await _repository.InsertRoutine(exampleRoutine);

        // Assert
        var storedRoutine = await _repository.GetRoutine(routineId);
        Assert.NotNull(storedRoutine);
        Assert.Equal(exampleRoutine.Name, exampleRoutine.Name);
        Assert.NotEmpty(storedRoutine.Exercises);
        Assert.Equal(exampleRoutine.Exercises.Count, storedRoutine.Exercises.Count);
        Assert.Equal(exampleRoutine.Exercises[0].Name, storedRoutine.Exercises[0].Name);
        Assert.Equal(exampleRoutine.UserId, storedRoutine.Exercises[0].UserId);
        Assert.Equal(exampleRoutine.Id, storedRoutine.Exercises[0].RoutineId);
        Assert.Equal(0, storedRoutine.Exercises[0].PracticeCount);
        Assert.Null(exampleRoutine.Exercises[0].LastPracticedAt);
    }

    private static Routine CreateTestRoutine()
    {
        return new()
        {
            Id = 1,
            UserId = 1,
            Name = "A Routine",
            Exercises = new List<Exercise>()
            {
                new Exercise(){ Id = 1, Name = "2 Octave Scales" },
                new Exercise(){ Id = 2, Name = "Hanon Exercises" }
            },
            LastPracticedAt = new DateTime(1999,1,1),
            CreatedAt = new DateTime(1999,1,1),
            LastUpdatedAt = new DateTime(1999,1,1)
        };
    }

    public async ValueTask DisposeAsync()
    {
        await _database.ExecuteAsync("DELETE FROM routine");
        await _database.ExecuteAsync("DELETE FROM exercise");
    }
}