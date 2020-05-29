using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class CheckoutDbContext : DbContext, ICheckoutDbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public CheckoutDbContext(DbContextOptions<CheckoutDbContext> options) : base(options)
        {
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CheckoutDbContext).Assembly);
        }
    }
}