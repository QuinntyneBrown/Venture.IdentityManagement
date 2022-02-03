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

    public class GetUsersRequest: IRequest<GetUsersResponse> { }
    public class GetUsersResponse: ResponseBase
    {
        public List<UserDto> Users { get; set; }
    }
    public class GetUsersHandler: IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetUsersHandler> _logger;
    
        public GetUsersHandler(IIdentityManagementDbContext context, ILogger<GetUsersHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Users = await _context.Users.ToDtosAsync(cancellationToken)
            };
        }
        
    }

}
