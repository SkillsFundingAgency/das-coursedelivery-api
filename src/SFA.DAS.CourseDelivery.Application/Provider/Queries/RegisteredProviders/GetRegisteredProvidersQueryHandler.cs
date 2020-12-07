using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.RegisteredProviders
{
    public class GetRegisteredProvidersQueryHandler : IRequestHandler<GetRegisteredProvidersQuery, GetRegisteredProvidersResponse>
    {
        private readonly IProviderService _providerService;

        public GetRegisteredProvidersQueryHandler(IProviderService providerService)
        {
            _providerService = providerService;
        }

        public async Task<GetRegisteredProvidersResponse> Handle(GetRegisteredProvidersQuery request, CancellationToken cancellationToken)
        {
            var registeredProviders = await _providerService.GetRegisteredProviders();

            return new GetRegisteredProvidersResponse
            {
                RegisteredProviders = registeredProviders
            };
        }
    }
}