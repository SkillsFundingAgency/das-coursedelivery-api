using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.Provider
{
    public class GetProviderQueryHandler : IRequestHandler<GetProviderQuery, GetProviderResponse>
    {
        private readonly IProviderService _service;

        public GetProviderQueryHandler(IProviderService service)
        {
            _service = service;
        }

        public async Task<GetProviderResponse> Handle(GetProviderQuery query, CancellationToken cancellationToken)
        {
            var provider = await _service.GetProviderByUkprn(query.Ukprn);

            return new GetProviderResponse
            {
                Provider = provider
            };
        }
    }
}