using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse
{
    public class GetCourseProviderQueryHandler : IRequestHandler<GetCourseProviderQuery, GetCourseProviderResponse>
    {
        private readonly IProviderService _service;

        public GetCourseProviderQueryHandler (IProviderService service)
        {
            _service = service;
        }
        public async Task<GetCourseProviderResponse> Handle(GetCourseProviderQuery request, CancellationToken cancellationToken)
        {
            var provider = await _service.GetProviderByUkprnAndStandard(request.Ukprn, request.StandardId);
            
            return new GetCourseProviderResponse
            {
                ProviderStandardContact = provider
            }; 
                
        }
    }
}