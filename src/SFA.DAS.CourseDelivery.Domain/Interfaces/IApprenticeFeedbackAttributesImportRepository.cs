using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IApprenticeFeedbackAttributesImportRepository
    {
        Task InsertMany(IEnumerable<ApprenticeFeedbackAttributesImport> apprenticeFeedbackAttributesImports);
        void DeleteAll();
        Task<IEnumerable<ApprenticeFeedbackAttributesImport>> GetAll();
    }
}
