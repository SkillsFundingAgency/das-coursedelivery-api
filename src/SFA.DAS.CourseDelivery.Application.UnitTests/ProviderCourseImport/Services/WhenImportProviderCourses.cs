using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using StandardLocation = SFA.DAS.CourseDelivery.Domain.ImportTypes.StandardLocation;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.ProviderCourseImport.Services
{
    public class WhenImportProviderCourses
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_ProviderCourses_Are_Read_From_The_Api(
            [Frozen] Mock<ICourseDirectoryService> service,
            ProviderCourseImportService providerCourseImportService)
        {
            //Act
            await providerCourseImportService.ImportProviderCourses();
            
            //Assert
            service.Verify(x=>x.GetProviderCourseInformation(), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Staging_Tables_Are_Emptied(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            ProviderCourseImportService providerCourseImportService)
        {
            //Act
            await providerCourseImportService.ImportProviderCourses();
            
            //Assert
            providerStandardImportRepository.Verify(x=>x.DeleteAll(), Times.Once);
            providerImportRepository.Verify(x=>x.DeleteAll(), Times.Once);
            providerStandardLocationImportRepository.Verify(x=>x.DeleteAll(), Times.Once);
            standardLocationImportRepository.Verify(x=>x.DeleteAll(), Times.Once);
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Staging_Tables_Are_Not_Emptied_If_No_Data_Is_Returned(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            ProviderCourseImportService providerCourseImportService)
        {
            //Arrange
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(new List<Domain.ImportTypes.Provider>());
            
            //Act
            await providerCourseImportService.ImportProviderCourses();
            
            //Assert
            providerStandardImportRepository.Verify(x=>x.DeleteAll(), Times.Never);
            providerImportRepository.Verify(x=>x.DeleteAll(), Times.Never);
            providerStandardLocationImportRepository.Verify(x=>x.DeleteAll(), Times.Never);
            standardLocationImportRepository.Verify(x=>x.DeleteAll(), Times.Never);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_Distinct_Providers_Are_Loaded_Into_The_Import_Table(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            Domain.ImportTypes.Provider providerImportDuplicate,
            Domain.ImportTypes.Provider providerImportDuplicateIdDifferentUkprn,
            List<Domain.ImportTypes.Provider> providerImports,
            CourseLocation courseLocation,
            ProviderCourseImportService providerCourseImportService)
        {
            //Arrange
            providerImportDuplicateIdDifferentUkprn.Ukprn = providerImportDuplicate.Ukprn;
            providerImports.Add(providerImportDuplicate);
            providerImports.Add(providerImportDuplicate);
            providerImports.Add(providerImportDuplicateIdDifferentUkprn);
            
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(providerImports);
            
            //Act
            await providerCourseImportService.ImportProviderCourses();
            
            //Assert
            providerImportRepository.Verify(x=>
                                        x.InsertMany(It.Is<List<ProviderImport>>(
                                            c=> c.Count.Equals(providerImports.Count - 1)
                                            )
                                        ), Times.Once);
            
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Distinct_Locations_Are_Loaded_Into_The_Import_Table(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            List<Domain.ImportTypes.Provider> providerImport,
            CourseLocation courseLocation,
            ProviderCourseImportService providerCourseImportService
            )
        {
            //Arrange 
            providerImport.FirstOrDefault().Locations.Add(courseLocation);
            providerImport.FirstOrDefault().Locations.Add(courseLocation);
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(providerImport);
            
            //Act
            await providerCourseImportService.ImportProviderCourses();
            
            //Assert
            standardLocationImportRepository.Verify(x=>
                x.InsertMany(It.Is<List<StandardLocationImport>>(c=>
                    c.Count.Equals(providerImport.Sum(d=>d.Locations.Count)-1))), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_National_Flag_Is_Populated_From_Location_And_Provider_Standard_Information(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            long locationId,
            Domain.ImportTypes.Provider providerImport,
            CourseStandard standardImport,
            CourseLocation locationImport,
            ProviderCourseImportService providerCourseImportService)
        {
            //Arrange
            locationImport.Address.Lat =  52.564269;
            locationImport.Address.Long = -1.466056 ;
            locationImport.Id = locationId;
            standardImport.Locations = new List<StandardLocation>{new StandardLocation
            {
                Radius = 500,
                Id = locationId,
                DeliveryModes = new List<string>{"100PercentEmployer"}
            }};
            providerImport.Locations = new List<CourseLocation>{locationImport};
            providerImport.Standards = new List<CourseStandard>{standardImport};
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(new List<Domain.ImportTypes.Provider>{providerImport});
            
            //Act
            await providerCourseImportService.ImportProviderCourses();
            
            //Assert
            providerStandardLocationImportRepository.Verify(x =>
                x.InsertMany(It.Is<List<ProviderStandardLocationImport>>(c => c.TrueForAll(ps => ps.National))));
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Distinct_ProviderStandards_Are_Loaded_Into_The_Import_Table(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            List<Domain.ImportTypes.Provider> providerImport,
            CourseStandard courseStandard,
            ProviderCourseImportService providerCourseImportService
        )
        {
            //Arrange 
            providerImport.FirstOrDefault().Standards.Add(courseStandard);
            providerImport.FirstOrDefault().Standards.Add(courseStandard);
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(providerImport);
            
            //Act
            await providerCourseImportService.ImportProviderCourses();
            
            //Assert
            providerStandardImportRepository.Verify(x=>
                x.InsertMany(It.Is<List<ProviderStandardImport>>(c=>
                    c.Count.Equals(providerImport.Sum(d=>d.Standards.Count)-1))), Times.Once);
        }
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Distinct_ProviderStandardLocations_Are_Loaded_Into_The_Import_Table(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            List<Domain.ImportTypes.Provider> providerImport,
            Domain.ImportTypes.StandardLocation standardLocation,
            ProviderCourseImportService providerCourseImportService
        )
        {
            //Arrange 
            providerImport.FirstOrDefault().Standards.FirstOrDefault().Locations.Add(standardLocation);
            providerImport.FirstOrDefault().Standards.FirstOrDefault().Locations.Add(standardLocation);
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(providerImport);
            
            //Act
            await providerCourseImportService.ImportProviderCourses();
            
            //Assert
            providerStandardLocationImportRepository.Verify(x=>
                x.InsertMany(It.Is<List<ProviderStandardLocationImport>>(c=>
                    c.Count.Equals(providerImport.SelectMany(d=>d.Standards).Sum(e=>e.Locations.Count)-1))), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Data_Is_Loaded_Into_The_Staging_Table_From_The_Api(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            List<Domain.ImportTypes.Provider> providerImport,
            ProviderCourseImportService standardsImportService)
        {
            //Arrange
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(providerImport);
            
            //Act
            await standardsImportService.ImportProviderCourses();
            
            //Assert
            providerImportRepository.Verify(x=>
                x.InsertMany(It.Is<List<ProviderImport>>(c=>
                    c.Count.Equals(providerImport.Count))), Times.Once);
            standardLocationImportRepository.Verify(x=>
                x.InsertMany(It.Is<List<StandardLocationImport>>(c=>
                    c.Count.Equals(providerImport.Sum(d=>d.Locations.Count)))), Times.Once);
            providerStandardImportRepository.Verify(x=>
                x.InsertMany(It.Is<List<ProviderStandardImport>>(c=>
                    c.Count.Equals(providerImport.Sum(d=>d.Standards.Count)))), Times.Once);
            providerStandardLocationImportRepository.Verify(x=>
                x.InsertMany(It.Is<List<ProviderStandardLocationImport>>(c=>
                    c.Count.Equals(providerImport.SelectMany(d=>d.Standards).Sum(e=>e.Locations.Count)))), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_ProviderStandard_Data_Is_Deleted_When_There_Is_New_Data_In_The_Import_Table(
            [Frozen] Mock<IProviderStandardRepository> providerStandardRepository,
            [Frozen] Mock<IProviderRepository> providerRepository,
            [Frozen] Mock<IProviderStandardLocationRepository> providerStandardLocationRepository,
            [Frozen] Mock<IStandardLocationRepository> standardLocationRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            List<Domain.ImportTypes.Provider> providerImport,
            ProviderCourseImportService standardsImportService)
        {
            //Arrange
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(providerImport);
            
            //Act
            await standardsImportService.ImportProviderCourses();
            
            //Assert
            providerStandardRepository.Verify(x=>x.DeleteAll(), Times.Once);
            providerRepository.Verify(x=>x.DeleteAll(), Times.Once);
            providerStandardLocationRepository.Verify(x=>x.DeleteAll(), Times.Once);
            standardLocationRepository.Verify(x=>x.DeleteAll(), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_ImportData_Is_Loaded_Into_The_Data_Tables(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            [Frozen] Mock<IProviderStandardRepository> providerStandardRepository,
            [Frozen] Mock<IProviderRepository> providerRepository,
            [Frozen] Mock<IProviderStandardLocationRepository> providerStandardLocationRepository,
            [Frozen] Mock<IStandardLocationRepository> standardLocationRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            List<Domain.ImportTypes.Provider> providerImport,
            List<ProviderImport> providers,
            List<ProviderStandardImport> providerStandards,
            List<ProviderStandardLocationImport> providerStandardLocations,
            List<StandardLocationImport> standardLocations,
            ProviderCourseImportService standardsImportService)
        {
            //Arrange
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(providerImport);
            providerImportRepository.Setup(x => x.GetAll()).ReturnsAsync(providers);
            providerStandardImportRepository.Setup(x => x.GetAll()).ReturnsAsync(providerStandards);
            providerStandardLocationImportRepository.Setup(x => x.GetAll()).ReturnsAsync(providerStandardLocations);
            standardLocationImportRepository.Setup(x => x.GetAll()).ReturnsAsync(standardLocations);
            
            //Act
            await standardsImportService.ImportProviderCourses();
            
            //Assert
            providerStandardRepository
                .Verify(x=>
                    x.InsertMany(It.Is<List<ProviderStandard>>(c=>c.Count.Equals(providerStandards.Count))), Times.Once);
            providerRepository
                .Verify(x=>
                    x.InsertMany(It.Is<List<Domain.Entities.Provider>>(c=>c.Count.Equals(providers.Count))), Times.Once);
            providerStandardLocationRepository
                .Verify(x=>
                    x.InsertMany(It.Is<List<ProviderStandardLocation>>(c=>c.Count.Equals(providerStandardLocations.Count))), Times.Once);
            standardLocationRepository
                .Verify(x=>
                    x.InsertMany(It.Is<List<Domain.Entities.StandardLocation>>(c=>c.Count.Equals(standardLocations.Count))), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_An_Audit_Record_Is_Added_After_Successfully_Added(
            [Frozen] Mock<IProviderStandardImportRepository> providerStandardImportRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderStandardLocationImportRepository> providerStandardLocationImportRepository,
            [Frozen] Mock<IStandardLocationImportRepository> standardLocationImportRepository,
            [Frozen] Mock<ICourseDirectoryService> service,
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            List<Domain.ImportTypes.Provider> providerImport,
            List<ProviderImport> providers,
            List<ProviderStandardImport> providerStandards,
            List<ProviderStandardLocationImport> providerStandardLocations,
            List<StandardLocationImport> standardLocations,
            ProviderCourseImportService standardsImportService)
        {
            //Arrange
            service.Setup(x => x.GetProviderCourseInformation()).ReturnsAsync(providerImport);
            providerImportRepository.Setup(x => x.GetAll()).ReturnsAsync(providers);
            providerStandardImportRepository.Setup(x => x.GetAll()).ReturnsAsync(providerStandards);
            providerStandardLocationImportRepository.Setup(x => x.GetAll()).ReturnsAsync(providerStandardLocations);
            standardLocationImportRepository.Setup(x => x.GetAll()).ReturnsAsync(standardLocations);
            
            //Act
            await standardsImportService.ImportProviderCourses();
        
            //Assert
            auditRepository.Verify(x=>x.Insert(It.Is<ImportAudit>(c=>c.RowsImported.Equals(providers.Count))));
        }
    }
}