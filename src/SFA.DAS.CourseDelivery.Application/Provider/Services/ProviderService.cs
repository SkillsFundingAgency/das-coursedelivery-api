using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderStandardRepository _providerStandardRepository;

        public ProviderService (IProviderRepository providerRepository, IProviderStandardRepository providerStandardRepository)
        {
            _providerRepository = providerRepository;
            _providerStandardRepository = providerStandardRepository;
        }
        public async Task<IEnumerable<Domain.Entities.Provider>> GetProvidersByStandardId(int standardId)
        {
            var providers = await _providerRepository.GetByStandardId(standardId);

            return providers;
        }

        public async Task<Domain.Entities.ProviderStandard> GetProviderByUkprnAndStandard(int ukPrn, int standardId)
        {
            var provider = await _providerStandardRepository.GetByUkprnAndStandard(ukPrn, standardId);

            return provider;
        }

        public async Task<IEnumerable<int>> GetStandardIdsByUkprn(int ukprn)
        {
            var standardIds = await _providerStandardRepository.GetCoursesByUkprn(ukprn);

            return standardIds;
        }
    }
}