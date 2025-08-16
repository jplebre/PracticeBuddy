using Dapper;
using FakeItEasy;
using PracticeBuddy.Core.DataAccess;
using PracticeBuddy.Core.DataAccess.Repositories;
using PracticeBuddy.Core.DataAccess.DataModels;

namespace PracticeBuddy.Core.Tests.DataAccess.Repositories;

public class RoutineRepositoryTests
{
    private readonly IDapperDb _dapperDb;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly RoutineRepository _sut;

    public RoutineRepositoryTests()
    {
        _dapperDb = A.Fake<IDapperDb>();
        _dateTimeProvider = A.Fake<IDateTimeProvider>();

        _sut = new RoutineRepository(_dapperDb, _dateTimeProvider);
    }

    [Fact]
    public async Task InsertRoutine_WithoutExercises_ReturnsCorrectResult()
    {
        // Arrange
        Routine routine = CreateTestRoutine(withExercises:false);

        A.CallTo(() => _dapperDb.QuerySingleAsync<int>(
                                A<string>.That.Contains("INSERT INTO `routine`"), routine))
                                .Returns(0000000001);
        A.CallTo(() => _dateTimeProvider.Now()).Returns(new DateTime(2001, 01, 01, 00, 01, 01));

        // Act
        var result = await _sut.InsertRoutine(routine);

        // Assert
        Assert.Equal(0000000001, result);
        A.CallTo(() => _dapperDb.QuerySingleAsync<int>(
            A<string>.That.Contains("INSERT INTO `routine`"), A<Routine>._)).MustHaveHappened();
        A.CallTo(() => _dapperDb.ExecuteAsync(
            A<string>.That.Contains("INSERT INTO `exercise`"), A<List<Exercise>>._)).MustNotHaveHappened();
    }

    [Fact]
    public async Task InsertRoutine_WithExercises_ReturnsCorrectResult()
    {
        // Arrange
        Routine routine = CreateTestRoutine(withExercises:true);

        A.CallTo(() => _dapperDb.QuerySingleAsync<int>(A<string>.That.Contains("INSERT INTO `routine`"), routine))
                                .Returns(0000000001);
        A.CallTo(() => _dateTimeProvider.Now()).Returns(new DateTime(2001, 01, 01, 00, 01, 01));

        // Act
        var result = await _sut.InsertRoutine(routine);

        // Assert
        Assert.Equal(0000000001, result);
        A.CallTo(() => _dapperDb.QuerySingleAsync<int>(
            A<string>.That.Contains("INSERT INTO `routine`"), A<Routine>._)).MustHaveHappened();
        A.CallTo(() => _dapperDb.ExecuteAsync(
            A<string>.That.Contains("INSERT INTO `exercise`"), A<List<Exercise>>._)).MustHaveHappened();    
    }

    [Fact]
    public async Task InsertRoutine_PopulatesTimestampsCorrectly()
    {
        // Arrange
        var capturedRoutine = A.Captured<Routine>();
        Routine routine = CreateTestRoutine(withExercises: false);

        A.CallTo(() => _dapperDb.QuerySingleAsync<int>(A<string>.That.Contains("INSERT INTO `routine`"), capturedRoutine._)).Returns(0000000001);
        A.CallTo(() => _dapperDb.ExecuteAsync(A<string>.That.Contains("INSERT INTO `exercise`"), A<List<Exercise>>._));
        A.CallTo(() => _dateTimeProvider.Now()).Returns(new DateTime(2001, 01, 01, 00, 01, 01));

        // Act
        var result = await _sut.InsertRoutine(routine);

        // Assert
        Assert.Null(capturedRoutine.Values.FirstOrDefault()?.LastPracticedAt);
        Assert.Equal(0, capturedRoutine.Values.FirstOrDefault()?.PracticeCount);
        Assert.Equal(_dateTimeProvider.Now(), capturedRoutine.Values.FirstOrDefault()?.LastUpdatedAt);
        Assert.Equal(_dateTimeProvider.Now(), capturedRoutine.Values.FirstOrDefault()?.CreatedAt);
    }

    [Fact]
    public async Task InsertRoutine_PopulatesExercisesTimestampsCorrectly()
    {
        // Arrange
        var capturedExercises = A.Captured<List<Exercise>>();
        Routine routine = CreateTestRoutine(withExercises: true);

        A.CallTo(() => _dapperDb.QuerySingleAsync<int>(A<string>.That.Contains("INSERT INTO `routine`"), A<Routine>._)).Returns(0000000001);
        A.CallTo(() => _dapperDb.ExecuteAsync(A<string>.That.Contains("INSERT INTO `exercise`"), capturedExercises._)).Returns(2);
        A.CallTo(() => _dateTimeProvider.Now()).Returns(new DateTime(2001, 01, 01, 00, 01, 01));

        // Act
        var result = await _sut.InsertRoutine(routine);

        // Assert
        Assert.NotEmpty(capturedExercises.Values);
        Assert.Equal(routine.Id, capturedExercises.Values.FirstOrDefault()?.FirstOrDefault()?.RoutineId);
        Assert.Equal(routine.UserId, capturedExercises.Values.FirstOrDefault()?.FirstOrDefault()?.UserId);
        Assert.Null(capturedExercises.Values.FirstOrDefault()?.FirstOrDefault()?.LastPracticedAt);
        Assert.Equal(0, capturedExercises.Values.FirstOrDefault()?.FirstOrDefault()?.PracticeCount);
        Assert.Equal(_dateTimeProvider.Now(), capturedExercises.Values.FirstOrDefault()?.FirstOrDefault()?.LastUpdatedAt);
        Assert.Equal(_dateTimeProvider.Now(), capturedExercises.Values.FirstOrDefault()?.FirstOrDefault()?.CreatedAt);
    }

    [Fact]
    public async Task GetRoutine_ReturnsCorrectRoutine()
    {
        // Arrange
        var expectedRoutine = CreateTestRoutine(withExercises:true);
        A.CallTo(() => _dapperDb.QuerySingleAsync<Routine>(A<string>.That.Contains("SELECT * FROM `routine`"), A<DynamicParameters>._)).Returns(expectedRoutine);
        A.CallTo(() => _dapperDb.QueryAsync<Exercise>(A<string>.That.Contains("SELECT * FROM `exercise`"), A<DynamicParameters>._)).Returns(expectedRoutine.Exercises);

        // Act
        var result = await _sut.GetRoutine(0000000001);

        // Assert
        Assert.NotNull(result);

        A.CallTo(() => _dapperDb.QuerySingleAsync<Routine>(
            A<string>.That.Contains("SELECT * FROM `routine`"), A<DynamicParameters>.That.Matches(x => x.Get<int>("@Id") == 0000000001))).MustHaveHappened();
        A.CallTo(() => _dapperDb.QueryAsync<Exercise>(
            A<string>.That.Contains("SELECT * FROM `exercise` WHERE routine_id"), A<DynamicParameters>.That.Matches(x => x.Get<int>("@Id") == 0000000001))).MustHaveHappened();

        Assert.Equal(expectedRoutine.Id, result.Id);
        Assert.Equal(expectedRoutine.Name, result.Name);
        Assert.Equal(expectedRoutine.CreatedAt, result.CreatedAt);
        Assert.Equal(expectedRoutine.LastPracticedAt, result.LastPracticedAt);
        Assert.Equal(expectedRoutine.LastUpdatedAt, result.LastUpdatedAt);
        Assert.NotEmpty(result.Exercises);
        Assert.Equal(expectedRoutine.Exercises[0], result.Exercises[0]);
    }

    [Fact]
    public async Task GetRoutines_ReturnsListOfRoutines()
    {
        // Arrange
        var expectedRoutine = CreateTestRoutine();
        A.CallTo(() => _dapperDb.QueryAsync<Routine>(A<string>.That.Contains("SELECT * FROM `routine`"))).Returns(new List<Routine>(){expectedRoutine});

        // Act
        var result = await _sut.GetRoutines();

        // Assert
        Assert.NotEmpty(result);
        A.CallTo(() => _dapperDb.QueryAsync<Routine>(
            A<string>.That.Contains("SELECT * FROM `routine`"))).MustHaveHappened();
        Assert.Single(result);
    }

    private static Routine CreateTestRoutine(bool withExercises = false)
    {
        var exercises = new List<Exercise>();
        if (withExercises)
        {
            exercises.Add(new Exercise() { Name = "2 Octave Scales" });
            exercises.Add(new Exercise() { Name = "20 Hanon Exercises" });
        }
        return new()
        {
            Id = 1,
            Name = "A Routine",
            CreatedAt = new DateTime(2001, 01, 01, 12, 01, 59),
            LastPracticedAt = new DateTime(2010, 01, 01, 09, 00, 00),
            LastUpdatedAt = new DateTime(2005, 06, 01, 21, 00, 00),
            Exercises = exercises
        };
    }
}