using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Venture.IdentityManagement.Core.Interfaces;
using Venture.IdentityManagement.SharedKernal;

namespace Venture.IdentityManagement.Core
{

    public class CreateRoleValidator: AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleValidator()
        {
            RuleFor(request => request.Role).NotNull();
            RuleFor(request => request.Role).SetValidator(new RoleValidator());
        }
    
    }
    public class CreateRoleRequest: IRequest<CreateRoleResponse>
    {
        public RoleDto Role { get; set; }
    }
    public class CreateRoleResponse: ResponseBase
    {
        public RoleDto Role { get; set; }
    }
    public class CreateRoleHandler: IRequestHandler<CreateRoleRequest, CreateRoleResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<CreateRoleHandler> _logger;
    
        public CreateRoleHandler(IIdentityManagementDbContext context, ILogger<CreateRoleHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var role = new Role();
            
            _context.Roles.Add(role);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Role = role.ToDto()
            };
        }
        
    }

}
