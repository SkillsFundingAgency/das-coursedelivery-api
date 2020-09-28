using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation
{
    public class GetUkprnsQueryHandler : IRequestHandler<GetUkprnsQuery, GetUkprnsQueryResult>
    {
        private readonly IValidator<GetUkprnsQuery> _validator;
        private readonly IProviderService _service;

        public GetUkprnsQueryHandler (IValidator<GetUkprnsQuery> validator, IProviderService service)
        {
            _validator = validator;
            _service = service;
        }
        public async Task<GetUkprnsQueryResult> Handle(GetUkprnsQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }

            var ukprns = await _service.GetUkprnsForStandardAndLocation(request.StandardId, request.Lat, request.Lon);
            
            return new GetUkprnsQueryResult
            {
                UkprnsByStandardAndLocation = ukprns.UkprnsFilteredByStandardAndLocation,
                UkprnsByStandard = ukprns.UkprnsFilteredByStandard
            };
        }
    }
}