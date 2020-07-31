using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
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
        [Route("{id}/providers")]
        public async Task<IActionResult> GetProvidersByStandardId(int id)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetCourseProvidersQuery {StandardId = id});

                var getCourseProviderResponses = queryResult
                    .Providers
                    .Select(c=>(GetProviderResponse)c)
                    .ToList();
                
                var response = new GetCourseProvidersListResponse
                {
                    Providers = getCourseProviderResponses,
                    TotalResults = getCourseProviderResponses.Count 
                        
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unable to get providers by course id:{id}");
                return BadRequest();
            }
        }
    }
}