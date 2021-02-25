using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationFeedbackAttributeImportRepository
{
    public class WhenCallingInsertMany
    {
        [Test, MoqAutoData]
        public async Task Then_Inserts_Records_Into_DbContext(
            List<ProviderRegistrationFeedbackAttributeImport> importsInDb,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.Import.ProviderRegistrationFeedbackAttributeImportRepository repository)
        {
            await repository.InsertMany(importsInDb);

            mockContext.Verify(context => context.ProviderRegistrationFeedbackAttributeImports.AddRangeAsync(
                importsInDb, 
                It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}