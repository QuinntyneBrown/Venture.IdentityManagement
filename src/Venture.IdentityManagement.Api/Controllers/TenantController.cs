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
    public class TenantController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TenantController> _logger;

        public TenantController(IMediator mediator, ILogger<TenantController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation(
            Summary = "Get Tenant by id.",
            Description = @"Get Tenant by id."
        )]
        [HttpGet("{tenantId:guid}", Name = "getTenantById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTenantByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTenantByIdResponse>> GetById([FromRoute]Guid tenantId, CancellationToken cancellationToken)
        {
            var request = new GetTenantByIdRequest() { TenantId = tenantId };
        
            var response = await _mediator.Send(request, cancellationToken);
        
            if (response.Tenant == null)
            {
                return new NotFoundObjectResult(request.TenantId);
            }
        
            return response;
        }
        
        [SwaggerOperation(
            Summary = "Get Tenants.",
            Description = @"Get Tenants."
        )]
        [HttpGet(Name = "getTenants")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTenantsResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTenantsResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTenantsRequest(), cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Create Tenant.",
            Description = @"Create Tenant."
        )]
        [HttpPost(Name = "createTenant")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateTenantResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateTenantResponse>> Create([FromBody]CreateTenantRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(CreateTenantRequest),
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Get Tenant Page.",
            Description = @"Get Tenant Page."
        )]
        [HttpGet("page/{pageSize}/{index}", Name = "getTenantsPage")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTenantsPageResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTenantsPageResponse>> Page([FromRoute]int pageSize, [FromRoute]int index, CancellationToken cancellationToken)
        {
            var request = new GetTenantsPageRequest { Index = index, PageSize = pageSize };
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Update Tenant.",
            Description = @"Update Tenant."
        )]
        [HttpPut(Name = "updateTenant")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateTenantResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateTenantResponse>> Update([FromBody]UpdateTenantRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(UpdateTenantRequest),
                nameof(request.Tenant.TenantId),
                request.Tenant.TenantId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Delete Tenant.",
            Description = @"Delete Tenant."
        )]
        [HttpDelete("{tenantId:guid}", Name = "removeTenant")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveTenantResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveTenantResponse>> Remove([FromRoute]Guid tenantId, CancellationToken cancellationToken)
        {
            var request = new RemoveTenantRequest() { TenantId = tenantId };
        
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(RemoveTenantRequest),
                nameof(request.TenantId),
                request.TenantId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
    }
}
