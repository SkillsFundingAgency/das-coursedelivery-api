using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderStandardRepository : IProviderStandardRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderStandardRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeleteAll()
        {
            _dataContext.ProviderStandards.RemoveRange(_dataContext.ProviderStandards.ToList());
            _dataContext.SaveChanges();
        }

        public async Task InsertFromImportTable()
        {
            await _dataContext.ExecuteRawSql("INSERT INTO dbo.ProviderStandard SELECT * FROM dbo.ProviderStandard_Import");
        }
        
        public async Task<ProviderStandard> GetByUkprnAndStandard(int ukPrn, int standardId)
        {
            var providerStandard = await _dataContext
                .ProviderStandards
                .Include(c => c.Provider)
                .ThenInclude(c=>c.ProviderRegistrationFeedbackAttributes)
                .Include(c => c.Provider)
                .ThenInclude(c=>c.ProviderRegistrationFeedbackRating)
                .Include(c=>c.NationalAchievementRate)
                .SingleOrDefaultAsync(c => c.StandardId.Equals(standardId) && c.Ukprn.Equals(ukPrn));

            return providerStandard;
        }

        public async Task<IEnumerable<int>> GetCoursesByUkprn(int ukPrn)
        {
            var courses = await _dataContext
                .ProviderStandards
                .Where(c => c.Ukprn == ukPrn)
                .Select(c => c.StandardId).ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<int>> GetUkprnsByStandard(int standardId)
        {
            var providers = await _dataContext.ProviderStandards
                .Where(c => c.StandardId.Equals(standardId))
                .Select(c => c.Ukprn).Distinct().ToListAsync();

            return providers;
        }
    }
}