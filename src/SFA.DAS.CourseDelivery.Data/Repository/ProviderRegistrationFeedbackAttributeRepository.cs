using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderRegistrationFeedbackAttributeRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderRegistrationFeedbackAttributeRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<ProviderRegistrationFeedbackAttribute> items)
        {
            await _dataContext.ProviderRegistrationFeedbackAttributes.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _dataContext.ProviderRegistrationFeedbackAttributes.RemoveRange(_dataContext.ProviderRegistrationFeedbackAttributes.ToList());
            _dataContext.SaveChanges();
        }
    }
}