using System.Net;
using Microsoft.AspNetCore.Mvc;
using PracticeBuddy.API.Controllers;

namespace PracticeBuddy.API.Tests.Controllers;

public class ExampleControllerTests
{
    [Fact]
    public void Post()
    {
        // Arrange
        // var mockRepo = new Mock<IBrainstormSessionRepository>();
        // mockRepo.Setup(repo => repo.ListAsync()).ReturnsAsync(GetTestSessions());
        var controller = new ExampleController();
        var content = new { content = "content!" };

        // Act
        var result = controller.PostItem(content);

        // Assert
        var model = Assert.IsType<OkObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, model.StatusCode);
        Assert.Equal(content, model.Value.GetType().GetProperty("Thing").GetValue(model.Value, null));
    }
}