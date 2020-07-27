using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<Provider> providers)
        {
            await _dataContext.Providers.AddRangeAsync(providers);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _dataContext.Providers.RemoveRange(_dataContext.Providers);
            _dataContext.SaveChanges();
        }
    }
}