using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class StandardLocationImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public StandardLocationImportRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<StandardLocationImport> items)
        {
            await _dataContext.StandardLocationImports.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _dataContext.StandardLocationImports.RemoveRange(_dataContext.StandardLocationImports);
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<StandardLocationImport>> GetAll()
        {
            var items = await _dataContext.StandardLocationImports.ToListAsync();
            return items;
        }
    }
}