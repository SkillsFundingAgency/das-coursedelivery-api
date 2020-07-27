using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderStandardLocationRepository : IProviderStandardLocationRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderStandardLocationRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<ProviderStandardLocation> providerStandardLocations)
        {
            await _dataContext.ProviderStandardLocations.AddRangeAsync(providerStandardLocations);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _dataContext.ProviderStandardLocations.RemoveRange(_dataContext.ProviderStandardLocations);
            _dataContext.SaveChanges();
        }
    }
}