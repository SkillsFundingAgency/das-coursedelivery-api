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

    }
}