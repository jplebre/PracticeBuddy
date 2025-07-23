using Microsoft.AspNetCore.Mvc.Testing;

namespace PracticeBuddy.API.IntegrationTests.Controllers;

public class IndexPageTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public IndexPageTests(
        CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    // [Fact]
    // public async Task Post_Thing_ReturnsCorrectResponse()
    // {
    //     // Arrange
    //     var content = new { bored = true, busy = false };

    //     //Act
    //     var response = await _client.PostAsync("/example", JsonContent.Create(content));

    //     // Assert
    //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //     Assert.Contains("bored", await response.Content.ReadAsStringAsync());
    //     Assert.Contains("busy", await response.Content.ReadAsStringAsync());
    // }
}