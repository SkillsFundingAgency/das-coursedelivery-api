using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderStandard
{
    public class GetProviderStandardQuery : IRequest<GetProviderStandardResponse>
    {
        public int Ukprn { get ; set ; }
        public int StandardId { get ; set ; }
    }
}