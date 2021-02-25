using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Api.ApiRequests;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse;

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
        [Route("{id}/ukprns")]
        public async Task<IActionResult> GetProviderIdsByStandardAndLocation(int id, double lat, double lon)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetUkprnsQuery
                {
                    StandardId = id,
                    Lat = lat,
                    Lon = lon
                });
                var response = new GetProvidersByCourseAndLocationResponse
                {
                    UkprnsByStandardAndLocation = queryResult.UkprnsByStandardAndLocation,
                    UkprnsByStandard = queryResult.UkprnsByStandard
                };
                return Ok(response);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            
        }
        
        [HttpGet]
        [Route("{id}/providers")]
        public async Task<IActionResult> GetProvidersByStandardId(int id, [FromQuery]GetProvidersByStandardRequest request)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetCourseProvidersQuery
                {
                    StandardId = id,
                    Lat = request.Lat,
                    Lon = request.Lon,
                    SortOrder = (short)request.SortOrder,
                    SectorSubjectArea = request.SectorSubjectArea,
                    Level = (short)request.Level,
                    ShortlistUserId = request.ShortlistUserId
                });

                var getCourseProviderResponses = queryResult
                    .Providers
                    .Select(c=> GetProviderDetailResponse.Map(c, (short)request.Age))
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetProviderDetailResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(GetProviderDetailResponse))]
        public async Task<IActionResult> GetProviderByUkprn(int id, int ukprn, string sectorSubjectArea, double? lat = null, double? lon = null, Guid? shortlistUserId = null)
        {
            var queryResult = await _mediator.Send(new GetCourseProviderQuery
            {
                Ukprn = ukprn, 
                StandardId = id, 
                Lat = lat, 
                Lon = lon,
                SectorSubjectArea = sectorSubjectArea,
                ShortlistUserId = shortlistUserId
            });

            if (queryResult.ProviderStandardLocation == null)
            {
                return NotFound();
            }

            return Ok(GetProviderDetailResponse.Map(queryResult.ProviderStandardLocation));
        }
    }
}