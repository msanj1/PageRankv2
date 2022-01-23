using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PageRank.Core.Features.Search.SearchUrlPositions;

namespace PageRank.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PageRankController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PageRankController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> SearchUrlPositions([FromQuery] SearchUrlPositionsParameters resourceParameters,
            CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new SearchUrlPositionsQuery(resourceParameters.Url,
                resourceParameters.SearchEngineType, resourceParameters.TitleSearch), cancellationToken);

            return Ok(results.SearchPositions);
        }
    }
}