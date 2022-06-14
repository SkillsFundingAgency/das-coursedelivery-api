using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ApprenticeFeedbackAttributesRepository : IApprenticeFeedbackAttributesRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;
        private readonly ICourseDeliveryReadonlyDataContext _readonlyDataContext;

        public ApprenticeFeedbackAttributesRepository(ICourseDeliveryDataContext dataContext, ICourseDeliveryReadonlyDataContext readonlyDataContext)
        {
            _dataContext = dataContext;
            _readonlyDataContext = readonlyDataContext;
        }
        public void DeleteAll()
        {
            var toDelete = _dataContext.ApprenticeFeedbackAttributes.ToList();
            _dataContext.ApprenticeFeedbackAttributes.RemoveRange(toDelete);
            _dataContext.SaveChanges();
        }

        public async Task InsertMany(IEnumerable<ApprenticeFeedbackAttributes> apprenticeFeedbackAttributes)
        {
            await _dataContext.ApprenticeFeedbackAttributes.AddRangeAsync(apprenticeFeedbackAttributes);
            _dataContext.SaveChanges();
        }
    }
}
