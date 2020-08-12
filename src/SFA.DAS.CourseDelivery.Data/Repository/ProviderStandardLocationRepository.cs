using System.Linq;
using System.Threading.Tasks;
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

        public async Task InsertFromImportTable()
        {
            await _dataContext.ExecuteRawSql(
                @"INSERT INTO dbo.ProviderStandardLocation SELECT * FROM dbo.ProviderStandardLocation_Import");
            
        }

        public void DeleteAll()
        {
            _dataContext.ProviderStandardLocations.RemoveRange(_dataContext.ProviderStandardLocations.ToList());
            _dataContext.SaveChanges();
        }
    }
}