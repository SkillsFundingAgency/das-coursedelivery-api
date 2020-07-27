using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IImportAuditRepository
    {
        Task Insert(ImportAudit importAudit);
    }
}