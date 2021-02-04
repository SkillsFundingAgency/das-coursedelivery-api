using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistItemsForUser
{
    public class GetShortlistItemsForUserQueryResponse
    {
        public IEnumerable<Domain.Models.Shortlist> ShortlistItems { get; set; }
    }
}