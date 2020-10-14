using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ImportAuditRepository : IImportAuditRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;
        private readonly ICourseDeliveryReadonlyDataContext _readonlyDataContext;

        public ImportAuditRepository (ICourseDeliveryDataContext dataContext, ICourseDeliveryReadonlyDataContext readonlyDataContext)
        {
            _dataContext = dataContext;
            _readonlyDataContext = readonlyDataContext;
        }

        public async Task Insert(ImportAudit importAudit)
        {
            await _dataContext.ImportAudit.AddAsync(importAudit);
            _dataContext.SaveChanges();
        }

        public async Task<ImportAudit> GetLastImportByType(ImportType importType)
        {
            var record = await _readonlyDataContext 
                .ImportAudit
                .OrderByDescending(c => c.TimeStarted)
                .FirstOrDefaultAsync(c => c.ImportType.Equals(importType));

            return record;
        }
    }
}