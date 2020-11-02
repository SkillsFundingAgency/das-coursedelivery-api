using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse
{
    public class GetCourseProviderQueryHandler : IRequestHandler<GetCourseProviderQuery, GetCourseProviderQueryResponse>
    {
        private readonly IProviderService _service;

        public GetCourseProviderQueryHandler (IProviderService service)
        {
            _service = service;
        }
        public async Task<GetCourseProviderQueryResponse> Handle(GetCourseProviderQuery request, CancellationToken cancellationToken)
        {
            var provider = await _service.GetProviderByUkprnAndStandard(request.Ukprn, request.StandardId, request.Lat, request.Lon, request.SectorSubjectArea);
            
            return new GetCourseProviderQueryResponse
            {
                ProviderStandardLocation = provider
            }; 
                
        }
    }
}