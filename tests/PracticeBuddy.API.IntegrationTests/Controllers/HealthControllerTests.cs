using System.Text.Json.Nodes;

namespace PracticeBuddy.API.IntegrationTests.Controllers;

public class HealthControllerTests : IClassFixture<PracticeBuddyDbFixture>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public HealthControllerTests(PracticeBuddyDbFixture fixture)
    {
        _factory = new CustomWebApplicationFactory<Program>(fixture);
    }

    [Theory]
    [InlineData("/health")]
    public async Task Get_HealthCheck_Returns200(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }

    [Theory]
    [InlineData("/healthz/live")]
    public async Task Get_HealthLivelinessCheck_Returns200(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", JsonNode.Parse(body)!["status"].GetValue<string>());
        Assert.Equal("Healthy", JsonNode.Parse(body)!["results"]![0]!["status"]!.GetValue<string>());
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }

    [Theory]
    [InlineData("/healthz/ready")]
    public async Task Get_HealthReadynessCheck_Returns200(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", JsonNode.Parse(body)!["status"].GetValue<string>());
        Assert.Equal("Healthy", JsonNode.Parse(body)!["results"]![0]!["status"]!.GetValue<string>());
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}