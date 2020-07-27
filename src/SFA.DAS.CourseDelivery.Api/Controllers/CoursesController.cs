using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse;

namespace SFA.DAS.CourseDelivery.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController (IMediator mediator)
        {
            _mediator = mediator;
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
                    .Select(c=>(GetCourseProviderResponse)c)
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
                return BadRequest();
            }
        }
    }
}