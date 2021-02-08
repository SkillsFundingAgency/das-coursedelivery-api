using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository.Import
{
    public class ProviderStandardImportRepository : IProviderStandardImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderStandardImportRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<ProviderStandardImport>> GetAll()
        {
            var providerStandardImports = await _dataContext.ProviderStandardImports.ToListAsync();
            return providerStandardImports;
        }

        public void DeleteAll()
        {
            _dataContext.ProviderStandardImports.RemoveRange(_dataContext.ProviderStandardImports);
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<ProviderStandardImport> providerStandardImports)
        {
            await _dataContext.ProviderStandardImports.AddRangeAsync(providerStandardImports);
            _dataContext.SaveChanges();
        }
    }
}