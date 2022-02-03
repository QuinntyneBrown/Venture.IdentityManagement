using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Venture.IdentityManagement.Core.Interfaces;
using Venture.IdentityManagement.SharedKernal;

namespace Venture.IdentityManagement.Core
{

    public class GetPrivilegesRequest: IRequest<GetPrivilegesResponse> { }
    public class GetPrivilegesResponse: ResponseBase
    {
        public List<PrivilegeDto> Privileges { get; set; }
    }
    public class GetPrivilegesHandler: IRequestHandler<GetPrivilegesRequest, GetPrivilegesResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetPrivilegesHandler> _logger;
    
        public GetPrivilegesHandler(IIdentityManagementDbContext context, ILogger<GetPrivilegesHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetPrivilegesResponse> Handle(GetPrivilegesRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Privileges = await _context.Privileges.ToDtosAsync(cancellationToken)
            };
        }
        
    }

}
