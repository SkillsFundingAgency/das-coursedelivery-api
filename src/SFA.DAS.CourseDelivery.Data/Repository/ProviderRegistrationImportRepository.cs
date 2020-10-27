using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderRegistrationImportRepository : IProviderRegistrationImportRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderRegistrationImportRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<ProviderRegistrationImport> providerImports)
        {
            await _dataContext.ProviderRegistrationImports.AddRangeAsync(providerImports);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            var itemsToDelete = _dataContext.ProviderRegistrationImports.ToList();
            _dataContext.ProviderRegistrationImports.RemoveRange(itemsToDelete);
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<ProviderRegistrationImport>> GetAll()
        {
            return await _dataContext.ProviderRegistrationImports.ToListAsync();
        }
        
        public async Task UpdateAddress(int ukprn, string postcode, double lat, double lon)
        {
            var providerImport = await _dataContext
                .ProviderRegistrationImports
                .SingleOrDefaultAsync(c => c.Ukprn.Equals(ukprn));

            providerImport.Lat = lat;
            providerImport.Long = lon;
            providerImport.Postcode = postcode;

            _dataContext.SaveChanges();
        }
    }
}