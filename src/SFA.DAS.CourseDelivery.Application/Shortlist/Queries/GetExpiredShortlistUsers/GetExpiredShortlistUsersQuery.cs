using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetExpiredShortlistUsers
{
    public class GetExpiredShortlistUsersQuery : IRequest<GetExpiredShortlistUsersQueryResult>
    {
        public uint ExpiryInDays { get; set; }
        
    }
}