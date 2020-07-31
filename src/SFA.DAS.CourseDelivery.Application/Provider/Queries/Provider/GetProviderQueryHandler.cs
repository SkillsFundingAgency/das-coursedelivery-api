using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.Provider
{
    public class GetProviderQueryHandler : IRequestHandler<GetProviderQuery, GetProviderResponse>
    {
        private readonly IProviderService _service;

        public GetProviderQueryHandler (IProviderService service)
        {
            _service = service;
        }
        public async Task<GetProviderResponse> Handle(GetProviderQuery request, CancellationToken cancellationToken)
        {
            var provider = await _service.GetProviderByUkprn(request.Ukprn);
            
            return new GetProviderResponse
            {
                Provider = provider
            }; 
                
        }
    }
}