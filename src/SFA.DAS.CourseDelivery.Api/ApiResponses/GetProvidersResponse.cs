using System.Collections.Generic;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.ApiResponses
{
    public class GetProvidersResponse
    {
        public IEnumerable<ProviderSummary> Providers { get; set; }
    }
}