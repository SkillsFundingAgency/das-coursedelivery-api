using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.Provider;
using GetProviderResponse = SFA.DAS.CourseDelivery.Api.ApiResponses.GetProviderResponse;

namespace SFA.DAS.CourseDelivery.Api.Controllers
{
    
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class ProvidersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProvidersController> _logger;

        public ProvidersController (IMediator mediator, ILogger<ProvidersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("{ukprn}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetProviderResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(GetProviderResponse))]
        public async Task<IActionResult> GetProviderByUkprn(int ukprn)
        {
            var queryResult = await _mediator.Send(new GetProviderQuery {Ukprn = ukprn});

            if (queryResult.Provider == null)
            {
                return NotFound();
            }

            return Ok((GetProviderResponse) queryResult.Provider);
        }
    }
}