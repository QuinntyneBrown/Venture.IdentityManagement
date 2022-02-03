using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Venture.IdentityManagement.Core.Interfaces
{
    public interface IIdentityManagementDbContext
    {
        DbSet<Tenant> Tenants { get; }
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<Privilege> Privileges { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
