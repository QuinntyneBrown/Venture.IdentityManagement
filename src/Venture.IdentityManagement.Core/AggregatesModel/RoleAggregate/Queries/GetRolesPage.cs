using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Venture.IdentityManagement.Core.Interfaces;
using Venture.IdentityManagement.SharedKernal;

namespace Venture.IdentityManagement.Core
{

    public class GetRolesPageRequest: IRequest<GetRolesPageResponse>
    {
        public int PageSize { get; set; }
        public int Index { get; set; }
    }
    public class GetRolesPageResponse: ResponseBase
    {
        public int Length { get; set; }
        public List<RoleDto> Entities { get; set; }
    }
    public class GetRolesPageHandler: IRequestHandler<GetRolesPageRequest, GetRolesPageResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetRolesPageHandler> _logger;
    
        public GetRolesPageHandler(IIdentityManagementDbContext context, ILogger<GetRolesPageHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetRolesPageResponse> Handle(GetRolesPageRequest request, CancellationToken cancellationToken)
        {
            var query = from role in _context.Roles
                select role;
            
            var length = await _context.Roles.CountAsync();
            
            var roles = await query.Page(request.Index, request.PageSize)
                .Select(x => x.ToDto()).ToListAsync();
            
            return new ()
            {
                Length = length,
                Entities = roles
            };
        }
        
    }

}
