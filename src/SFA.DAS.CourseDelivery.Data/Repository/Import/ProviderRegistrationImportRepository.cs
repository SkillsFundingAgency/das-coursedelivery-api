using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository.Import
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
        
        public async Task UpdateAddress(int ukprn, ContactAddress address, double lat, double lon)
        {
            var providerRegistrationImport = await _dataContext
                .ProviderRegistrationImports
                .SingleOrDefaultAsync(c => c.Ukprn.Equals(ukprn));

            providerRegistrationImport.Lat = lat;
            providerRegistrationImport.Long = lon;
            providerRegistrationImport.Address1 = address.Address1;
            providerRegistrationImport.Address2 = address.Address2;
            providerRegistrationImport.Address3 = address.Address3;
            providerRegistrationImport.Address4 = address.Address4;
            providerRegistrationImport.Town = address.Town;
            providerRegistrationImport.Postcode = address.PostCode;

            _dataContext.SaveChanges();
        }
    }
}