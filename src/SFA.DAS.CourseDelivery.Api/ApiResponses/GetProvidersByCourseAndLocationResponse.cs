using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProvidersByCourseAndLocationResponse
    {
        public IEnumerable<int> Ukprns { get; set; }
    }
}