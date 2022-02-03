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

    public class GetTenantsPageRequest: IRequest<GetTenantsPageResponse>
    {
        public int PageSize { get; set; }
        public int Index { get; set; }
    }
    public class GetTenantsPageResponse: ResponseBase
    {
        public int Length { get; set; }
        public List<TenantDto> Entities { get; set; }
    }
    public class GetTenantsPageHandler: IRequestHandler<GetTenantsPageRequest, GetTenantsPageResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetTenantsPageHandler> _logger;
    
        public GetTenantsPageHandler(IIdentityManagementDbContext context, ILogger<GetTenantsPageHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetTenantsPageResponse> Handle(GetTenantsPageRequest request, CancellationToken cancellationToken)
        {
            var query = from tenant in _context.Tenants
                select tenant;
            
            var length = await _context.Tenants.CountAsync();
            
            var tenants = await query.Page(request.Index, request.PageSize)
                .Select(x => x.ToDto()).ToListAsync();
            
            return new ()
            {
                Length = length,
                Entities = tenants
            };
        }
        
    }

}
