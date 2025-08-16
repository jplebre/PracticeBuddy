using PracticeBuddy.Core.DataAccess;
using PracticeBuddy.Core.DataAccess.DataModels;
using PracticeBuddy.Core.DataAccess.Repositories;

namespace PracticeBuddy.API.IntegrationTests.DataAccess;

public class ExerciseInstanceRepositoryTests : IClassFixture<PracticeBuddyDbFixture>, IAsyncDisposable
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly IExerciseInstanceRepository _repository;
    private readonly IDapperDb _database;

    public ExerciseInstanceRepositoryTests(PracticeBuddyDbFixture fixture)
    {
        _factory = new CustomWebApplicationFactory<Program>(fixture);

        var scope = _factory.Services.CreateScope();

        var scopedServices = scope.ServiceProvider;
        _repository = scopedServices.GetRequiredService<IExerciseInstanceRepository>();
        _database = scopedServices.GetRequiredService<IDapperDb>();

    }

    [Fact]
    public async Task InsertExerciseInstance_StoresDataAsExpected()
    {
        // Arrange
        var exampleExerciseInstances = CreateTestExerciseInstances();
        var userId = await _database.QuerySingleAsync<int>(
            @"INSERT INTO `user` (username, firstname, lastname, email) VALUES (@UserName, @FirstName, @LastName, @Email);
            SELECT LAST_INSERT_ID()",
            new { UserName = "JD", FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com" });
        var routineId = await _database.QuerySingleAsync<int>(
            @"INSERT INTO `routine` (name,user_id) VALUES (@Name,@UserId);
            SELECT LAST_INSERT_ID()",
            new { Name = "User's Routine", UserId = userId });
        var exerciseId = await _database.QuerySingleAsync<int>(
            @"INSERT INTO `exercise` (name,user_id,routine_id) VALUES (@Name,@UserId,@RoutineId);
            SELECT LAST_INSERT_ID()",
            new { Name = "Scales", UserId = userId, RoutineId = routineId });

        // Act
        var affectedRows = await _repository.InsertExerciseInstance(exampleExerciseInstances);

        // Assert
        Assert.Equal(2, affectedRows);
        
        var storedExerciseInstances = await _repository.GetExerciseInstancesByUser(userId);
        Assert.NotNull(storedExerciseInstances);
        Assert.Equal(2, storedExerciseInstances.Count);
        Assert.Equal(exampleExerciseInstances.First().Name, exampleExerciseInstances.First().Name);
        Assert.Equal(exampleExerciseInstances.First().Tonality, storedExerciseInstances.First().Tonality);
        Assert.Equal(exampleExerciseInstances.First().Modifier, storedExerciseInstances.First().Modifier);
        Assert.Equal(exampleExerciseInstances.First().ExerciseDuration, storedExerciseInstances.First().ExerciseDuration);
        Assert.Equal(exampleExerciseInstances.First().TotalTimePracticed, storedExerciseInstances.First().TotalTimePracticed);
        Assert.Equal(exampleExerciseInstances.First().GoalBpm, storedExerciseInstances.First().GoalBpm);
        Assert.Equal(exampleExerciseInstances.First().ExerciseId, storedExerciseInstances.First().ExerciseId);
        Assert.Equal(exampleExerciseInstances.First().UserId, storedExerciseInstances.First().UserId);
        Assert.Equal(exampleExerciseInstances.First().CreatedAt, storedExerciseInstances.First().CreatedAt, TimeSpan.FromMinutes(2));
        Assert.Equal(exampleExerciseInstances.First().LastUpdatedAt, storedExerciseInstances.First().LastUpdatedAt, TimeSpan.FromMinutes(2));
        Assert.Equal(0, storedExerciseInstances.First().PracticeCount);
        Assert.Null(exampleExerciseInstances.First().LastPracticedAt);
    }

    private static List<ExerciseInstance> CreateTestExerciseInstances()
    {
        return new List<ExerciseInstance>()
        {
            new ()
            {
                Name = "Scale in C Major Descending",
                Tonality = "C Major",
                Modifier = "Descending, Staccato",
                ExerciseDuration = new TimeSpan(0, 5, 0),
                TotalTimePracticed = new TimeSpan(0, 5, 0),
                GoalBpm = 120,
                PracticeCount = 99999999,
                ExerciseId = 1,
                UserId = 1,
                LastPracticedAt = new DateTime(1999,1,1),
                CreatedAt = new DateTime(1999,1,1),
                LastUpdatedAt = new DateTime(1999,1,1)
            },
            new ()
            {
                Name = "Scale in A minor Descending",
                Tonality = "A minor",
                Modifier = "Descending, Staccato",
                ExerciseDuration = new TimeSpan(0, 5, 0),
                TotalTimePracticed = new TimeSpan(0, 5, 0),
                GoalBpm = 120,
                PracticeCount = 99999999,
                ExerciseId = 1,
                UserId = 1,
                LastPracticedAt = new DateTime(1999,1,1),
                CreatedAt = new DateTime(1999,1,1),
                LastUpdatedAt = new DateTime(1999,1,1)
            },
        };
    }

    [Fact]
    public async Task UpdatesExerciseInstance_UpdatesDataAsExpected()
    {

    }

    [Fact]
    public async Task DeletesExerciseInstance_DeletesDataAsExpected()
    {

    }

    public async ValueTask DisposeAsync()
    {
        await _database.ExecuteAsync("DELETE FROM user");
        await _database.ExecuteAsync("DELETE FROM routine");
        await _database.ExecuteAsync("DELETE FROM exercise");
        await _database.ExecuteAsync("DELETE FROM exercise_instance");
    }
}