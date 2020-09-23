using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation
{
    public class GetUkprnsQueryResult
    {
        public IEnumerable<int> Ukprns { get ; set ; }
    }
}