using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderStandardRepository : IProviderStandardRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;
        private readonly ICourseDeliveryReadonlyDataContext _readonlyDataContext;

        public ProviderStandardRepository(ICourseDeliveryDataContext dataContext, ICourseDeliveryReadonlyDataContext readonlyDataContext)
        {
            _dataContext = dataContext;
            _readonlyDataContext = readonlyDataContext;
        }

        public void DeleteAll()
        {
            _dataContext.ProviderStandards.RemoveRange(_dataContext.ProviderStandards.ToList());
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<ProviderStandard> providerStandards)
        {
            await _dataContext.ProviderStandards.AddRangeAsync(providerStandards);
            _dataContext.SaveChanges();
        }
        
        public async Task<ProviderStandard> GetByUkprnAndStandard(int ukPrn, int standardId)
        {
            var providerStandard = await _readonlyDataContext
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
            var courses = await _readonlyDataContext
                .ProviderStandards
                .Include(c=>c.ProviderStandardLocation)
                .Where(c => c.Ukprn == ukPrn)
                .Where(c=>c.ProviderStandardLocation!=null && c.ProviderStandardLocation.Any())
                .Select(c => c.StandardId).ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<int>> GetUkprnsByStandard(int standardId)
        {
            var providers = await _readonlyDataContext
                .ProviderStandards
                .Include(c=>c.ProviderStandardLocation)
                .ThenInclude(c=>c.Location)
                .Where(c => c.StandardId.Equals(standardId))
                .Where(c=>c.ProviderStandardLocation!=null && c.ProviderStandardLocation.Any(x=>x.Location!=null))
                .Where(c=>c.Provider.ProviderRegistration.ProviderTypeId == RoatpTypeConstants.ProviderTypeOfMainProvider 
                          && c.Provider.ProviderRegistration.StatusId == RoatpTypeConstants.StatusOfActive)
                .Select(c => c.Ukprn).Distinct().ToListAsync();

            return providers;
        }
    }
}