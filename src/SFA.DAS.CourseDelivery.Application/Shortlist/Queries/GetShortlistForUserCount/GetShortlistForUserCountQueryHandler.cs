using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUserCount
{
    public class GetShortlistForUserCountQueryHandler : IRequestHandler<GetShortlistForUserCountQuery, int>
    {
        private readonly IShortlistService _service;

        public GetShortlistForUserCountQueryHandler (IShortlistService service)
        {
            _service = service;
        }
        public async Task<int> Handle(GetShortlistForUserCountQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetShortlistItemCountForUser(request.UserId);

            return result;
        }
    }
}