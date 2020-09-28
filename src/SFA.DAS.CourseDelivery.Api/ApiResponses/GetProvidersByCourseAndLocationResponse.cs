using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProvidersByCourseAndLocationResponse
    {
        public IEnumerable<int> UkprnsByStandardAndLocation { get; set; }
        public IEnumerable<int> UkprnsByStandard { get ; set ; }
    }
}