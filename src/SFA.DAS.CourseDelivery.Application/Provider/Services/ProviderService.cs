using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Application.Provider.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderStandardRepository _providerStandardRepository;
        private readonly INationalAchievementRateOverallRepository _nationalAchievementRateOverallRepository;

        public ProviderService (IProviderRepository providerRepository, 
            IProviderStandardRepository providerStandardRepository, 
            INationalAchievementRateOverallRepository nationalAchievementRateOverallRepository)
        {
            _providerRepository = providerRepository;
            _providerStandardRepository = providerStandardRepository;
            _nationalAchievementRateOverallRepository = nationalAchievementRateOverallRepository;
        }
        public async Task<IEnumerable<ProviderLocation>> GetProvidersByStandardId(int standardId)
        {
            var providers = (await _providerRepository.GetByStandardId(standardId)).ToList();

            var providerLocations = BuildProviderLocations(providers);
            
            return providerLocations;
        }

        public async Task<ProviderLocation> GetProviderByUkprnAndStandard(int ukPrn, int standardId, double? lat, double? lon)
        {
            if (lat ==null && lon == null)
            {
                var providerResult = await _providerRepository.GetByUkprnAndStandardId(ukPrn, standardId);

                return BuildProviderLocations(providerResult).FirstOrDefault();
            }
            
            var provider = await _providerRepository.GetProviderByStandardIdAndLocation(ukPrn, standardId, lat.Value, lon.Value);
            
            return BuildProviderLocations(provider).FirstOrDefault();
            
        }

        public async Task<IEnumerable<Domain.Entities.NationalAchievementRateOverall>> GetOverallAchievementRates(string description)
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
            double lon, short querySortOrder)
        {
            var providers = await _providerRepository.GetByStandardIdAndLocation(standardId, lat, lon, querySortOrder);

            var providerLocations = BuildProviderLocations(providers);
            
            return providerLocations;
        }

        private static IEnumerable<ProviderLocation> BuildProviderLocations(IEnumerable<ProviderWithStandardAndLocation> providers)
        {
            return providers
                .GroupBy(item => new
                {
                    UkPrn = item.Ukprn, 
                    item.Name, 
                    item.ContactUrl, 
                    item.Email, 
                    item.Phone, 
                    item.ProviderDistanceInMiles,
                    item.ProviderHeadOfficeAddress1,
                    item.ProviderHeadOfficeAddress2,
                    item.ProviderHeadOfficeAddress3,
                    item.ProviderHeadOfficeAddress4,
                    item.ProviderHeadOfficeTown,
                    item.ProviderHeadOfficePostcode
                })
                .Select(group => new ProviderLocation(
                    group.Key.UkPrn, 
                    group.Key.Name,
                    group.Key.ContactUrl, 
                    group.Key.Phone, 
                    group.Key.Email, 
                    group.Key.ProviderDistanceInMiles,
                    group.Key.ProviderHeadOfficeAddress1,
                    group.Key.ProviderHeadOfficeAddress2,
                    group.Key.ProviderHeadOfficeAddress3,
                    group.Key.ProviderHeadOfficeAddress4,
                    group.Key.ProviderHeadOfficeTown,
                    group.Key.ProviderHeadOfficePostcode,
                    group.ToList()))
                .ToList();
        }

        public async Task<Domain.Entities.Provider> GetProviderByUkprn(int ukprn)
        {
            var provider = await _providerRepository.GetByUkprn(ukprn);

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
