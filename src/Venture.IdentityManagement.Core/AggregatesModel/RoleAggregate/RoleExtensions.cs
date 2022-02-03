using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Venture.IdentityManagement.Core
{
    public static class RoleExtensions
    {
        public static RoleDto ToDto(this Role role)
        {
            return new ()
            {
                RoleId = role.RoleId
            };
        }
        
        public static async Task<List<RoleDto>> ToDtosAsync(this IQueryable<Role> roles, CancellationToken cancellationToken)
        {
            return await roles.Select(x => x.ToDto()).ToListAsync(cancellationToken);
        }
        
    }
}
