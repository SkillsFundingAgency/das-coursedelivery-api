using System;
using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse
{
    public class GetCourseProvidersQuery : IRequest<GetCourseProvidersQueryResponse>
    {
        public int StandardId { get ; set ; }
        public double? Lat { get ; set ; }
        public double? Lon { get ; set ; }
        public short SortOrder { get ; set ; }
        public string SectorSubjectArea { get ; set ; }
        public short Level { get ; set ; }
        public Guid ShortlistUserId { get ; set ; }
    }
}