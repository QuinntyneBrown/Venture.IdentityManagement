using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Venture.IdentityManagement.Core.Interfaces;
using Venture.IdentityManagement.SharedKernal;

namespace Venture.IdentityManagement.Core
{

    public class CreateTenantValidator: AbstractValidator<CreateTenantRequest>
    {
        public CreateTenantValidator()
        {
            RuleFor(request => request.Tenant).NotNull();
            RuleFor(request => request.Tenant).SetValidator(new TenantValidator());
        }
    
    }
    public class CreateTenantRequest: IRequest<CreateTenantResponse>
    {
        public TenantDto Tenant { get; set; }
    }
    public class CreateTenantResponse: ResponseBase
    {
        public TenantDto Tenant { get; set; }
    }
    public class CreateTenantHandler: IRequestHandler<CreateTenantRequest, CreateTenantResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<CreateTenantHandler> _logger;
    
        public CreateTenantHandler(IIdentityManagementDbContext context, ILogger<CreateTenantHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<CreateTenantResponse> Handle(CreateTenantRequest request, CancellationToken cancellationToken)
        {
            var tenant = new Tenant(new CreateTenantDomainEvent(request.Tenant.Name));
            
            _context.Tenants.Add(tenant);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Tenant = tenant.ToDto()
            };
        }
        
    }

}
