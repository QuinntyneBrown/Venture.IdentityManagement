using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Venture.IdentityManagement.Core;
using MediatR;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Venture.IdentityManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class UserController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation(
            Summary = "Get User by id.",
            Description = @"Get User by id."
        )]
        [HttpGet("{userId:guid}", Name = "getUserById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetUserByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetUserByIdResponse>> GetById([FromRoute]Guid userId, CancellationToken cancellationToken)
        {
            var request = new GetUserByIdRequest() { UserId = userId };
        
            var response = await _mediator.Send(request, cancellationToken);
        
            if (response.User == null)
            {
                return new NotFoundObjectResult(request.UserId);
            }
        
            return response;
        }
        
        [SwaggerOperation(
            Summary = "Get Users.",
            Description = @"Get Users."
        )]
        [HttpGet(Name = "getUsers")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetUsersResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetUsersResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetUsersRequest(), cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Create User.",
            Description = @"Create User."
        )]
        [HttpPost(Name = "createUser")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateUserResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody]CreateUserRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(CreateUserRequest),
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Get User Page.",
            Description = @"Get User Page."
        )]
        [HttpGet("page/{pageSize}/{index}", Name = "getUsersPage")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetUsersPageResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetUsersPageResponse>> Page([FromRoute]int pageSize, [FromRoute]int index, CancellationToken cancellationToken)
        {
            var request = new GetUsersPageRequest { Index = index, PageSize = pageSize };
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Update User.",
            Description = @"Update User."
        )]
        [HttpPut(Name = "updateUser")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateUserResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateUserResponse>> Update([FromBody]UpdateUserRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(UpdateUserRequest),
                nameof(request.User.UserId),
                request.User.UserId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Delete User.",
            Description = @"Delete User."
        )]
        [HttpDelete("{userId:guid}", Name = "removeUser")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveUserResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveUserResponse>> Remove([FromRoute]Guid userId, CancellationToken cancellationToken)
        {
            var request = new RemoveUserRequest() { UserId = userId };
        
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(RemoveUserRequest),
                nameof(request.UserId),
                request.UserId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
    }
}
