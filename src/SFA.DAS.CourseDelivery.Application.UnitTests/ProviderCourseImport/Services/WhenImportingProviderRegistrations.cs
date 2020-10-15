using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using ProviderRegistration = SFA.DAS.CourseDelivery.Domain.ImportTypes.ProviderRegistration;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.ProviderCourseImport.Services
{
    public class WhenImportingProviderRegistrations
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Provider_Registrations_From_Roatp_Api(
            [Frozen] Mock<IRoatpApiService> mockRoatpApiService,
            ProviderRegistrationImportService importService)
        {
            await importService.ImportData();

            mockRoatpApiService.Verify(service => service.GetProviderRegistrations(), Times.Once);
        }

        [Test, MoqAutoData]
        public async Task And_No_Records_To_Import_Then_No_Further_Processing(
            [Frozen] Mock<IRoatpApiService> mockRoatpApiService,
            [Frozen] Mock<IProviderRegistrationImportRepository> mockImportRepository,
            [Frozen] Mock<IProviderRegistrationRepository> mockRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackAttributeRepository> mockFeedbackAttributeRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackAttributeImportRepository> mockFeedbackAttributeImportRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackRatingRepository> mockFeedbackRatingRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackRatingImportRepository> mockFeedbackRatingImportRepository,
            ProviderRegistrationImportService importService)
        {
            mockRoatpApiService
                .Setup(service => service.GetProviderRegistrations())
                .ReturnsAsync(new List<ProviderRegistration>());

            await importService.ImportData();

            mockImportRepository.Verify(repository => repository.DeleteAll(), Times.Never);
            mockImportRepository.Verify(repository => repository.InsertMany(It.IsAny<IEnumerable<ProviderRegistrationImport>>()), Times.Never);
            mockRepository.Verify(repository => repository.DeleteAll(), Times.Never);
            mockRepository.Verify(repository => repository.InsertMany(It.IsAny<IEnumerable<Domain.Entities.ProviderRegistration>>()), Times.Never);
            mockFeedbackAttributeRepository.Verify(repository => repository.DeleteAll(), Times.Never);
            mockFeedbackAttributeRepository.Verify(repository => repository.InsertMany(It.IsAny<IEnumerable<ProviderRegistrationFeedbackAttribute>>()), Times.Never);
            mockFeedbackAttributeImportRepository.Verify(repository => repository.DeleteAll(), Times.Never);
            mockFeedbackAttributeImportRepository.Verify(repository => repository.InsertMany(It.IsAny<IEnumerable<ProviderRegistrationFeedbackAttributeImport>>()), Times.Never);
            mockFeedbackRatingRepository.Verify(repository => repository.DeleteAll(), Times.Never);
            mockFeedbackRatingRepository.Verify(repository => repository.InsertMany(It.IsAny<IEnumerable<ProviderRegistrationFeedbackRating>>()), Times.Never);
            mockFeedbackRatingImportRepository.Verify(repository => repository.DeleteAll(), Times.Never);
            mockFeedbackRatingImportRepository.Verify(repository => repository.InsertMany(It.IsAny<IEnumerable<ProviderRegistrationFeedbackRatingImport>>()), Times.Never);
        }

        [Test, MoqAutoData]
        public async Task Then_Adds_Roatp_Data_To_Import_Table(
            List<ProviderRegistration> providerRegistrationsFromRoatp,
            List<Domain.Entities.ProviderRegistrationImport> providerRegistrationImports,
            List<ProviderRegistrationFeedbackRatingImport> feedbackRatingImports,
            List<ProviderRegistrationFeedbackAttributeImport> feedbackAttributeImports,
            [Frozen] Mock<IRoatpApiService> mockRoatpApiService,
            [Frozen] Mock<IProviderRegistrationImportRepository> mockImportRepository,
            [Frozen] Mock<IProviderRegistrationRepository> mockRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackAttributeRepository> mockFeedbackAttributeRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackAttributeImportRepository> mockFeedbackAttributeImportRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackRatingRepository> mockFeedbackRatingRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackRatingImportRepository> mockFeedbackRatingImportRepository,
            ProviderRegistrationImportService importService)
        {
            mockRoatpApiService
                .Setup(service => service.GetProviderRegistrations())
                .ReturnsAsync(providerRegistrationsFromRoatp);
            mockFeedbackAttributeImportRepository.Setup(x => x.GetAll())
                .ReturnsAsync(feedbackAttributeImports);
            mockFeedbackRatingImportRepository.Setup(x => x.GetAll())
                .ReturnsAsync(feedbackRatingImports);
            mockImportRepository.Setup(x => x.GetAll())
                .ReturnsAsync(providerRegistrationImports);
                
            var expectedProviderRegistrationImports = providerRegistrationsFromRoatp
                .Select(registration => (ProviderRegistrationImport) registration)
                .ToList();
            
            var actualProviderRegistrationImports = new List<ProviderRegistrationImport>();
            
            mockImportRepository
                .Setup(repository =>
                    repository.InsertMany(It.IsAny<IEnumerable<ProviderRegistrationImport>>()))
                .Callback((IEnumerable<ProviderRegistrationImport> imports) =>
                    actualProviderRegistrationImports = imports.ToList())
                .Returns(Task.CompletedTask);
            
            await importService.ImportData();

            mockImportRepository.Verify(repository => repository.DeleteAll(), Times.Once);
            mockImportRepository.Verify(repository => repository.InsertMany(It.IsAny<IEnumerable<ProviderRegistrationImport>>()), Times.Once);
            mockFeedbackAttributeImportRepository.Verify(repository => repository.DeleteAll(), Times.Once);
            mockFeedbackAttributeImportRepository.Verify(
                repository =>
                    repository.InsertMany(It.Is<IEnumerable<ProviderRegistrationFeedbackAttributeImport>>(c=>
                        c.Count().Equals(providerRegistrationsFromRoatp.Sum(s => s.Feedback.ProviderAttributes.Count)))),
                Times.Once);
            mockFeedbackRatingImportRepository.Verify(repository => repository.DeleteAll(), Times.Once);
            mockFeedbackRatingImportRepository.Verify(
                repository => repository.InsertMany(It.Is<IEnumerable<ProviderRegistrationFeedbackRatingImport>>(s=>
                    s.Count().Equals(providerRegistrationsFromRoatp.Sum(c => c.Feedback.FeedbackRating.Count)))),
                Times.Once);
            actualProviderRegistrationImports.Should().BeEquivalentTo(expectedProviderRegistrationImports);
            mockRepository.Verify(repository => repository.DeleteAll(), Times.Once);
            mockFeedbackAttributeRepository.Verify(repository => repository.DeleteAll(), Times.Once);
            mockFeedbackRatingRepository.Verify(repository => repository.DeleteAll(), Times.Once);
            mockRepository.Verify(repository => repository.InsertMany(It.Is<IEnumerable<Domain.Entities.ProviderRegistration>>
                (c=> c.Count().Equals(providerRegistrationImports.Count))), Times.Once);
            mockFeedbackAttributeRepository.Verify(repository => repository.InsertMany(It.Is<IEnumerable<ProviderRegistrationFeedbackAttribute>>
                (c=> c.Count().Equals(feedbackAttributeImports.Count))), Times.Once);
            mockFeedbackRatingRepository.Verify(repository => repository.InsertMany(It.Is<IEnumerable<ProviderRegistrationFeedbackRating>>
                (c=> c.Count().Equals(feedbackRatingImports.Count))), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_Null_Provider_Feedback_Data_Is_Ignored(
            ProviderRegistration providerRegistrationFromRoatp,
            List<ProviderRegistrationFeedbackRatingImport> feedbackRatingImports,
            List<ProviderRegistrationFeedbackAttributeImport> feedbackAttributeImports,
            [Frozen] Mock<IRoatpApiService> mockRoatpApiService,
            [Frozen] Mock<IProviderRegistrationImportRepository> mockImportRepository,
            [Frozen] Mock<IProviderRegistrationRepository> mockRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackAttributeRepository> mockFeedbackAttributeRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackAttributeImportRepository> mockFeedbackAttributeImportRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackRatingRepository> mockFeedbackRatingRepository,
            [Frozen] Mock<IProviderRegistrationFeedbackRatingImportRepository> mockFeedbackRatingImportRepository,
            ProviderRegistrationImportService importService)
        {
            providerRegistrationFromRoatp.Feedback.FeedbackRating = null;
            providerRegistrationFromRoatp.Feedback.ProviderAttributes = null;
            var apiData = new List<ProviderRegistration> {providerRegistrationFromRoatp};
            mockRoatpApiService
                .Setup(service => service.GetProviderRegistrations())
                .ReturnsAsync(apiData);
            
            await importService.ImportData();
            
            mockFeedbackAttributeImportRepository.Verify(
                repository =>
                    repository.InsertMany(It.Is<IEnumerable<ProviderRegistrationFeedbackAttributeImport>>(c=>
                        c.Count().Equals(0))),
                Times.Once);
            mockFeedbackRatingImportRepository.Verify(
                repository => repository.InsertMany(It.Is<IEnumerable<ProviderRegistrationFeedbackRatingImport>>(s=>
                    s.Count().Equals(0))),
                Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_An_Audit_Record_Is_Added_After_Successful_Import(
            List<ProviderRegistration> providerRegistrationsFromRoatp,
            [Frozen] Mock<IRoatpApiService> mockRoatpApiService,
            [Frozen] Mock<IImportAuditRepository> mockAuditRepository,
            ProviderRegistrationImportService importService)
        {
            mockRoatpApiService
                .Setup(service => service.GetProviderRegistrations())
                .ReturnsAsync(providerRegistrationsFromRoatp);
            
            await importService.ImportData();
        
            mockAuditRepository.Verify(x=>x.Insert(It.Is<ImportAudit>(c=>
                c.ImportType == ImportType.ProviderRegistrations &&
                c.RowsImported.Equals(providerRegistrationsFromRoatp.Count))));
        }
    }
}