using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderStandard
{
    public class GetProviderStandardQueryHandler : IRequestHandler<GetProviderStandardQuery, GetProviderStandardResponse>
    {
        private readonly IProviderService _service;

        public GetProviderStandardQueryHandler (IProviderService service)
        {
            _service = service;
        }
        public async Task<GetProviderStandardResponse> Handle(GetProviderStandardQuery request, CancellationToken cancellationToken)
        {
            var provider = await _service.GetProviderByUkprnAndStandard(request.Ukprn, request.StandardId);
            
            return new GetProviderStandardResponse
            {
                ProviderStandardContact = provider
            }; 
                
        }
    }
}