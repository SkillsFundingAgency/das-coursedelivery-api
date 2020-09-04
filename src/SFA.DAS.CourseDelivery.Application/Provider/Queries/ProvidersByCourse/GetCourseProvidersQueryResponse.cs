using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse
{
    public class GetCourseProvidersQueryResponse
    {
        public IEnumerable<Domain.Models.ProviderLocation> Providers { get ; set ; }
    }
}