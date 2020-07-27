using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class StandardLocationRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public StandardLocationRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<StandardLocation> standardLocations)
        {
            await _dataContext.StandardLocations.AddRangeAsync(standardLocations);
            _dataContext.SaveChanges();
        }

        public void DeleteAll()
        {
            _dataContext.StandardLocations.RemoveRange(_dataContext.StandardLocations);
            _dataContext.SaveChanges();
        }
    }
}