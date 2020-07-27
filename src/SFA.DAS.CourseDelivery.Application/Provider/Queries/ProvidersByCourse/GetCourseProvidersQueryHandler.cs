using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse
{
    public class GetCourseProvidersQueryHandler :IRequestHandler<GetCourseProvidersQuery, GetProvidersResponse>
    {
        private readonly IProviderService _providerService;

        public GetCourseProvidersQueryHandler (IProviderService providerService)
        {
            _providerService = providerService;
        }
        public async Task<GetProvidersResponse> Handle(GetCourseProvidersQuery request, CancellationToken cancellationToken)
        {
            var providers = await _providerService.GetProvidersByStandardId(request.StandardId);
            
            return new GetProvidersResponse
            {
                Providers = providers
            };
        }
    }
}