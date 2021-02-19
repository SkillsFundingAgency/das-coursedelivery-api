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

            var actual = await repository.GetRegisteredApprovedAndActiveProviderByUkprn(ukprn);

            actual.Should().BeEquivalentTo(recordsInDb.First(c=>c.Ukprn.Equals(ukprn)));
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_ReturnsNull_Matching_Ukprn_From_DbContext_That_Are_Not_Registered(
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

            var actual = await repository.GetRegisteredApprovedAndActiveProviderByUkprn(ukprn);

            actual.Should().BeNull();
        }
    }

    public class WhenGettingAllRegisteredProviders
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Returns_All_Registered_Approved_And_Active_Providers(
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

            var actual = await repository.GetAllRegisteredApprovedAndActiveProviders();

            actual.Should().BeEquivalentTo(recordsInDb.Where(c => c.ProviderTypeId == 1 && c.StatusId == 1));
        }
    }
}