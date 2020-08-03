using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ProviderStandardRepository : IProviderStandardRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public ProviderStandardRepository(ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeleteAll()
        {
            _dataContext.ProviderStandards.RemoveRange(_dataContext.ProviderStandards);
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<ProviderStandard> providerStandards)
        {
            await _dataContext.ProviderStandards.AddRangeAsync(providerStandards);
            _dataContext.SaveChanges();
        }
    }
}