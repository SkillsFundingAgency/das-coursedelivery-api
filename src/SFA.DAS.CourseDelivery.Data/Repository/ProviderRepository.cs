using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<Provider> providers)
        {
            await _dataContext.Providers.AddRangeAsync(providers);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _dataContext.Providers.RemoveRange(_dataContext.Providers);
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<Provider>> GetByStandardId(int standardId)
        {
            var providers = await _dataContext
                .ProviderStandards
                .Where(c => c.StandardId.Equals(standardId))
                .Include(c => c.Provider)
                .Select(c=>c.Provider).OrderBy(c=>c.Name).ToListAsync();

            return providers;
        }

        public async Task<Provider> GetByUkprn(int ukPrn)
        {
            var provider = await _dataContext.Providers.SingleOrDefaultAsync(c => c.Ukprn.Equals(ukPrn));

            return provider;
        }
    }
}