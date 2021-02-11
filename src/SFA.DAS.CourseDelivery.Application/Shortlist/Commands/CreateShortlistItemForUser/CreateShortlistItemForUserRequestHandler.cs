using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Commands.CreateShortlistItemForUser
{
    public class CreateShortlistItemForUserRequestHandler : IRequestHandler<CreateShortlistItemForUserRequest, Unit>
    {
        private readonly IValidator<CreateShortlistItemForUserRequest> _validator;
        private readonly IShortlistService _service;

        public CreateShortlistItemForUserRequestHandler (IValidator<CreateShortlistItemForUserRequest> validator, IShortlistService service)
        {
            _validator = validator;
            _service = service;
        }
        public async Task<Unit> Handle(CreateShortlistItemForUserRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }

            await _service.CreateShortlistItem(new Domain.Entities.Shortlist
            {
                Id = Guid.NewGuid(),
                Lat = request.Lat,
                Long = request.Lon,
                StandardId = request.StandardId,
                CourseSector = request.SectorSubjectArea,
                LocationDescription = request.LocationDescription,
                Ukprn = request.Ukprn,
                ShortlistUserId = request.ShortlistUserId
            });
            
            return Unit.Value;
        }
    }
}