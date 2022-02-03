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

    public class GetRoleByIdRequest: IRequest<GetRoleByIdResponse>
    {
        public Guid RoleId { get; set; }
    }
    public class GetRoleByIdResponse: ResponseBase
    {
        public RoleDto Role { get; set; }
    }
    public class GetRoleByIdHandler: IRequestHandler<GetRoleByIdRequest, GetRoleByIdResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetRoleByIdHandler> _logger;
    
        public GetRoleByIdHandler(IIdentityManagementDbContext context, ILogger<GetRoleByIdHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetRoleByIdResponse> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Role = (await _context.Roles.SingleOrDefaultAsync(x => x.RoleId == request.RoleId)).ToDto()
            };
        }
        
    }

}
