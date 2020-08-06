using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.CoursesByProvider
{
    public class GetProviderCoursesResponse
    {
        public IEnumerable<int> CourseIds { get; set; }
    }
}