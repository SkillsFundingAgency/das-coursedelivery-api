using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationFeedbackRatingImportRepository
{
    public class WhenCallingInsertMany
    {
        [Test, MoqAutoData]
        public async Task Then_Inserts_Records_Into_DbContext(
            List<ProviderRegistrationFeedbackRatingImport> importsInDb,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ProviderRegistrationFeedbackRatingImportRepository repository)
        {
            await repository.InsertMany(importsInDb);

            mockContext.Verify(context => context.ProviderRegistrationFeedbackRatingImports.AddRangeAsync(
                importsInDb, 
                It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}