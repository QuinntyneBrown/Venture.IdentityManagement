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

    public class UpdateTenantValidator: AbstractValidator<UpdateTenantRequest>
    {
        public UpdateTenantValidator()
        {
            RuleFor(request => request.Tenant).NotNull();
            RuleFor(request => request.Tenant).SetValidator(new TenantValidator());
        }
    
    }
    public class UpdateTenantRequest: IRequest<UpdateTenantResponse>
    {
        public TenantDto Tenant { get; set; }
    }
    public class UpdateTenantResponse: ResponseBase
    {
        public TenantDto Tenant { get; set; }
    }
    public class UpdateTenantHandler: IRequestHandler<UpdateTenantRequest, UpdateTenantResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<UpdateTenantHandler> _logger;
    
        public UpdateTenantHandler(IIdentityManagementDbContext context, ILogger<UpdateTenantHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<UpdateTenantResponse> Handle(UpdateTenantRequest request, CancellationToken cancellationToken)
        {
            var tenant = await _context.Tenants.SingleAsync(x => x.TenantId == request.Tenant.TenantId);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Tenant = tenant.ToDto()
            };
        }
        
    }

}
