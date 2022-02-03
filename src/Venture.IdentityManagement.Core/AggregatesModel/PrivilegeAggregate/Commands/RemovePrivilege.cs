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

    public class RemovePrivilegeRequest: IRequest<RemovePrivilegeResponse>
    {
        public Guid PrivilegeId { get; set; }
    }
    public class RemovePrivilegeResponse: ResponseBase
    {
        public PrivilegeDto Privilege { get; set; }
    }
    public class RemovePrivilegeHandler: IRequestHandler<RemovePrivilegeRequest, RemovePrivilegeResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<RemovePrivilegeHandler> _logger;
    
        public RemovePrivilegeHandler(IIdentityManagementDbContext context, ILogger<RemovePrivilegeHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<RemovePrivilegeResponse> Handle(RemovePrivilegeRequest request, CancellationToken cancellationToken)
        {
            var privilege = await _context.Privileges.SingleAsync(x => x.PrivilegeId == request.PrivilegeId);
            
            _context.Privileges.Remove(privilege);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Privilege = privilege.ToDto()
            };
        }
        
    }

}
