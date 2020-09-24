using System.Collections.Generic;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationFeedbackRatingImportRepository
{
    public class WhenCallingDeleteAll
    {
        [Test, MoqAutoData]
        public void Then_Deletes_All_Records_In_Db(
            List<ProviderRegistrationFeedbackRatingImport> importsInDb,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ProviderRegistrationFeedbackRatingImportRepository repository)
        {
            mockContext
                .Setup(context => context.ProviderRegistrationFeedbackRatingImports)
                .ReturnsDbSet(importsInDb);

            repository.DeleteAll();

            mockContext.Verify(context => context.ProviderRegistrationFeedbackRatingImports.RemoveRange(importsInDb), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}