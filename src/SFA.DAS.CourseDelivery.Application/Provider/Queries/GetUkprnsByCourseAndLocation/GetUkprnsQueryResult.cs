using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation
{
    public class GetUkprnsQueryResult
    {
        public IEnumerable<int> UkprnsByStandardAndLocation { get ; set ; }
        public IEnumerable<int> UkprnsByStandard { get ; set ; }
    }
}