using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRegistrationFeedbackRatingImportRepository
    {
        void DeleteAll();
        Task<IEnumerable<ProviderRegistrationFeedbackRatingImport>> GetAll();
        Task InsertMany(IEnumerable<ProviderRegistrationFeedbackRatingImport> items);
    }
}