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

        public async Task InsertFromImportTable()
        {
            await _dataContext.ExecuteRawSql(@"INSERT INTO dbo.Provider SELECT * FROM dbo.Provider_Import");
        }

        public void DeleteAll()
        {
            _dataContext.Providers.RemoveRange(_dataContext.Providers.ToList());
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<Provider>> GetByStandardId(int standardId)
        {
            _dataContext.TrackChanges(false);
            
            var providers = await _dataContext
                .ProviderStandards
                .Include(c => c.Provider)
                .ThenInclude(c=>c.NationalAchievementRates)
                .Where(c => c.StandardId.Equals(standardId))
                .Select(c=>c.Provider)
                .OrderBy(c=>c.Name).ToListAsync();
            
            _dataContext.TrackChanges();
            return providers;
        }

        public async Task<Provider> GetByUkprn(int ukPrn)
        {
            var provider = await _dataContext.Providers.SingleOrDefaultAsync(c => c.Ukprn.Equals(ukPrn));

            return provider;
        }

    }
}