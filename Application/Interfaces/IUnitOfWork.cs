using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBlockedDomainRepository BlockedDomainRepository { get; }
        Task<int> CompleteAsync();
    }
}
