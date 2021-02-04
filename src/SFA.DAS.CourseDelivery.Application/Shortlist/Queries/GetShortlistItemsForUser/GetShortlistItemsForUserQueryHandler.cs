using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistItemsForUser
{
    public class GetShortlistItemsForUserQueryHandler : IRequestHandler<GetShortlistItemsForUserQuery, GetShortlistItemsForUserQueryResponse>
    {
        private readonly IShortlistService _shortlistService;

        public GetShortlistItemsForUserQueryHandler(IShortlistService shortlistService)
        {
            _shortlistService = shortlistService;
        }

        public async Task<GetShortlistItemsForUserQueryResponse> Handle(GetShortlistItemsForUserQuery request, CancellationToken cancellationToken)
        {
            var shortlist = await _shortlistService.GetAllForUser(request.UserId);

            return new GetShortlistItemsForUserQueryResponse
            {
                ShortlistItems = shortlist
            };
        }
    }
}