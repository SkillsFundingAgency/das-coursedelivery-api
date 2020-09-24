using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRegistrationFeedbackRatingRepository
    {
        void DeleteAll();
        Task InsertMany(IEnumerable<ProviderRegistrationFeedbackRating> items);
    }
}