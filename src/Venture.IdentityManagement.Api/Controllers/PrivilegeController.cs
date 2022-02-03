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
    public class PrivilegeController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PrivilegeController> _logger;

        public PrivilegeController(IMediator mediator, ILogger<PrivilegeController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation(
            Summary = "Get Privilege by id.",
            Description = @"Get Privilege by id."
        )]
        [HttpGet("{privilegeId:guid}", Name = "getPrivilegeById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPrivilegeByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPrivilegeByIdResponse>> GetById([FromRoute]Guid privilegeId, CancellationToken cancellationToken)
        {
            var request = new GetPrivilegeByIdRequest() { PrivilegeId = privilegeId };
        
            var response = await _mediator.Send(request, cancellationToken);
        
            if (response.Privilege == null)
            {
                return new NotFoundObjectResult(request.PrivilegeId);
            }
        
            return response;
        }
        
        [SwaggerOperation(
            Summary = "Get Privileges.",
            Description = @"Get Privileges."
        )]
        [HttpGet(Name = "getPrivileges")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPrivilegesResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPrivilegesResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetPrivilegesRequest(), cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Create Privilege.",
            Description = @"Create Privilege."
        )]
        [HttpPost(Name = "createPrivilege")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePrivilegeResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePrivilegeResponse>> Create([FromBody]CreatePrivilegeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(CreatePrivilegeRequest),
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Get Privilege Page.",
            Description = @"Get Privilege Page."
        )]
        [HttpGet("page/{pageSize}/{index}", Name = "getPrivilegesPage")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPrivilegesPageResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPrivilegesPageResponse>> Page([FromRoute]int pageSize, [FromRoute]int index, CancellationToken cancellationToken)
        {
            var request = new GetPrivilegesPageRequest { Index = index, PageSize = pageSize };
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Update Privilege.",
            Description = @"Update Privilege."
        )]
        [HttpPut(Name = "updatePrivilege")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdatePrivilegeResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdatePrivilegeResponse>> Update([FromBody]UpdatePrivilegeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(UpdatePrivilegeRequest),
                nameof(request.Privilege.PrivilegeId),
                request.Privilege.PrivilegeId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Delete Privilege.",
            Description = @"Delete Privilege."
        )]
        [HttpDelete("{privilegeId:guid}", Name = "removePrivilege")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemovePrivilegeResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemovePrivilegeResponse>> Remove([FromRoute]Guid privilegeId, CancellationToken cancellationToken)
        {
            var request = new RemovePrivilegeRequest() { PrivilegeId = privilegeId };
        
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(RemovePrivilegeRequest),
                nameof(request.PrivilegeId),
                request.PrivilegeId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
    }
}
