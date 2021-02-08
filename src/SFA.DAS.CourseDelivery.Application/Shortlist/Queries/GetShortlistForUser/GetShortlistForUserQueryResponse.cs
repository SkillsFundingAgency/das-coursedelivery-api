using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUser
{
    public class GetShortlistForUserQueryResponse
    {
        public IEnumerable<Domain.Models.Shortlist> ShortlistItems { get; set; }
    }
}