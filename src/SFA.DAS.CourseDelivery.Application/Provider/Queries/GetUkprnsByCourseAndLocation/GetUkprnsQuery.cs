using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation
{
    public class GetUkprnsQuery : IRequest<GetUkprnsQueryResult>
    {
        public int StandardId { get ; set ; }
        public double Lat { get ; set ; }
        public double Lon { get ; set ; }
    }
}