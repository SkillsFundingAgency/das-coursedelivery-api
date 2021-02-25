using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetShortlistForUserResponse
    {
        public IEnumerable<GetShortlistItemResponse> Shortlist { get; set; }
    }
}