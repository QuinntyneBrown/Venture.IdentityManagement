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
    public class RoleController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IMediator mediator, ILogger<RoleController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation(
            Summary = "Get Role by id.",
            Description = @"Get Role by id."
        )]
        [HttpGet("{roleId:guid}", Name = "getRoleById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetRoleByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetRoleByIdResponse>> GetById([FromRoute]Guid roleId, CancellationToken cancellationToken)
        {
            var request = new GetRoleByIdRequest() { RoleId = roleId };
        
            var response = await _mediator.Send(request, cancellationToken);
        
            if (response.Role == null)
            {
                return new NotFoundObjectResult(request.RoleId);
            }
        
            return response;
        }
        
        [SwaggerOperation(
            Summary = "Get Roles.",
            Description = @"Get Roles."
        )]
        [HttpGet(Name = "getRoles")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetRolesResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetRolesResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetRolesRequest(), cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Create Role.",
            Description = @"Create Role."
        )]
        [HttpPost(Name = "createRole")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateRoleResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateRoleResponse>> Create([FromBody]CreateRoleRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(CreateRoleRequest),
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Get Role Page.",
            Description = @"Get Role Page."
        )]
        [HttpGet("page/{pageSize}/{index}", Name = "getRolesPage")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetRolesPageResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetRolesPageResponse>> Page([FromRoute]int pageSize, [FromRoute]int index, CancellationToken cancellationToken)
        {
            var request = new GetRolesPageRequest { Index = index, PageSize = pageSize };
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Update Role.",
            Description = @"Update Role."
        )]
        [HttpPut(Name = "updateRole")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateRoleResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateRoleResponse>> Update([FromBody]UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(UpdateRoleRequest),
                nameof(request.Role.RoleId),
                request.Role.RoleId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Delete Role.",
            Description = @"Delete Role."
        )]
        [HttpDelete("{roleId:guid}", Name = "removeRole")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveRoleResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveRoleResponse>> Remove([FromRoute]Guid roleId, CancellationToken cancellationToken)
        {
            var request = new RemoveRoleRequest() { RoleId = roleId };
        
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(RemoveRoleRequest),
                nameof(request.RoleId),
                request.RoleId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
    }
}
