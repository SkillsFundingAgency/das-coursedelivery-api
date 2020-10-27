using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderImportRepository : IProviderImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderImportRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<ProviderImport> providerImports)
        {
            await _dataContext.ProviderImports.AddRangeAsync(providerImports);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            var providerImports = _dataContext.ProviderImports.ToList();
            _dataContext.ProviderImports.RemoveRange(providerImports);
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<ProviderImport>> GetAll()
        {
            var providerImports = await _dataContext.ProviderImports.ToListAsync();
            return providerImports;
        }

    }
}