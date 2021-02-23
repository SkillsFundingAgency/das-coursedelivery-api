using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Extensions;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Application.Provider.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderStandardRepository _providerStandardRepository;
        private readonly INationalAchievementRateOverallRepository _nationalAchievementRateOverallRepository;
        private readonly IProviderRegistrationRepository _providerRegistrationRepository;

        public ProviderService (IProviderRepository providerRepository, 
            IProviderStandardRepository providerStandardRepository, 
            INationalAchievementRateOverallRepository nationalAchievementRateOverallRepository,
            IProviderRegistrationRepository providerRegistrationRepository)
        {
            _providerRepository = providerRepository;
            _providerStandardRepository = providerStandardRepository;
            _nationalAchievementRateOverallRepository = nationalAchievementRateOverallRepository;
            _providerRegistrationRepository = providerRegistrationRepository;
        }

        public async Task<IEnumerable<ProviderLocation>> GetProvidersByStandardId(  int standardId,
            string sectorSubjectArea, short level, Guid shortlistUserId)
        {
            var providers = (await _providerRepository.GetByStandardId(standardId, sectorSubjectArea, level, shortlistUserId)).ToList();

            
            return providers.BuildProviderLocations();
        }

        public async Task<ProviderLocation> GetProviderByUkprnAndStandard(int ukPrn, int standardId, double? lat, double? lon, string sectorSubjectArea, Guid shortlistUserId)
        {
            if (lat == null || lon == null)
            {
                var providerResult = await _providerRepository.GetByUkprnAndStandardId(ukPrn, standardId, sectorSubjectArea, shortlistUserId);

                return providerResult.BuildProviderLocations().FirstOrDefault();
            }
            
            var provider = await _providerRepository.GetProviderByStandardIdAndLocation(ukPrn, standardId, shortlistUserId, lat.Value, lon.Value, sectorSubjectArea);
            
            return provider.BuildProviderLocations().FirstOrDefault();
            
        }

        public async Task<IEnumerable<ProviderSummary>> GetRegisteredProviders()
        {
            var providersFromRepo = await _providerRegistrationRepository.GetAllRegisteredApprovedAndActiveProviders();
            return providersFromRepo.Select(provider => (ProviderSummary)provider);
        }

        public async Task<IEnumerable<NationalAchievementRateOverall>> GetOverallAchievementRates(string description)
        {
            var items = await _nationalAchievementRateOverallRepository.GetBySectorSubjectArea(description);

            return items;
        }
        public async Task<IEnumerable<int>> GetStandardIdsByUkprn(int ukprn)
        {
            var standardIds = await _providerStandardRepository.GetCoursesByUkprn(ukprn);

            return standardIds;
        }

        public async Task<IEnumerable<ProviderLocation>> GetProvidersByStandardAndLocation(  int standardId, double lat,
            double lon, short querySortOrder, string sectorSubjectArea, short level, Guid shortlistUserId)
        {
            var providers = await _providerRepository.GetByStandardIdAndLocation(standardId, lat, lon, querySortOrder, sectorSubjectArea, level, shortlistUserId);

            return providers.BuildProviderLocations();
        }

        public async Task<ProviderSummary> GetProviderByUkprn(int ukprn)
        {
            var provider = await _providerRepository.GetByUkprn(ukprn);

            if (provider == null)
            {
                return await  _providerRegistrationRepository.GetRegisteredApprovedAndActiveProviderByUkprn(ukprn);
            }

            return provider;
        }

        public async Task<UkprnsForStandard> GetUkprnsForStandardAndLocation(int standardId, double lat, double lon)
        {
            var ukprnsForStandardAndLocation = _providerRepository.GetUkprnsForStandardAndLocation(standardId, lat, lon);
            var ukrpnsForStandard = _providerStandardRepository.GetUkprnsByStandard(standardId);
            
            await Task.WhenAll(ukrpnsForStandard, ukprnsForStandardAndLocation);
            
            return new UkprnsForStandard
            {
                UkprnsFilteredByStandardAndLocation = ukprnsForStandardAndLocation.Result,
                UkprnsFilteredByStandard = ukrpnsForStandard.Result
            };
        }

    }
}
