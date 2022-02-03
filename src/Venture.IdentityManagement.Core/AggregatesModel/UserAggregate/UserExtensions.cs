using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Venture.IdentityManagement.Core
{
    public static class UserExtensions
    {
        public static UserDto ToDto(this User user)
        {
            return new ()
            {
                UserId = user.UserId
            };
        }
        
        public static async Task<List<UserDto>> ToDtosAsync(this IQueryable<User> users, CancellationToken cancellationToken)
        {
            return await users.Select(x => x.ToDto()).ToListAsync(cancellationToken);
        }
        
    }
}
