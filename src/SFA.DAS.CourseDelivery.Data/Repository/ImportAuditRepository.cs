using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ImportAuditRepository : IImportAuditRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ImportAuditRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Insert(ImportAudit importAudit)
        {
            await _dataContext.ImportAudit.AddAsync(importAudit);
            _dataContext.SaveChanges();
        }
    }
}