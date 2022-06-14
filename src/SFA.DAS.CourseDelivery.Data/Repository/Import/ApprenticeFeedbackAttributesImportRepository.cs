using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository.Import
{
    public class ApprenticeFeedbackAttributesImportRepository : IApprenticeFeedbackAttributesImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ApprenticeFeedbackAttributesImportRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<ApprenticeFeedbackAttributesImport> apprenticeFeedbackAttributesImports)
        {
            await _dataContext.ApprenticeFeedbackAttributesImports.AddRangeAsync(apprenticeFeedbackAttributesImports);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            var itemsToDelete = _dataContext.ApprenticeFeedbackAttributesImports.ToList();
            _dataContext.ApprenticeFeedbackAttributesImports.RemoveRange(itemsToDelete);
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<ApprenticeFeedbackAttributesImport>> GetAll()
        {
            return await _dataContext.ApprenticeFeedbackAttributesImports.ToListAsync();
        }
    }
}