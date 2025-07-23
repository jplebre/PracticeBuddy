using FakeItEasy;
using PracticeBuddy.Core.DataAccess;
using PracticeBuddy.Core.DataAccess.Repositories;
using PracticeBuddy.Core.Models;

namespace PracticeBuddy.Core.Tests.DataAccess.Repositories;

public class DapperTests
{
    [Fact(Skip = "Not Yet Implemented")]
    public async Task InsertRoutine_ReturnsCorrectResult()
    {
        var dapperDb = A.Fake<IDapperDb>();
        A.CallTo(() => dapperDb.QuerySingleAsync<int>("SELECT '1'")).Returns(1);

        var repo = new RoutineRepository(dapperDb);
        var result = await repo.InsertRoutine(new Routine());

        Assert.Equal(1, result);
    }
}