using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IApprenticeFeedbackAttributesApiService
    {
        Task<IEnumerable<ApprenticeFeedbackAttribute>> GetApprenticeFeedbackAttributes();
    }
}
