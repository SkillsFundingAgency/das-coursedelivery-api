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
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.DeleteShortlistItemForUser;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUser;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUserCount;

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
                var response = new GetShortlistForUserResponse
                {
                    Shortlist = queryResult.Shortlist.Select(shortlist => (GetShortlistItemResponse)shortlist)
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
                var id =await _mediator.Send(new CreateShortlistItemForUserRequest
                {
                    Lat = request.Lat,
                    Lon = request.Lon,
                    StandardId = request.StandardId,
                    LocationDescription = request.LocationDescription,
                    Ukprn = request.Ukprn,
                    SectorSubjectArea = request.SectorSubjectArea,
                    ShortlistUserId = request.ShortlistUserId
                });
                
                return Created($"/api/{ControllerContext.ActionDescriptor.ControllerName}/{request.ShortlistUserId}/items/{id}", new {Id=id});
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationResult.ErrorMessage);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("users/{userId}/items/{id}")]
        public async Task<IActionResult> DeleteShortlistItemForUser(Guid userId, Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteShortlistItemForUserRequest
                {
                    Id = id,
                    ShortlistUserId = userId
                });

                return Accepted();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("users/expired")]
        public async Task<IActionResult> GetExpiredShortlistUserIds([FromQuery] uint expiryInDays)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetExpiredShortlistUsersQuery
                {
                    ExpiryInDays = expiryInDays
                });

                return Ok(queryResult.UserIds.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
            return Ok();
        }
        [HttpGet]
        [Route("count/users/{userId}")]
        public async Task<IActionResult> GetShortlistForUserCount(Guid userId)
        {
            try
            {
                var queryResult = await _mediator.Send(new GetShortlistForUserCountQuery
                {
                    UserId = userId
                });
                
                var response =  new GetShortlistForUserCountResponse
                {
                    Count = queryResult
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