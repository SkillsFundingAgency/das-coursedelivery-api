using System;
using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUser
{
    public class GetShortlistForUserQuery : IRequest<GetShortlistForUserQueryResponse>
    {
        public Guid UserId { get; set; }
    }
}