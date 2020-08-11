using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Entities;
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
            var provider = await _service.GetProviderByUkprnAndStandard(request.Ukprn, request.StandardId);
            var achievementRates = provider.NationalAchievementRate?.FirstOrDefault() == null || provider.NationalAchievementRate.Count == 0 ?
                new List<NationalAchievementRateOverall>() :
                await _service.GetOverallAchievementRates(provider.NationalAchievementRate.FirstOrDefault()
                    ?.SectorSubjectArea);
            
            return new GetProviderResponse
            {
                ProviderStandardContact = provider,
                OverallAchievementRates = achievementRates
            }; 
                
        }
    }
}