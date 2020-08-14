using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.CoursesByProvider
{
    public class GetProviderCoursesQueryHandler : IRequestHandler<GetProviderCoursesQuery,GetProviderCoursesResponse>
    {
        private readonly IProviderService _providerService;

        public GetProviderCoursesQueryHandler(IProviderService providerService)
        {
            _providerService = providerService;
        }

        public async Task<GetProviderCoursesResponse> Handle(GetProviderCoursesQuery request, CancellationToken cancellationToken)
        {
            var standardIds = await _providerService.GetStandardIdsByUkprn(request.Ukprn);

            return new GetProviderCoursesResponse
            {
                CourseIds = standardIds
            };
        }
    }
}