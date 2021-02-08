using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUser;

namespace SFA.DAS.CourseDelivery.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class ShortlistController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CoursesController> _logger;

        public ShortlistController(
            IMediator mediator, 
            ILogger<CoursesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<IActionResult> GetAllShortlistItemsForUser(Guid userId)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetShortlistForUserQuery
                {
                    UserId = userId
                });
                var response = new GetAllShortlistItemsForUserResponse
                {
                    ShortlistItems = queryResult.ShortlistItems.Select(shortlist => (GetShortlistResponse)shortlist)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }
    }
}