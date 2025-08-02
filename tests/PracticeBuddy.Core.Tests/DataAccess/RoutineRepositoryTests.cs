using System.Collections.ObjectModel;
using Dapper;
using FakeItEasy;
using PracticeBuddy.Core.Constants;
using PracticeBuddy.Core.DataAccess;
using PracticeBuddy.Core.DataAccess.Repositories;
using PracticeBuddy.Core.DataModels;

namespace PracticeBuddy.Core.Tests.DataAccess.Repositories;

public class RoutineRepositoryTests
{
    private IDapperDb _dapperDb;
    private IDateTimeProvider _dateTimeProvider;
    private RoutineRepository _sut;

    public RoutineRepositoryTests()
    {
        _dapperDb = A.Fake<IDapperDb>();
        _dateTimeProvider = A.Fake<IDateTimeProvider>();

        _sut = new RoutineRepository(_dapperDb, _dateTimeProvider);
    }

    [Fact]
    public async Task InsertRoutine_ReturnsCorrectResult()
    {
        // Arrange
        Routine routine = CreateTestRoutine();

        A.CallTo(() => _dapperDb.QuerySingleAsync<int>(A<string>.That.Contains("INSERT INTO `routine`"), routine))
                                .Returns(0000000001);
        A.CallTo(() => _dateTimeProvider.Now()).Returns(new DateTime(2001, 01, 01, 00, 01, 01));

        // Act
        var result = await _sut.InsertRoutine(routine);

        // Assert
        Assert.Equal(0000000001, result);
        A.CallTo(() => _dapperDb.QuerySingleAsync<int>(
            A<string>.That.Contains("INSERT INTO `routine`"), A<Routine>._)).MustHaveHappened();
    }

    [Fact]
    public async Task InsertRoutine_PopulatesTimestampsCorrectly()
    {
        // Arrange
        var capturedRoutine = A.Captured<Routine>();
        Routine routine = CreateTestRoutine();

        A.CallTo(() => _dapperDb.QuerySingleAsync<int>(A<string>.That.Contains("INSERT INTO `routine`"), capturedRoutine._))
                                .Returns(0000000001);
        A.CallTo(() => _dateTimeProvider.Now()).Returns(new DateTime(2001,01,01,00,01,01));

        // Act
        var result = await _sut.InsertRoutine(routine);

        // Assert
        Assert.Null(capturedRoutine.Values.FirstOrDefault()?.LastPracticedAt);
        Assert.Equal(0, capturedRoutine.Values.FirstOrDefault()?.PracticeCount);
        Assert.Equal(_dateTimeProvider.Now(), capturedRoutine.Values.FirstOrDefault()?.LastUpdatedAt);
        Assert.Equal(_dateTimeProvider.Now(), capturedRoutine.Values.FirstOrDefault()?.CreatedAt);
    }

    [Fact]
    public async Task GetRoutine_ReturnsCorrectRoutine()
    {
        // Arrange
        var expectedRoutine = CreateTestRoutine();
        A.CallTo(() => _dapperDb.QuerySingleAsync<Routine>(A<string>.That.Contains("SELECT * FROM `routine`"), A<DynamicParameters>._)).Returns(expectedRoutine);

        // Act
        var result = await _sut.GetRoutine(0000000001);

        // Assert
        Assert.NotNull(result);

        // TODO: This assertion is not concrete enough. Should pick up the dictionary
        A.CallTo(() => _dapperDb.QuerySingleAsync<Routine>(
            A<string>.That.Contains("SELECT * FROM `routine`"), A<DynamicParameters>.That.Matches(x => x.Get<int>("@Id") == 0000000001))).MustHaveHappened();

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

    private static Routine CreateTestRoutine()
    {
        return new()
        {
            Name = "A Routine",
            CreatedAt = new DateTime(2001, 01, 01, 12, 01, 59),
            LastPracticedAt = new DateTime(2010, 01, 01, 09, 00, 00),
            LastUpdatedAt = new DateTime(2005, 06, 01, 21, 00, 00),
            Exercises = new List<Exercise>()
            {
                new Exercise(){ Name = "2 Octave Scales" }
            }
        };
    }
}