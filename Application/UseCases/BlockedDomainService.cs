using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.Entities;

namespace Application.UseCases
{
    public class BlockedDomainService : IBlockedDomainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlockedDomainService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddBlockedDomainAsync(BlockedDomain blockedDomain)
        {
            await _unitOfWork.BlockedDomainRepository.AddAsync(blockedDomain);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> IsDomainBlockedAsync(string domain)
        {
            var blockedDomain = await _unitOfWork.BlockedDomainRepository.GetByDomainAsync(domain);
            return blockedDomain != null;
        }

        public async Task<List<BlockedDomain>> GetAllBlockedDomainsAsync()
        {
            return (await _unitOfWork.BlockedDomainRepository.GetAllAsync()).ToList();
        }
    }
}
