using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.AcceptanceTests.Infrastructure;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Data.Extensions;
using TechTalk.SpecFlow;

namespace SFA.DAS.CourseDelivery.Api.AcceptanceTests.Steps
{
    [Binding]
    public class ProvidersSteps
    {
        private readonly ScenarioContext _context;

        public ProvidersSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Then("all registered providers are returned")]
        public async Task ThenAllRegisteredProvidersAreReturned()
        {
            if (!_context.TryGetValue<HttpResponseMessage>(ContextKeys.HttpResponse, out var result))
            {
                Assert.Fail($"scenario context does not contain value for key [{ContextKeys.HttpResponse}]");
            }

            var model = await HttpUtilities.ReadContent<GetProvidersResponse>(result.Content);

            var registeredProviders = DbUtilities.GetAllProviders()
                .Join(DbUtilities.GetAllProviderRegistrations(), 
                    provider => provider.Ukprn,
                    registration => registration.Ukprn,
                    (provider, registration) =>
                    {
                        provider.ProviderRegistration = registration;
                        return provider;
                    }).AsQueryable()
                .FilterRegisteredProviders().ToList();

            model.Providers.Should().BeEquivalentTo(registeredProviders, options=> options
                .ExcludingMissingMembers()
            );
        }
    }
}