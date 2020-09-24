using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationFeedbackAttributeImportRepository
{
    public class WhenCallingGetAll
    {
        [Test, MoqAutoData]
        public async Task Then_Returns_All_Records_From_DbContext(
            List<ProviderRegistrationFeedbackAttributeImport> importsInDb,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ProviderRegistrationFeedbackAttributeImportRepository repository)
        {
            mockContext
                .Setup(context => context.ProviderRegistrationFeedbackAttributeImports)
                .ReturnsDbSet(importsInDb);

            var actual = await repository.GetAll();

            actual.Should().BeEquivalentTo(importsInDb);
        }
    }
}