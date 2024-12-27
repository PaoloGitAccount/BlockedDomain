using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;

public class BlockedDomainEndToEndTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;

    public BlockedDomainEndToEndTests(WebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CompleteFlow_ShouldBlockAndCheckDomain()
    {
        var domain = new { Domain = "endtoendtest.com" };
        var content = new StringContent(JsonConvert.SerializeObject(domain), Encoding.UTF8, "application/json");

        // Add blocked domain
        var addResponse = await _client.PostAsync("/api/BlockedDomain", content);
        addResponse.EnsureSuccessStatusCode();

        // Check if domain is blocked
        var checkResponse = await _client.GetAsync($"/api/BlockedDomain/isBlocked?domain={domain.Domain}");
        checkResponse.EnsureSuccessStatusCode();

        var result = await checkResponse.Content.ReadAsStringAsync();
        Assert.True(bool.Parse(result));
    }

    [Fact]
    public async Task CompleteFlow_ShouldReturnAllBlockedDomains()
    {
        var domain = new { Domain = "endtoendtest2.com" };
        var content = new StringContent(JsonConvert.SerializeObject(domain), Encoding.UTF8, "application/json");

        // Add blocked domain
        await _client.PostAsync("/api/BlockedDomain", content);

        // Get all blocked domains
        var response = await _client.GetAsync("/api/BlockedDomain");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        Assert.Contains("endtoendtest2.com", result);
    }
}
