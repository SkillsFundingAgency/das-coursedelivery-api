using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRegistrationFeedbackAttributeImportRepository
    {
        void DeleteAll();
        Task<IEnumerable<ProviderRegistrationFeedbackAttributeImport>> GetAll();
        Task InsertMany(IEnumerable<ProviderRegistrationFeedbackAttributeImport> items);
    }
}