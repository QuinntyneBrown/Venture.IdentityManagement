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

    public class GetUserByIdRequest: IRequest<GetUserByIdResponse>
    {
        public Guid UserId { get; set; }
    }
    public class GetUserByIdResponse: ResponseBase
    {
        public UserDto User { get; set; }
    }
    public class GetUserByIdHandler: IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
    {
        private readonly IIdentityManagementDbContext _context;
        private readonly ILogger<GetUserByIdHandler> _logger;
    
        public GetUserByIdHandler(IIdentityManagementDbContext context, ILogger<GetUserByIdHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            return new () {
                User = (await _context.Users.SingleOrDefaultAsync(x => x.UserId == request.UserId)).ToDto()
            };
        }
        
    }

}
