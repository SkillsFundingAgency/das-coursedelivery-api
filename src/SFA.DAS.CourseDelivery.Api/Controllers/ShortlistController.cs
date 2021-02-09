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
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.CreateShortlistItemForUser;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUser;

namespace SFA.DAS.CourseDelivery.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class ShortlistController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ShortlistController> _logger;

        public ShortlistController(
            IMediator mediator, 
            ILogger<ShortlistController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<IActionResult> GetShortlistForUser(Guid userId)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetShortlistForUserQuery
                {
                    UserId = userId
                });
                var response = new GetAllShortlistItemsForUserResponse
                {
                    ShortlistItems = queryResult.Shortlist.Select(shortlist => (GetShortlistResponse)shortlist)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateShortlistItemForUser(CreateShortlistRequest request)
        {
            try
            {
                await _mediator.Send(new CreateShortlistItemForUserRequest
                {
                    Lat = request.Lat,
                    Level = request.Level,
                    Lon = request.Lon,
                    CourseId = request.CourseId,
                    LocationDescription = request.LocationDescription,
                    ProviderUkprn = request.ProviderUkprn,
                    SectorSubjectArea = request.SectorSubjectArea,
                    ShortlistUserId = request.ShortlistUserId
                });
                
                return Created($"/api/{ControllerContext.ActionDescriptor.ControllerName}/{request.ShortlistUserId}", null);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationResult.ErrorMessage);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}