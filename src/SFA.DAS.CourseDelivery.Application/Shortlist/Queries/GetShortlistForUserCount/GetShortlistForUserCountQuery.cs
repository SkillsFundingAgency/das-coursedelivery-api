using System;
using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUserCount
{
    public class GetShortlistForUserCountQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }
}