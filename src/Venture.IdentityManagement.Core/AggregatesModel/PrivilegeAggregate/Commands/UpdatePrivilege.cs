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

    public class UpdatePrivilegeValidator: AbstractValidator<UpdatePrivilegeRequest>
    {
        public UpdatePrivilegeValidator()
        {
            RuleFor(request => request.Privilege).NotNull();
            RuleFor(request => request.Privilege).SetValidator(new PrivilegeValidator());
        }
    
    }
    public class UpdatePrivilegeRequest: IRequest<UpdatePrivilegeResponse>
    {
        public PrivilegeDto Privilege { get; set; }
    }
    public class UpdatePrivilegeResponse: ResponseBase
    {
        public PrivilegeDto Privilege { get; set; }
    }
    public class UpdatePrivilegeHandler: IRequestHandler<UpdatePrivilegeRequest, UpdatePrivilegeResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<UpdatePrivilegeHandler> _logger;
    
        public UpdatePrivilegeHandler(IIdentityManagementDbContext context, ILogger<UpdatePrivilegeHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<UpdatePrivilegeResponse> Handle(UpdatePrivilegeRequest request, CancellationToken cancellationToken)
        {
            var privilege = await _context.Privileges.SingleAsync(x => x.PrivilegeId == request.Privilege.PrivilegeId);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Privilege = privilege.ToDto()
            };
        }
        
    }

}
