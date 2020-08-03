using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse
{
    public class GetCourseProvidersResponse
    {
        public IEnumerable<Domain.Entities.Provider> Providers { get ; set ; }
    }
}