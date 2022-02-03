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

    public class GetPrivilegesPageRequest: IRequest<GetPrivilegesPageResponse>
    {
        public int PageSize { get; set; }
        public int Index { get; set; }
    }
    public class GetPrivilegesPageResponse: ResponseBase
    {
        public int Length { get; set; }
        public List<PrivilegeDto> Entities { get; set; }
    }
    public class GetPrivilegesPageHandler: IRequestHandler<GetPrivilegesPageRequest, GetPrivilegesPageResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetPrivilegesPageHandler> _logger;
    
        public GetPrivilegesPageHandler(IIdentityManagementDbContext context, ILogger<GetPrivilegesPageHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetPrivilegesPageResponse> Handle(GetPrivilegesPageRequest request, CancellationToken cancellationToken)
        {
            var query = from privilege in _context.Privileges
                select privilege;
            
            var length = await _context.Privileges.CountAsync();
            
            var privileges = await query.Page(request.Index, request.PageSize)
                .Select(x => x.ToDto()).ToListAsync();
            
            return new ()
            {
                Length = length,
                Entities = privileges
            };
        }
        
    }

}
