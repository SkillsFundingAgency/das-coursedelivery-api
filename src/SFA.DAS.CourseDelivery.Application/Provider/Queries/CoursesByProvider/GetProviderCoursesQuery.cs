using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.CoursesByProvider
{
    public class GetProviderCoursesQuery : IRequest<GetProviderCoursesQueryResponse>
    {
        public int Ukprn { get; set; }
    }
}