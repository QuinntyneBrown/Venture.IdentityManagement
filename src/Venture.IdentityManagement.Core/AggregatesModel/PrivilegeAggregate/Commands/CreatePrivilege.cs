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

    public class CreatePrivilegeValidator: AbstractValidator<CreatePrivilegeRequest>
    {
        public CreatePrivilegeValidator()
        {
            RuleFor(request => request.Privilege).NotNull();
            RuleFor(request => request.Privilege).SetValidator(new PrivilegeValidator());
        }
    
    }
    public class CreatePrivilegeRequest: IRequest<CreatePrivilegeResponse>
    {
        public PrivilegeDto Privilege { get; set; }
    }
    public class CreatePrivilegeResponse: ResponseBase
    {
        public PrivilegeDto Privilege { get; set; }
    }
    public class CreatePrivilegeHandler: IRequestHandler<CreatePrivilegeRequest, CreatePrivilegeResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<CreatePrivilegeHandler> _logger;
    
        public CreatePrivilegeHandler(IIdentityManagementDbContext context, ILogger<CreatePrivilegeHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<CreatePrivilegeResponse> Handle(CreatePrivilegeRequest request, CancellationToken cancellationToken)
        {
            var privilege = new Privilege();
            
            _context.Privileges.Add(privilege);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Privilege = privilege.ToDto()
            };
        }
        
    }

}
