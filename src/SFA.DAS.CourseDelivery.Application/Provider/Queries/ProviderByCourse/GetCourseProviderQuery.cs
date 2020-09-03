using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse
{
    public class GetCourseProviderQuery : IRequest<GetCourseProviderResponse>
    {
        public int Ukprn { get ; set ; }
        public int StandardId { get ; set ; }
    }
}