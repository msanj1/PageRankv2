using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageRank.Core.Features.Search.SearchUrlPositions;

namespace PageRank.Api.Controllers
{
    [Route("api/v1/pagerank")]
    [ApiController]
    public class PageRankController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PageRankController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<int>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchUrlPositions([FromQuery] SearchUrlPositionsParameters resourceParameters,
            CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new SearchUrlPositionsQuery(resourceParameters.Url,
                resourceParameters.SearchEngineType, resourceParameters.TitleSearch), cancellationToken);

            return Ok(results.SearchPositions);
        }
    }
}