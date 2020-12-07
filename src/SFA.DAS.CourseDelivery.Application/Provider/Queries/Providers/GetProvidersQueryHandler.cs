using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.Providers
{
    public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, GetProvidersResponse>
    {
        private readonly IProviderService _providerService;

        public GetProvidersQueryHandler(IProviderService providerService)
        {
            _providerService = providerService;
        }

        public async Task<GetProvidersResponse> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
        {
            var registeredProviders = await _providerService.GetRegisteredProviders();

            return new GetProvidersResponse
            {
                RegisteredProviders = registeredProviders
            };
        }
    }
}