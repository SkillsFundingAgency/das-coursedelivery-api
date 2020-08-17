using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProviderCoursesListResponse
    {
        public IEnumerable<int> StandardIds { get; set; }
        public int TotalResults { get; set; }
    }
}