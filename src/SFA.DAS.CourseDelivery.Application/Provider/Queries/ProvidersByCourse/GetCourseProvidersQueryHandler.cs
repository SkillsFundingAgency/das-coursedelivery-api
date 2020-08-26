using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse
{
    public class GetCourseProvidersQueryHandler :IRequestHandler<GetCourseProvidersQuery, GetCourseProvidersResponse>
    {
        private readonly IProviderService _providerService;

        public GetCourseProvidersQueryHandler (IProviderService providerService)
        {
            _providerService = providerService;
        }
        public async Task<GetCourseProvidersResponse> Handle(GetCourseProvidersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Models.ProviderLocation> providers;

            if (request.Lat != null && request.Lon != null)
            {
                providers = await _providerService.GetProvidersByStandardAndLocation(request.StandardId, request.Lat.Value, request.Lon.Value);
            }
            else
            {
                providers = await _providerService.GetProvidersByStandardId(request.StandardId);    
            }
            
            
            return new GetCourseProvidersResponse
            {
                Providers = providers
            };
        }
    }
}