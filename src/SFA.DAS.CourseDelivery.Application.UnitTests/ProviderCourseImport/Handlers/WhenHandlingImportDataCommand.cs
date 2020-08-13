using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Handlers.ImportProviderStandards;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.ProviderCourseImport.Handlers
{
    public class WhenHandlingImportDataCommand
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called_To_Import_Data(
            ImportDataCommand command,
            [Frozen] Mock<IProviderCourseImportService> providerStandardsImportService,
            [Frozen] Mock<INationalAchievementRatesImportService> nationalAchievementRatesImportService,
            ImportDataCommandHandler handler)
        {
            // Act
            await handler.Handle(command, new CancellationToken());
            
            //Assert
            providerStandardsImportService.Verify(x=>x.ImportProviderCourses(), Times.Once);
            nationalAchievementRatesImportService.Verify(x=>x.ImportData(), Times.Once);
            
        }
    }
}