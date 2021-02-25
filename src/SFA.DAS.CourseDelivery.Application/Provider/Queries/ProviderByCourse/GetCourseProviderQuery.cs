using System;
using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse
{
    public class GetCourseProviderQuery : IRequest<GetCourseProviderQueryResponse>
    {
        public int Ukprn { get ; set ; }
        public int StandardId { get ; set ; }
        public double? Lat { get ; set ; } = 0d;
        public double? Lon { get ; set ; } = 0d;
        public string SectorSubjectArea { get ; set ; }
        public Guid? ShortlistUserId { get ; set ; }
    }
}