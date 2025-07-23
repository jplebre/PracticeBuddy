using System.Net;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticeBuddy.API.Controllers;
using PracticeBuddy.Core.DataAccess;

namespace PracticeBuddy.API.Tests.Controllers;

public class HealthControllerTests
{
    private ILogger<HealthController> _logger;

    public HealthControllerTests()
    {
        _logger = A.Fake<ILogger<HealthController>>();
    }

    // A.CallTo(() => dapperDb.QueryAsync<string>("SELECT '1'")).Returns(new List<string> { "1" });
    // A.CallTo(() => dapperDb.QueryAsync<string>("SELECT '1'")).MustHaveHappenedOnceExactly();

    [Fact]
    public async Task WhenCallingHealthCheck_IfDatabaseIsOk_ReturnsOKResult()
    {
        // Arrange
        var dapperDb = A.Fake<IDapperDb>();
        A.CallTo(() => dapperDb.PingAsync()).Returns(true);

        var controller = new HealthController(_logger, dapperDb);

        // Act
        var result = await controller.GetHealthAsync();

        // Assert
        var model = Assert.IsType<OkObjectResult>(result);

        // Assert.Equals(result, 1);
        Assert.Equal((int)HttpStatusCode.OK, model.StatusCode);
        A.CallTo(() => dapperDb.PingAsync()).MustHaveHappenedOnceExactly();
        Assert.Equal("Database is OK!", model.Value);
    }

    [Fact]
    public async Task WhenCallingHealthCheck_IfDatabaseIsNotOk_Returns500Error()
    {
        // Arrange
        var dapperDb = A.Fake<IDapperDb>();
        A.CallTo(() => dapperDb.PingAsync()).Returns(false);

        var controller = new HealthController(_logger, dapperDb);

        // Act
        var result = await controller.GetHealthAsync();

        // Assert
        var model = Assert.IsType<ObjectResult>(result);

        // Assert.Equals(result, 1);
        Assert.Equal((int)HttpStatusCode.InternalServerError, model.StatusCode);
        A.CallTo(() => dapperDb.PingAsync()).MustHaveHappenedOnceExactly();
        Assert.Equal("database unreachable", model.Value);
    }
}