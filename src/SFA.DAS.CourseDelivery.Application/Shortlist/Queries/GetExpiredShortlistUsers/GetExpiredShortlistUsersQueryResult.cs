using System;
using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetExpiredShortlistUsers
{
    public class GetExpiredShortlistUsersQueryResult
    {
        public IEnumerable<Guid> UserIds { get; set; }
    }
}