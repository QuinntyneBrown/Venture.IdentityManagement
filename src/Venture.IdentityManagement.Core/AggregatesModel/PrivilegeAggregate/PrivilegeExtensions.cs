using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Venture.IdentityManagement.Core
{
    public static class PrivilegeExtensions
    {
        public static PrivilegeDto ToDto(this Privilege privilege)
        {
            return new ()
            {
                PrivilegeId = privilege.PrivilegeId
            };
        }
        
        public static async Task<List<PrivilegeDto>> ToDtosAsync(this IQueryable<Privilege> privileges, CancellationToken cancellationToken)
        {
            return await privileges.Select(x => x.ToDto()).ToListAsync(cancellationToken);
        }
        
    }
}
