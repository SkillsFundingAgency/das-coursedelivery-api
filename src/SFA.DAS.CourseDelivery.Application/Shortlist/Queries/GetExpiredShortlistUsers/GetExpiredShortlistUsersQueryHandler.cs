using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetExpiredShortlistUsers
{
    public class GetExpiredShortlistUsersQueryHandler : IRequestHandler<GetExpiredShortlistUsersQuery, GetExpiredShortlistUsersQueryResult>
    {
        private readonly IShortlistService _service;

        public GetExpiredShortlistUsersQueryHandler (IShortlistService service)
        {
            _service = service;
        }
        public async Task<GetExpiredShortlistUsersQueryResult> Handle(GetExpiredShortlistUsersQuery request, CancellationToken cancellationToken)
        {
            var shortlistUserIds = await _service.GetExpiredShortlistUserIds(request.ExpiryInDays);

            return new GetExpiredShortlistUsersQueryResult
            {
                UserIds = shortlistUserIds
            };
        }
    }
}