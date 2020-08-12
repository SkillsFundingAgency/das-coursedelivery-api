using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class StandardLocationRepository : IStandardLocationRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;

        public StandardLocationRepository (ICourseDeliveryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertFromImportTable()
        {
            await _dataContext.ExecuteRawSql(
                @"INSERT INTO dbo.StandardLocation SELECT * FROM dbo.StandardLocation_Import");
            
        }

        public void DeleteAll()
        {
            _dataContext.StandardLocations.RemoveRange(_dataContext.StandardLocations.ToList());
            _dataContext.SaveChanges();
        }
    }
}