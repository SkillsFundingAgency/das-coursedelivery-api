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
    public class WhenGettingProviderRegistration
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Returns_Record_Matching_Ukprn_From_DbContext(
            List<ProviderRegistration> recordsInDb,
            [Frozen] Mock<ICourseDeliveryReadonlyDataContext> mockContext,
            Data.Repository.ProviderRegistrationRepository repository)
        {
            mockContext
                .Setup(context => context.ProviderRegistrations)
                .ReturnsDbSet(recordsInDb);

            var actual = await repository.GetByUkprn(recordsInDb.First().Ukprn);

            actual.Should().BeEquivalentTo(recordsInDb);
        }
    }
}