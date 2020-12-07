using System.Collections.Generic;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.RegisteredProviders
{
    public class GetRegisteredProvidersResponse
    {
        public IEnumerable<ProviderSummary> RegisteredProviders { get; set; }
    }
}