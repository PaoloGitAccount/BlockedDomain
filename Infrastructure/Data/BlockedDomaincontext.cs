using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.Data
{
    public class BlockedDomainContext : DbContext
    {
        public BlockedDomainContext(DbContextOptions<BlockedDomainContext> options)
            : base(options)
        {
        }

        public DbSet<BlockedDomain> BlockedDomains { get; set; }
        public DbSet<Host> Hosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BlockedDomain>().HasKey(b => b.Domain);
            modelBuilder.Entity<Host>().HasKey(h => h.Id);
        }
    }
}
