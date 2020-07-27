using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetCourseProvidersListResponse
    {
        public IEnumerable<GetCourseProviderResponse> Providers { get ; set ; }
        public int TotalResults { get ; set ; }
    }
}