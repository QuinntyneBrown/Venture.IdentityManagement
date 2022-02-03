using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Venture.IdentityManagement.Core.Interfaces;
using Venture.IdentityManagement.SharedKernal;

namespace Venture.IdentityManagement.Core
{

    public class GetTenantByIdRequest: IRequest<GetTenantByIdResponse>
    {
        public Guid TenantId { get; set; }
    }
    public class GetTenantByIdResponse: ResponseBase
    {
        public TenantDto Tenant { get; set; }
    }
    public class GetTenantByIdHandler: IRequestHandler<GetTenantByIdRequest, GetTenantByIdResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetTenantByIdHandler> _logger;
    
        public GetTenantByIdHandler(IIdentityManagementDbContext context, ILogger<GetTenantByIdHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetTenantByIdResponse> Handle(GetTenantByIdRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Tenant = (await _context.Tenants.SingleOrDefaultAsync(x => x.TenantId == request.TenantId)).ToDto()
            };
        }
        
    }

}
