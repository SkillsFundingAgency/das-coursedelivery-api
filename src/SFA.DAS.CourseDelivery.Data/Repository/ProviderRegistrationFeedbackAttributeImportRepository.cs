using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderRegistrationFeedbackAttributeImportRepository : IProviderRegistrationFeedbackAttributeImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderRegistrationFeedbackAttributeImportRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeleteAll()
        {
            _dataContext.ProviderRegistrationFeedbackAttributeImports.RemoveRange(_dataContext.ProviderRegistrationFeedbackAttributeImports.ToList());
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<ProviderRegistrationFeedbackAttributeImport>> GetAll()
        {
            var results = await _dataContext.ProviderRegistrationFeedbackAttributeImports.ToListAsync();

            return results;
        }

        public async Task InsertMany(IEnumerable<ProviderRegistrationFeedbackAttributeImport> items)
        {
            await _dataContext.ProviderRegistrationFeedbackAttributeImports.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }
    }
}