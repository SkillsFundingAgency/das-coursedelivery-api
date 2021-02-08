using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository.Import
{
    public class ProviderRegistrationFeedbackRatingImportRepository : IProviderRegistrationFeedbackRatingImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderRegistrationFeedbackRatingImportRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeleteAll()
        {
            _dataContext.ProviderRegistrationFeedbackRatingImports.RemoveRange(_dataContext.ProviderRegistrationFeedbackRatingImports.ToList());
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<ProviderRegistrationFeedbackRatingImport>> GetAll()
        {
            var results = await _dataContext.ProviderRegistrationFeedbackRatingImports.ToListAsync();
            return results;
        }

        public async Task InsertMany(IEnumerable<ProviderRegistrationFeedbackRatingImport> items)
        {
            await _dataContext.ProviderRegistrationFeedbackRatingImports.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }
    }
}