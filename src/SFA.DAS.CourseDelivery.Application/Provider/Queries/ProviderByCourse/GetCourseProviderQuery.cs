using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse
{
    public class GetCourseProviderQuery : IRequest<GetCourseProviderQueryResponse>
    {
        public int Ukprn { get ; set ; }
        public int StandardId { get ; set ; }
    }
}