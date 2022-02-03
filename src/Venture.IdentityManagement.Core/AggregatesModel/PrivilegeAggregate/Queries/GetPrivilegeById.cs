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

    public class GetPrivilegeByIdRequest: IRequest<GetPrivilegeByIdResponse>
    {
        public Guid PrivilegeId { get; set; }
    }
    public class GetPrivilegeByIdResponse: ResponseBase
    {
        public PrivilegeDto Privilege { get; set; }
    }
    public class GetPrivilegeByIdHandler: IRequestHandler<GetPrivilegeByIdRequest, GetPrivilegeByIdResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetPrivilegeByIdHandler> _logger;
    
        public GetPrivilegeByIdHandler(IIdentityManagementDbContext context, ILogger<GetPrivilegeByIdHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetPrivilegeByIdResponse> Handle(GetPrivilegeByIdRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Privilege = (await _context.Privileges.SingleOrDefaultAsync(x => x.PrivilegeId == request.PrivilegeId)).ToDto()
            };
        }
        
    }

}
