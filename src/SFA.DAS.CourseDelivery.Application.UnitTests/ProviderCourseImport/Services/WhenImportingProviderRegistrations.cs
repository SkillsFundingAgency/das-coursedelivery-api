using System;
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
            ProviderRegistrationImportService importService)
        {
            mockRoatpApiService
                .Setup(service => service.GetProviderRegistrations())
                .ReturnsAsync(new List<ProviderRegistration>());

            await importService.ImportData();

            mockImportRepository.Verify(repository => repository.DeleteAll(), Times.Never);
            mockImportRepository.Verify(repository => repository.InsertMany(It.IsAny<IEnumerable<ProviderRegistrationImport>>()), Times.Never);
            mockRepository.Verify(repository => repository.DeleteAll(), Times.Never);
            mockRepository.Verify(repository => repository.InsertFromImportTable(), Times.Never);
        }

        [Test, MoqAutoData]
        public async Task Then_Adds_Roatp_Data_To_Import_Table(
            List<ProviderRegistration> providerRegistrationsFromRoatp,
            [Frozen] Mock<IRoatpApiService> mockRoatpApiService,
            [Frozen] Mock<IProviderRegistrationImportRepository> mockImportRepository,
            [Frozen] Mock<IProviderRegistrationRepository> mockRepository,
            ProviderRegistrationImportService importService)
        {
            mockRoatpApiService
                .Setup(service => service.GetProviderRegistrations())
                .ReturnsAsync(providerRegistrationsFromRoatp);
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
            actualProviderRegistrationImports.Should().BeEquivalentTo(expectedProviderRegistrationImports);
            mockRepository.Verify(repository => repository.DeleteAll(), Times.Once);
            mockRepository.Verify(repository => repository.InsertFromImportTable(), Times.Once);
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