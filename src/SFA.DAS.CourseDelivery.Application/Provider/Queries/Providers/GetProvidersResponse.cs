using System.Collections.Generic;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.Providers
{
    public class GetProvidersResponse
    {
        public IEnumerable<ProviderSummary> RegisteredProviders { get; set; }
    }
}