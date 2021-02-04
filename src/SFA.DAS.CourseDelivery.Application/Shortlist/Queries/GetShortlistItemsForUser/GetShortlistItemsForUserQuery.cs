using System;
using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistItemsForUser
{
    public class GetShortlistItemsForUserQuery : IRequest<GetShortlistItemsForUserQueryResponse>
    {
        public Guid UserId { get; set; }
    }
}