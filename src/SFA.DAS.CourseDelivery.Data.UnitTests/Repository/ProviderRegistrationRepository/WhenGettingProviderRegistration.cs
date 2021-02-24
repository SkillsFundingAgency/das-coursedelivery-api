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
        public async Task Then_Returns_Record_Matching_Ukprn_From_DbContext_That_Are_Registered(
            List<ProviderRegistration> recordsInDb,
            [Frozen] Mock<ICourseDeliveryReadonlyDataContext> mockContext,
            Data.Repository.ProviderRegistrationRepository repository)
        {
            recordsInDb.First().ProviderTypeId = 1;
            recordsInDb.First().StatusId = 1;
            mockContext
                .Setup(context => context.ProviderRegistrations)
                .ReturnsDbSet(recordsInDb);
            var ukprn = recordsInDb.First().Ukprn;

            var actual = await repository.GetProviderByUkprn(ukprn);

            actual.Should().BeEquivalentTo(recordsInDb.First(c=>c.Ukprn.Equals(ukprn)));
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_Returns_Matching_Ukprn_From_DbContext_That_Are_Not_Registered(
            List<ProviderRegistration> recordsInDb,
            [Frozen] Mock<ICourseDeliveryReadonlyDataContext> mockContext,
            Data.Repository.ProviderRegistrationRepository repository)
        {
            recordsInDb.First().ProviderTypeId = 0;
            recordsInDb.First().StatusId = 0;
            mockContext
                .Setup(context => context.ProviderRegistrations)
                .ReturnsDbSet(recordsInDb);
            var ukprn = recordsInDb.First().Ukprn;

            var actual = await repository.GetProviderByUkprn(ukprn);

            actual.Should().BeEquivalentTo(recordsInDb.First(c=>c.Ukprn.Equals(ukprn)));
        }
    }
}