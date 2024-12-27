using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlockedDomainRepository
    {
        Task AddAsync(BlockedDomain entity);
        Task<BlockedDomain> GetByDomainAsync(string domain);
        Task<IEnumerable<BlockedDomain>> GetAllAsync();
    }
}
