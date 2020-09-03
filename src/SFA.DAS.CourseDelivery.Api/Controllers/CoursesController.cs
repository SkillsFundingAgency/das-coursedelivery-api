using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Api.ApiRequests;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse;
using GetCourseProviderResponse = SFA.DAS.CourseDelivery.Api.ApiResponses.GetCourseProviderResponse;
using GetProviderResponse = SFA.DAS.CourseDelivery.Api.ApiResponses.GetProviderResponse;

namespace SFA.DAS.CourseDelivery.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController (IMediator mediator, ILogger<CoursesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("{id}/providers")]
        public async Task<IActionResult> GetProvidersByStandardId(int id, [FromQuery]Age age = 0, [FromQuery]Level level = 0, [FromQuery]double? lat = null, [FromQuery]double? lon = null, [FromQuery]SortOrder sortOrder = SortOrder.Distance)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetCourseProvidersQuery
                {
                    StandardId = id,
                    Lat = lat,
                    Lon = lon,
                    SortOrder = (short)sortOrder
                });

                var getCourseProviderResponses = queryResult
                    .Providers
                    .Select(c=> new GetProviderResponse().Map(c, (short)age, (short)level))
                    .ToList();
                
                var response = new GetCourseProvidersListResponse
                {
                    Providers = getCourseProviderResponses,
                    TotalResults = queryResult.Providers.Count() 
                        
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to get providers by course id:{id}");
                return BadRequest();
            }
        }
        
        [HttpGet]
        [Route("{id}/providers/{ukprn}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetCourseProviderResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(GetCourseProviderResponse))]
        public async Task<IActionResult> GetProviderByUkprn(int id, int ukprn)
        {
            var queryResult = await _mediator.Send(new GetCourseProviderQuery {Ukprn = ukprn, StandardId = id});

            if (queryResult.ProviderStandardContact == null)
            {
                return NotFound();
            }

            return Ok((GetCourseProviderResponse) queryResult);
        }
    }
}