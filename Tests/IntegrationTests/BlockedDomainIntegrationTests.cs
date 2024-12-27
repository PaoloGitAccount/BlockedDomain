using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;

public class BlockedDomainIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;

    public BlockedDomainIntegrationTests(WebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddBlockedDomain_ShouldReturnOk()
    {
        var domain = new { Domain = "integrationtest.com" };
        var content = new StringContent(JsonConvert.SerializeObject(domain), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/BlockedDomain", content);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task IsDomainBlocked_ShouldReturnTrue_WhenDomainIsBlocked()
    {
        var domain = new { Domain = "blockedtest.com" };
        var content = new StringContent(JsonConvert.SerializeObject(domain), Encoding.UTF8, "application/json");

        await _client.PostAsync("/api/BlockedDomain", content);

        var response = await _client.GetAsync($"/api/BlockedDomain/isBlocked?domain={domain.Domain}");

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        Assert.True(bool.Parse(result));
    }

    [Fact]
    public async Task GetAllBlockedDomains_ShouldReturnBlockedDomains()
    {
        var response = await _client.GetAsync("/api/BlockedDomain");

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(result);
    }
}
