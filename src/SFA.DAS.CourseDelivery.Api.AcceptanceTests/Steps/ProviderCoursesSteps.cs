using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.AcceptanceTests.Infrastructure;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.CourseDelivery.Api.AcceptanceTests.Steps
{
    public class ProviderCoursesSteps
    {
        private readonly ScenarioContext _context;

        public ProviderCoursesSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Then("provider courses are returned")]
        public async Task ThenProviderCoursesAreReturned()
        {
            if (!_context.TryGetValue<HttpResponseMessage>(ContextKeys.HttpResponse, out var result))
            {
                Assert.Fail($"scenario context does not contain value for key [{ContextKeys.HttpResponse}]");
            }

            var model = await HttpUtilities.ReadContent<GetProviderCoursesListResponse>(result.Content);
            model.Should().NotBeNull();
        }
    }
}
