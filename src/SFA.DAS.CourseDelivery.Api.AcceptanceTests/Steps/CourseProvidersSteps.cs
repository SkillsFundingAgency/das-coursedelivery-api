using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.AcceptanceTests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.Linq;
using SFA.DAS.CourseDelivery.Api.ApiResponses;

namespace SFA.DAS.CourseDelivery.Api.AcceptanceTests.Steps
{
    [Binding]
    public class CourseProvidersSteps
    {
        private readonly ScenarioContext _context;

        public CourseProvidersSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Then("course providers are returned")]
        public async Task ThenCourseProvidersAreReturned()
        {
            if (!_context.TryGetValue<HttpResponseMessage>(ContextKeys.HttpResponse, out var result))
            {
                Assert.Fail($"scenario context does not contain value for key [{ContextKeys.HttpResponse}]");
            }

            var model = await HttpUtilities.ReadContent<GetCourseProvidersListResponse>(result.Content);

            model.Providers.ToList().Count().Equals(0);
        }
    }
}
