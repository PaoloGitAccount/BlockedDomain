using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlockedDomainService
    {
        Task<bool> IsDomainBlockedAsync(string domain);
        Task AddBlockedDomainAsync(BlockedDomain blockedDomain);
        Task<List<BlockedDomain>> GetAllBlockedDomainsAsync();
    }
}
