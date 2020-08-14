using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.CoursesByProvider
{
    public class GetProviderCoursesQuery : IRequest<GetProviderCoursesResponse>
    {
        public int Ukprn { get; set; }
    }
}