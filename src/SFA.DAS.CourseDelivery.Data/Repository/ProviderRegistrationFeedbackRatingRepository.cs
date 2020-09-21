using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderRegistrationFeedbackRatingRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderRegistrationFeedbackRatingRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeleteAll()
        {
            _dataContext.ProviderRegistrationFeedbackRatings.RemoveRange(_dataContext.ProviderRegistrationFeedbackRatings.ToList());
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<ProviderRegistrationFeedbackRating> items)
        {
            await _dataContext.ProviderRegistrationFeedbackRatings.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }
    }
}