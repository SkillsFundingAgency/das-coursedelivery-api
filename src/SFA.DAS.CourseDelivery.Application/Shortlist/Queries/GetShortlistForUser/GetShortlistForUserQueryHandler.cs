using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Extensions;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUser
{
    public class GetShortlistForUserQueryHandler : IRequestHandler<GetShortlistForUserQuery, GetShortlistForUserQueryResponse>
    {
        private readonly IShortlistService _shortlistService;
        private readonly IProviderService _providerService;

        public GetShortlistForUserQueryHandler(
            IShortlistService shortlistService,
            IProviderService providerService)
        {
            _shortlistService = shortlistService;
            _providerService = providerService;
        }

        public async Task<GetShortlistForUserQueryResponse> Handle(GetShortlistForUserQuery request, CancellationToken cancellationToken)
        {
            var items = await _shortlistService.GetAllForUserWithProviders(request.UserId);

            return new GetShortlistForUserQueryResponse
            {
                Shortlist = items
            };
        }
    }
}