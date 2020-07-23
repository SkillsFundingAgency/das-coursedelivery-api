using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderStandardLocationImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderStandardLocationImportRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<ProviderStandardLocationImport>> GetAll()
        {
            var items = await _dataContext.ProviderStandardLocationImports.ToListAsync();
            return items;
        }

        public void DeleteAll()
        {
            _dataContext.ProviderStandardLocationImports.RemoveRange(_dataContext.ProviderStandardLocationImports);
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<ProviderStandardLocationImport> items)
        {
            await _dataContext.ProviderStandardLocationImports.AddRangeAsync(items);
            _dataContext.SaveChanges();
        }
    }
}