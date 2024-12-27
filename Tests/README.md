
# Blocked Domain Application - Tests

## Overview
This README provides detailed information about the testing strategy and structure for the Blocked Domain Application. The project employs comprehensive unit tests, integration tests, and end-to-end tests to ensure the robustness, reliability, and correctness of the application.

## Table of Contents
- [Testing Strategy](#testing-strategy)
- [Technologies Used](#technologies-used)
- [Test Structure](#test-structure)
- [Running Tests](#running-tests)
- [Unit Tests](#unit-tests)
- [Integration Tests](#integration-tests)
- [End-to-End Tests](#end-to-end-tests)
- [Advanced Testing Practices](#advanced-testing-practices)

## Testing Strategy
The testing strategy for the Blocked Domain Application encompasses multiple levels of testing to ensure comprehensive coverage:
- **Unit Tests**: Focus on individual components, ensuring they work in isolation.
- **Integration Tests**: Verify the interaction between multiple components.
- **End-to-End Tests**: Test the complete flow of the application from end to end.

## Technologies Used
- xUnit: A testing framework for .NET.
- Moq: A mocking library for .NET.
- Microsoft.AspNetCore.Mvc.Testing: For integration and end-to-end testing.
- Newtonsoft.Json: For JSON serialization and deserialization in tests.

## Test Structure
The tests are organized into three main categories:
```
/Tests
    /UnitTests
        BlockedDomainServiceTests.cs
    /IntegrationTests
        BlockedDomainIntegrationTests.cs
    /EndToEndTests
        BlockedDomainEndToEndTests.cs
```

## Running Tests
To run all the tests in the project, use the following command:
```sh
dotnet test
```

## Unit Tests
### Purpose
Unit tests focus on testing individual components in isolation. They ensure that each component behaves as expected under various conditions. we use the Moq Framework for mocking dependencies and xUnit for writing the test cases.

### Example: BlockedDomainServiceTests.cs
```csharp
using Moq;
using Xunit;
using Application.Interfaces;
using Application.UseCases;
using Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

public class BlockedDomainServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly BlockedDomainService _service;

    public BlockedDomainServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _service = new BlockedDomainService(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task AddBlockedDomainAsync_ShouldAddDomain()
    {
        var domain = new BlockedDomain { Domain = "example.com" };

        await _service.AddBlockedDomainAsync(domain);

        _mockUnitOfWork.Verify(uow => uow.BlockedDomainRepository.AddAsync(domain), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task IsDomainBlockedAsync_ShouldReturnTrue_WhenDomainIsBlocked()
    {
        var domain = new BlockedDomain { Domain = "example.com" };
        _mockUnitOfWork.Setup(uow => uow.BlockedDomainRepository.GetByDomainAsync(domain.Domain)).ReturnsAsync(domain);

        var result = await _service.IsDomainBlockedAsync(domain.Domain);

        Assert.True(result);
    }

    [Fact]
    public async Task IsDomainBlockedAsync_ShouldReturnFalse_WhenDomainIsNotBlocked()
    {
        var domain = "example.com";
        _mockUnitOfWork.Setup(uow => uow.BlockedDomainRepository.GetByDomainAsync(domain)).ReturnsAsync((BlockedDomain)null);

        var result = await _service.IsDomainBlockedAsync(domain);

        Assert.False(result);
    }

    [Fact]
    public async Task GetAllBlockedDomainsAsync_ShouldReturnAllDomains()
    {
        var domains = new List<BlockedDomain>
        {
            new BlockedDomain { Domain = "example.com" },
            new BlockedDomain { Domain = "test.com" }
        };
        _mockUnitOfWork.Setup(uow => uow.BlockedDomainRepository.GetAllAsync()).ReturnsAsync(domains);

        var result = await _service.GetAllBlockedDomainsAsync();

        Assert.Equal(2, result.Count);
    }
}
```

## Integration Tests
### Purpose
Integration tests verify the interaction between multiple components to ensure they work together correctly.

### Example: BlockedDomainIntegrationTests.cs
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

public class BlockedDomainIntegrationTests : IClassFixture<WebApplicationFactory<WebApi.Startup>>
{
    private readonly HttpClient _client;

    public BlockedDomainIntegrationTests(WebApplicationFactory<WebApi.Startup> factory)
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

