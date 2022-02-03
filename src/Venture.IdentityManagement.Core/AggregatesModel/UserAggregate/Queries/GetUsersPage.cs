using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Venture.IdentityManagement.Core.Interfaces;
using Venture.IdentityManagement.SharedKernal;

namespace Venture.IdentityManagement.Core
{

    public class GetUsersPageRequest: IRequest<GetUsersPageResponse>
    {
        public int PageSize { get; set; }
        public int Index { get; set; }
    }
    public class GetUsersPageResponse: ResponseBase
    {
        public int Length { get; set; }
        public List<UserDto> Entities { get; set; }
    }
    public class GetUsersPageHandler: IRequestHandler<GetUsersPageRequest, GetUsersPageResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetUsersPageHandler> _logger;
    
        public GetUsersPageHandler(IIdentityManagementDbContext context, ILogger<GetUsersPageHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetUsersPageResponse> Handle(GetUsersPageRequest request, CancellationToken cancellationToken)
        {
            var query = from user in _context.Users
                select user;
            
            var length = await _context.Users.CountAsync();
            
            var users = await query.Page(request.Index, request.PageSize)
                .Select(x => x.ToDto()).ToListAsync();
            
            return new ()
            {
                Length = length,
                Entities = users
            };
        }
        
    }

}
