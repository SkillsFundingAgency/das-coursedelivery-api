using System.Collections.Generic;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationImportRepository
{
    public class WhenCallingDeleteAll
    {
        [Test, MoqAutoData]
        public void Then_Deletes_All_Records_In_Db(
            List<ProviderRegistrationImport> providerRegistrationImportsInDb,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.Import.ProviderRegistrationImportRepository repository)
        {
            mockContext
                .Setup(context => context.ProviderRegistrationImports)
                .ReturnsDbSet(providerRegistrationImportsInDb);

            repository.DeleteAll();

            mockContext.Verify(context => context.ProviderRegistrationImports.RemoveRange(providerRegistrationImportsInDb), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}