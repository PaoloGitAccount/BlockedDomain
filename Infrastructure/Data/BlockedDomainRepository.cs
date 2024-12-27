using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BlockedDomainRepository : IBlockedDomainRepository
    {
        private readonly BlockedDomainContext _context;

        public BlockedDomainRepository(BlockedDomainContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BlockedDomain entity)
        {
            await _context.BlockedDomains.AddAsync(entity);
        }

        public async Task<BlockedDomain> GetByDomainAsync(string domain)
        {
            return await _context.BlockedDomains.SingleOrDefaultAsync(b => b.Domain == domain);
        }

        public async Task<IEnumerable<BlockedDomain>> GetAllAsync()
        {
            return await _context.BlockedDomains.ToListAsync();
        }
    }
}
