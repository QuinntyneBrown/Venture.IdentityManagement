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

    public class UpdateRoleValidator: AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleValidator()
        {
            RuleFor(request => request.Role).NotNull();
            RuleFor(request => request.Role).SetValidator(new RoleValidator());
        }
    
    }
    public class UpdateRoleRequest: IRequest<UpdateRoleResponse>
    {
        public RoleDto Role { get; set; }
    }
    public class UpdateRoleResponse: ResponseBase
    {
        public RoleDto Role { get; set; }
    }
    public class UpdateRoleHandler: IRequestHandler<UpdateRoleRequest, UpdateRoleResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<UpdateRoleHandler> _logger;
    
        public UpdateRoleHandler(IIdentityManagementDbContext context, ILogger<UpdateRoleHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<UpdateRoleResponse> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.SingleAsync(x => x.RoleId == request.Role.RoleId);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Role = role.ToDto()
            };
        }
        
    }

}
