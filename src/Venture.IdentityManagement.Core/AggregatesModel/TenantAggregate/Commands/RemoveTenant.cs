using FluentValidation;
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

    public class RemoveTenantRequest: IRequest<RemoveTenantResponse>
    {
        public Guid TenantId { get; set; }
    }
    public class RemoveTenantResponse: ResponseBase
    {
        public TenantDto Tenant { get; set; }
    }
    public class RemoveTenantHandler: IRequestHandler<RemoveTenantRequest, RemoveTenantResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<RemoveTenantHandler> _logger;
    
        public RemoveTenantHandler(IIdentityManagementDbContext context, ILogger<RemoveTenantHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<RemoveTenantResponse> Handle(RemoveTenantRequest request, CancellationToken cancellationToken)
        {
            var tenant = await _context.Tenants.SingleAsync(x => x.TenantId == request.TenantId);
            
            _context.Tenants.Remove(tenant);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Tenant = tenant.ToDto()
            };
        }
        
    }

}
