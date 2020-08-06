using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.CoursesByProvider;

namespace SFA.DAS.CourseDelivery.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class ProvidersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProvidersController> _logger;

        public ProvidersController(IMediator mediator, ILogger<ProvidersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("{ukprn}/courses")]
        public async Task<IActionResult> GetStandardIdsByProvider(int ukprn)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetProviderCoursesQuery { Ukprn = ukprn });

                var response = new GetProviderCoursesListResponse
                {
                    StandardIds = queryResult.CourseIds,
                    TotalResults = queryResult.CourseIds.Count()
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to get courses by ukprn:{ukprn}");
                return BadRequest();
            }
            
        }
    }
}