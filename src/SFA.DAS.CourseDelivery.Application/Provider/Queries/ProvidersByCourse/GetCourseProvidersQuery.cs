using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse
{
    public class GetCourseProvidersQuery : IRequest<GetCourseProvidersResponse>
    {
        public int StandardId { get ; set ; }
    }
}