using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderRegistrationRepository : IProviderRegistrationRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderRegistrationRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<ProviderRegistration> providerRegistrations)
        {
            await _dataContext.ProviderRegistrations.AddRangeAsync(providerRegistrations);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            
            var toDelete = _dataContext.ProviderRegistrations.ToList();
            _dataContext.ProviderRegistrations.RemoveRange(toDelete);
            _dataContext.SaveChanges();
            
        }
        
        public async Task UpdateAddressesFromImportTable()
        {
            var providerRegistrationImports = await _dataContext
                .ProviderRegistrationImports
                .Where(c=>!string.IsNullOrEmpty(c.Postcode))
                .ToListAsync();

            foreach (var providerRegistrationImport in providerRegistrationImports)
            {
                var providerRegistration = await _dataContext.ProviderRegistrations.FindAsync(providerRegistrationImport.Ukprn);
                providerRegistration.Postcode = providerRegistrationImport.Postcode;
                providerRegistration.Lat = providerRegistrationImport.Lat;
                providerRegistration.Long = providerRegistrationImport.Long;
            }
            _dataContext.SaveChanges();
        }
    }
}