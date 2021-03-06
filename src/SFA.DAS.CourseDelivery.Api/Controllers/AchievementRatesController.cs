using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using GetOverallAchievementRatesResponse = SFA.DAS.CourseDelivery.Api.ApiResponses.GetOverallAchievementRatesResponse;

namespace SFA.DAS.CourseDelivery.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class AchievementRatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AchievementRatesController> _logger;

        public AchievementRatesController (IMediator mediator, ILogger<AchievementRatesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("Overall")]
        public async Task<IActionResult> GetOverallAchievementRates([FromQuery] string sector)
        {
            
            try
            {
                var queryResult = await _mediator.Send(new Application.OverallNationalAchievementRates.Queries.GetOverallAchievementRatesQuery {SectorSubjectArea = sector});

                var response = new GetOverallAchievementRatesResponse
                {
                    OverallAchievementRates = queryResult.OverallAchievementRates.Select(c=>(GetOverallAchievementRateResponse)c).ToList()
                        
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to get overall achievement rates by:{sector}");
                return NotFound();
            }
        }
    }
}