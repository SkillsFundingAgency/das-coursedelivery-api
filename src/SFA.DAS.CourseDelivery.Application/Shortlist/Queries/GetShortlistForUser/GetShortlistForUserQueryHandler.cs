using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
            var shortlist = (await _shortlistService.GetAllForUser(request.UserId)).ToList();

            foreach (var item in shortlist)
            {
                item.ProviderLocation = await _providerService.GetProviderByUkprnAndStandard(
                    item.ProviderUkprn,
                    item.CourseId,
                    item.Lat,
                    item.Long,
                    item.CourseSector);
            }

            return new GetShortlistForUserQueryResponse
            {
                Shortlist = shortlist
            };
        }
    }
}