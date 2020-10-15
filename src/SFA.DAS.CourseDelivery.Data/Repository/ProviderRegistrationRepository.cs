using System.Linq;
using System.Threading.Tasks;
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

        public async Task InsertFromImportTable()
        {
            await _dataContext.ExecuteRawSql(SqlQueries.InsertProviderRegistrationsFromImport);
        }

        public void DeleteAll()
        {
            
            var toDelete = _dataContext.ProviderRegistrations.ToList();
            _dataContext.ProviderRegistrations.RemoveRange(toDelete);
            _dataContext.SaveChanges();
            
        }
    }
}