using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;

        public ProviderService (IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }
        public async Task<IEnumerable<Domain.Entities.Provider>> GetProvidersByStandardId(int standardId)
        {
            var providers = await _providerRepository.GetByStandardId(standardId);

            return providers;
        }
    }
}