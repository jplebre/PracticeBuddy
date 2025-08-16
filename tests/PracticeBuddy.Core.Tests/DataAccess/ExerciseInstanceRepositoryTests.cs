using FakeItEasy;
using PracticeBuddy.Core.DataAccess;
using PracticeBuddy.Core.DataAccess.Repositories;
using PracticeBuddy.Core.DataAccess.DataModels;
using PracticeBuddy.Core.Constants;
using Dapper;

namespace PracticeBuddy.Core.Tests.DataAccess.Repositories;

public class ExerciseInstanceRepositoryTests
{
    private const int UserId = 1337;
    private readonly IDapperDb _dapperDb;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ExerciseInstanceRepository _sut;

    public ExerciseInstanceRepositoryTests()
    {
        _dapperDb = A.Fake<IDapperDb>();
        _dateTimeProvider = A.Fake<IDateTimeProvider>();
        A.CallTo(() => _dateTimeProvider.Now()).Returns(new DateTime(2001, 01, 01, 00, 01, 01));

        _sut = new ExerciseInstanceRepository(_dapperDb, _dateTimeProvider);
    }

    [Fact]
    public async Task InsertExerciseInstance_ReturnsCorrectResult()
    {
        // Arrange
        List<ExerciseInstance> exerciseInstances = new List<ExerciseInstance>(){
            CreateTestExerciseInstance(),
            CreateTestExerciseInstance(),
            CreateTestExerciseInstance()
        };

        A.CallTo(() => _dapperDb.ExecuteAsync(
                                A<string>.That.Contains("INSERT INTO `exercise_instance`"), exerciseInstances))
                                .Returns(3);

        // Act
        var result = await _sut.InsertExerciseInstance(exerciseInstances);

        // Assert
        Assert.Equal(3, result);
        A.CallTo(() => _dapperDb.ExecuteAsync(
            A<string>.That.Contains("INSERT INTO `exercise_instance`"), A<List<ExerciseInstance>>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task InsertExerciseInstance_PopulatesTimestampsCorrectly()
    {
        // Arrange
        var capturedExercises = A.Captured<List<ExerciseInstance>>();
        List<ExerciseInstance> exerciseInstance = new List<ExerciseInstance>() { CreateTestExerciseInstance() };

        A.CallTo(() => _dapperDb.ExecuteAsync(
                            A<string>.That.Contains("INSERT INTO `exercise_instance`"), capturedExercises._))
                            .Returns(1);

        // Act
        var result = await _sut.InsertExerciseInstance(exerciseInstance);

        // Assert
        Assert.Equal(1, result);
        Assert.NotEmpty(capturedExercises.Values);
        var insertedExerciseInstance = capturedExercises.Values.FirstOrDefault()?.FirstOrDefault();
        Assert.NotNull(insertedExerciseInstance);
        Assert.Equal(1, insertedExerciseInstance.Id);
        Assert.Equal(12345, insertedExerciseInstance.ExerciseId);
        Assert.Equal(1337, insertedExerciseInstance.UserId);
        Assert.Equal(0, insertedExerciseInstance.PracticeCount);
        Assert.Equal(new TimeSpan(0,5,0), insertedExerciseInstance.ExerciseDuration);
        Assert.Equal(TimeSpan.Zero, insertedExerciseInstance.TotalTimePracticed);
        Assert.Null(insertedExerciseInstance.LastPracticedAt);
        Assert.Equal(_dateTimeProvider.Now(), insertedExerciseInstance.LastUpdatedAt);
        Assert.Equal(_dateTimeProvider.Now(), insertedExerciseInstance.CreatedAt);
    }

    [Fact]
    public async Task UpdateExerciseInstance_ReturnsCorrectResult()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task IncreaseExerciseInstancePracticeCount_ReturnsCorrectResult()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task DeleteExcerciseInstance_ReturnsCorrectResult()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task GetExerciseInstance_ReturnsCorrectResult()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task GetExerciseInstanceByRoutine_ReturnsCorrectResult()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task GetExerciseInstanceByExercise_ReturnsCorrectResult()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task GetExerciseInstanceByUser_ReturnsCorrectResult()
    {
        // Arrange
        List<ExerciseInstance> exerciseInstances = new List<ExerciseInstance>(){
            CreateTestExerciseInstance(),
            CreateTestExerciseInstance(),
            CreateTestExerciseInstance()
        };

        A.CallTo(() => _dapperDb.QueryAsync<ExerciseInstance>(
                                A<string>.That.Contains("SELECT * FROM `exercise_instance` WHERE `user_id` = @UserId"), A<DynamicParameters>._))
                                .Returns(exerciseInstances);

        // Act
        var result = await _sut.GetExerciseInstancesByUser(UserId);

        // Assert
        Assert.Equal(3, result.Count);
        A.CallTo(() => _dapperDb.QueryAsync<ExerciseInstance>(
            A<string>.That.Contains("SELECT * FROM `exercise_instance` WHERE `user_id` = @UserId"), 
            A<DynamicParameters>.That.Matches(x => x.Get<int>("@UserId") == 1337))).MustHaveHappenedOnceExactly();
    }

    private ExerciseInstance CreateTestExerciseInstance()
    {
        return new()
        {
            Id = 1,
            Name = "Scale in C Major Descending",
            Tonality = "C Major",
            Modifier = "Descending, Staccato",
            ExerciseDuration = new TimeSpan(0, 5, 0),
            TotalTimePracticed = new TimeSpan(0, 5, 0),
            GoalBpm = 120,
            PracticeCount = 0,
            ExerciseId = 12345,
            UserId = UserId,
            LastPracticedAt = new DateTime(),
            CreatedAt = new DateTime(),
            LastUpdatedAt = new DateTime()
        };
    }
}