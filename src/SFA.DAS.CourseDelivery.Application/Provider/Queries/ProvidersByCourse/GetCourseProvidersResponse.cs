using System.Collections.Generic;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.ProvidersByCourse
{
    public class GetCourseProvidersResponse
    {
        public IEnumerable<Domain.Models.ProviderLocation> Providers { get ; set ; }
    }
}