using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Venture.IdentityManagement.Core
{
    public static class TenantExtensions
    {
        public static TenantDto ToDto(this Tenant tenant)
        {
            return new ()
            {
                TenantId = tenant.TenantId
            };
        }
        
        public static async Task<List<TenantDto>> ToDtosAsync(this IQueryable<Tenant> tenants, CancellationToken cancellationToken)
        {
            return await tenants.Select(x => x.ToDto()).ToListAsync(cancellationToken);
        }
        
    }
}
