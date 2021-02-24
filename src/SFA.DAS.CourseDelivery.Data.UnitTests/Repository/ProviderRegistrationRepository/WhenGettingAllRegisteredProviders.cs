using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationRepository
{
    public class WhenGettingAllRegisteredProviders
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Returns_All_Providers(
            List<ProviderRegistration> recordsInDb,
            [Frozen] Mock<ICourseDeliveryReadonlyDataContext> mockContext,
            Data.Repository.ProviderRegistrationRepository repository)
        {
            recordsInDb = recordsInDb.Select(c =>
            {
                c.StatusId = 1;
                c.ProviderTypeId = 1;
                return c;
            }).ToList();
            recordsInDb.First().ProviderTypeId = 0;
            recordsInDb.First().StatusId = 0;
            mockContext
                .Setup(context => context.ProviderRegistrations)
                .ReturnsDbSet(recordsInDb);

            var actual = await repository.GetAllProviders();

            actual.Should().BeEquivalentTo(recordsInDb);
        }
    }
}