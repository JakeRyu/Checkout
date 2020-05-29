using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ICheckoutDbContext
    {
        DbSet<Payment> Payments { get; set; }
        
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}