using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.Provider
{
    public class GetProviderQuery : IRequest<GetProviderResponse>
    {
        public int Ukprn { get ; set ; }
        public int StandardId { get ; set ; }
    }
}