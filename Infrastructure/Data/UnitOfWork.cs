using Application.Interfaces;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlockedDomainContext _context;

        public UnitOfWork(BlockedDomainContext context)
        {
            _context = context;
            BlockedDomainRepository = new BlockedDomainRepository(_context);
        }

        public IBlockedDomainRepository BlockedDomainRepository { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
