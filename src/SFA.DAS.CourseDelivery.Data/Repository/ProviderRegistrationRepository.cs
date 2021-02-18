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
        private readonly ICourseDeliveryReadonlyDataContext _readonlyDataContext;

        public ProviderRegistrationRepository(ICourseDeliveryDataContext dataContext, 
            ICourseDeliveryReadonlyDataContext readonlyDataContext)
        {
            _dataContext = dataContext;
            _readonlyDataContext = readonlyDataContext;
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
                .ToListAsync();

            foreach (var providerRegistrationImport in providerRegistrationImports)
            {
                var providerRegistration = await _dataContext.ProviderRegistrations.FindAsync(providerRegistrationImport.Ukprn);
                providerRegistration.Address1 = providerRegistrationImport.Address1;
                providerRegistration.Address2 = providerRegistrationImport.Address2;
                providerRegistration.Address3 = providerRegistrationImport.Address3;
                providerRegistration.Address4 = providerRegistrationImport.Address4;
                providerRegistration.Town = providerRegistrationImport.Town;
                providerRegistration.Postcode = providerRegistrationImport.Postcode;
                providerRegistration.Lat = providerRegistrationImport.Lat;
                providerRegistration.Long = providerRegistrationImport.Long;
            }
            _dataContext.SaveChanges();
        }

        public async Task<ProviderRegistration> GetByUkprn(int ukprn)
        {
            return await _readonlyDataContext.ProviderRegistrations.SingleOrDefaultAsync(c => c.Ukprn.Equals(ukprn));
        }
    }
}