using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationImportRepository
{
    public class WhenCallingGetAll
    {
        [Test, MoqAutoData]
        public async Task Then_Returns_All_Records_From_DbContext(
            List<ProviderRegistrationImport> recordsInDb,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ProviderRegistrationImportRepository repository)
        {
            mockContext
                .Setup(context => context.ProviderRegistrationImports)
                .ReturnsDbSet(recordsInDb);

            var actual = await repository.GetAll();

            actual.Should().BeEquivalentTo(recordsInDb);
        }
    }
}