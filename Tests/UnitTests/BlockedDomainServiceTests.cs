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
