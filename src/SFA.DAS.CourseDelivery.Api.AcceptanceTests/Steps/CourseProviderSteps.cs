using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.AcceptanceTests.Infrastructure;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.ProviderByCourse;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using System.Linq;
using FluentAssertions;
using SFA.DAS.CourseDelivery.Api.ApiResponses;

namespace SFA.DAS.CourseDelivery.Api.AcceptanceTests.Steps
{
    [Binding]
    public class CourseProviderSteps
    {
        private readonly ScenarioContext _context;

        public CourseProviderSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Then("specific course provider is returned")]
        public async Task ThenCourseProviderIsReturned()
        {
            if (!_context.TryGetValue<HttpResponseMessage>(ContextKeys.HttpResponse, out var result))
            {
                Assert.Fail($"scenario context does not contain value for key [{ContextKeys.HttpResponse}]");
            }

            var model = await HttpUtilities.ReadContent<GetCourseProviderQueryResponse>(result.Content);
            var resp = new GetProviderResponse().Map(model.ProviderStandardLocation);
            resp.Should().NotBeNull();
        }
    }
}
