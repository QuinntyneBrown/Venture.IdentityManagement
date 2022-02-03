using Venture.IdentityManagement.Core;
using Venture.IdentityManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Venture.IdentityManagement.Infrastructure.Data
{
    public class IdentityManagementDbContext: DbContext, IIdentityManagementDbContext
    {
        public DbSet<Tenant> Tenants { get; private set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<Role> Roles { get; private set; }
        public DbSet<Privilege> Privileges { get; private set; }
        public IdentityManagementDbContext(DbContextOptions options)
            :base(options) { }

    }
}
