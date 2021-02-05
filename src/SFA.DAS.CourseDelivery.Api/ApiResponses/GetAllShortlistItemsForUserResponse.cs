using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetAllShortlistItemsForUserResponse
    {
        public IEnumerable<GetShortlistResponse> ShortlistItems { get; set; }
    }
}