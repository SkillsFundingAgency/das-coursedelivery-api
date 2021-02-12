using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.AcceptanceTests.Infrastructure;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using TechTalk.SpecFlow;

namespace SFA.DAS.CourseDelivery.Api.AcceptanceTests.Steps
{
    [Binding]
    public class ShortlistSteps
    {
        private readonly ScenarioContext _context;

        public ShortlistSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Then("all shortlist items for the user are returned")]
        public async Task ThenAllShortlistItemsForUserAreReturned()
        {
            if (!_context.TryGetValue<HttpResponseMessage>(ContextKeys.HttpResponse, out var result))
            {
                Assert.Fail($"scenario context does not contain value for key [{ContextKeys.HttpResponse}]");
            }

            var model = await HttpUtilities.ReadContent<GetShortlistForUserResponse>(result.Content);

            var allShortlistsForUser = DbUtilities.GetAllShortlists()
                .Where(shortlist => shortlist.ShortlistUserId == Guid.Parse(DbUtilities.ShortlistUserId));

            model.Shortlist.Should().BeEquivalentTo(allShortlistsForUser, options=> options
                .ExcludingMissingMembers()
            );
        }
    }
}
