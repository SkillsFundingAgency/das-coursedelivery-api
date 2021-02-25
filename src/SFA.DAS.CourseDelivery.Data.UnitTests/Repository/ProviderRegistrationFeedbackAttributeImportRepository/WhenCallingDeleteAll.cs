using System.Collections.Generic;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationFeedbackAttributeImportRepository
{
    public class WhenCallingDeleteAll
    {
        [Test, MoqAutoData]
        public void Then_Deletes_All_Records_In_Db(
            List<ProviderRegistrationFeedbackAttributeImport> importsInDb,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.Import.ProviderRegistrationFeedbackAttributeImportRepository repository)
        {
            mockContext
                .Setup(context => context.ProviderRegistrationFeedbackAttributeImports)
                .ReturnsDbSet(importsInDb);

            repository.DeleteAll();

            mockContext.Verify(context => context.ProviderRegistrationFeedbackAttributeImports.RemoveRange(importsInDb), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}