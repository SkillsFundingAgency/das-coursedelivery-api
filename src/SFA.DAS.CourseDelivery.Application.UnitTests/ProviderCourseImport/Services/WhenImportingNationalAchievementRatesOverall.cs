using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Infrastructure.Configuration;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.ProviderCourseImport.Services
{
    public class WhenImportingNationalAchievementRatesOverall
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Download_Path_Is_Parsed_From_The_Url_And_The_Current_File_Is_Checked_Against_The_Existing_And_If_Same_Then_No_Download(
            string filePath,
            [Frozen] Mock<INationalAchievementRatesPageParser> pageParser,
            [Frozen] Mock<IDataDownloadService> downloadService,
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            [Frozen] Mock<INationalAchievementRateOverallRepository> repository,
            NationalAchievementRatesOverallImportService service)
        {
            //Arrange
            pageParser.Setup(x => x.GetCurrentDownloadFilePath()).ReturnsAsync(filePath);
            auditRepository.Setup(x => x.GetLastImportByType(ImportType.NationalAchievementRatesOverall))
                .ReturnsAsync(new ImportAudit(DateTime.Now, 100, ImportType.NationalAchievementRatesOverall, filePath));
            
            //Act
            await service.ImportData();
            
            //Assert
            pageParser.Verify(x=>x.GetCurrentDownloadFilePath(), Times.Once);
            downloadService.Verify(x=>x.GetFileStream(It.IsAny<string>()), Times.Never);
            repository.Verify(x=>x.DeleteAll(), Times.Never);
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Download_Path_Is_Parsed_From_The_Url_And_Downloaded_If_No_Previous_File(
            string filePath,
            string content,
            [Frozen] Mock<INationalAchievementRatesPageParser> pageParser,
            [Frozen] Mock<IDataDownloadService> downloadService,
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            [Frozen] Mock<INationalAchievementRateOverallRepository> repository,
            NationalAchievementRatesOverallImportService service)
        {
            //Arrange
            downloadService.Setup(x => x.GetFileStream(filePath))
                .ReturnsAsync(new MemoryStream(Encoding.UTF8.GetBytes(content)));
            pageParser.Setup(x => x.GetCurrentDownloadFilePath()).ReturnsAsync(filePath);
            auditRepository.Setup(x => x.GetLastImportByType(ImportType.NationalAchievementRatesOverall))
                .ReturnsAsync((ImportAudit)null);
            
            //Act
            await service.ImportData();
            
            //Assert
            pageParser.Verify(x=>x.GetCurrentDownloadFilePath(), Times.Once);
            downloadService.Verify(x=>x.GetFileStream(filePath), Times.Once);
        }
        
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Download_Path_Is_Parsed_From_The_Url_And_Downloaded_If_Different_File(
            string filePath,
            string newFilePath,
            string content,
            [Frozen] Mock<INationalAchievementRatesPageParser> pageParser,
            [Frozen] Mock<IDataDownloadService> downloadService,
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            [Frozen] Mock<INationalAchievementRateOverallRepository> repository,
            NationalAchievementRatesOverallImportService service)
        {
            //Arrange
            downloadService.Setup(x => x.GetFileStream(newFilePath))
                .ReturnsAsync(new MemoryStream(Encoding.UTF8.GetBytes(content)));
            pageParser.Setup(x => x.GetCurrentDownloadFilePath()).ReturnsAsync(newFilePath);
            auditRepository.Setup(x => x.GetLastImportByType(ImportType.NationalAchievementRatesOverall))
                .ReturnsAsync(new ImportAudit(DateTime.Now, 100, ImportType.NationalAchievementRatesOverall, filePath));
            
            //Act
            await service.ImportData();
            
            //Assert
            pageParser.Verify(x=>x.GetCurrentDownloadFilePath(), Times.Once);
            downloadService.Verify(x=>x.GetFileStream(newFilePath), Times.Once);
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_NationalAchievementRatesOverall_Csv_File_Is_Extracted_From_The_Archive(
            string filePath,
            string content,
            string newFilePath,
            [Frozen] Mock<INationalAchievementRatesPageParser> pageParser,
            [Frozen] Mock<IDataDownloadService> service,
            [Frozen] Mock<IImportAuditRepository> repository,
            [Frozen] Mock<IZipArchiveHelper> zipHelper,
            NationalAchievementRatesOverallImportService importService)
        {
            //Arrange
            service.Setup(x => x.GetFileStream(newFilePath))
                .ReturnsAsync(new MemoryStream(Encoding.UTF8.GetBytes(content)));
            pageParser.Setup(x => x.GetCurrentDownloadFilePath()).ReturnsAsync(newFilePath);
            repository.Setup(x => x.GetLastImportByType(ImportType.NationalAchievementRatesOverall))
                .ReturnsAsync((ImportAudit)null);
            
            //Act
            await importService.ImportData();
            
            //Assert
            zipHelper.Verify(x=>
                    x.ExtractModelFromCsvFileZipStream<NationalAchievementRateOverallCsv>(It.IsAny<Stream>(),Constants.NationalAchievementRatesOverallCsvFileName), 
                Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Import_Table_Is_Cleared_And_Data_Loaded_From_Csv(
            string filePath,
            string content,
            string newFilePath,
            List<NationalAchievementRateOverallCsv> downloadData,
            [Frozen] Mock<INationalAchievementRatesPageParser> pageParser,
            [Frozen] Mock<IDataDownloadService> service,
            [Frozen] Mock<IImportAuditRepository> repository,
            [Frozen] Mock<INationalAchievementRateOverallImportRepository> importRepository,
            [Frozen] Mock<IZipArchiveHelper> zipHelper,
            NationalAchievementRatesOverallImportService importService)
        {
            //Arrange
            service.Setup(x => x.GetFileStream(newFilePath))
                .ReturnsAsync(new MemoryStream(Encoding.UTF8.GetBytes(content)));
            pageParser.Setup(x => x.GetCurrentDownloadFilePath()).ReturnsAsync(newFilePath);
            repository.Setup(x => x.GetLastImportByType(ImportType.NationalAchievementRatesOverall))
                .ReturnsAsync((ImportAudit)null);
            zipHelper.Setup(x =>
                x.ExtractModelFromCsvFileZipStream<NationalAchievementRateOverallCsv>(It.IsAny<Stream>(),
                    Constants.NationalAchievementRatesOverallCsvFileName)).Returns(downloadData);
            
            //Act
            await importService.ImportData();
            
            //Assert
            importRepository.Verify(x=>x.DeleteAll(), Times.Once);
            importRepository.Verify(x =>
                x.InsertMany(It.Is<List<NationalAchievementRateOverallImport>>(c => c.Count.Equals(downloadData.Count))));
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Imported_Data_Is_Loaded_To_Actual_Table_Excluding_Null_Values(
            string filePath,
            string content,
            string newFilePath,
            List<NationalAchievementRateOverallImport> importData,
            [Frozen] Mock<INationalAchievementRatesPageParser> pageParser,
            [Frozen] Mock<IDataDownloadService> service,
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            [Frozen] Mock<INationalAchievementRateOverallImportRepository> importRepository,
            [Frozen] Mock<INationalAchievementRateOverallRepository> repository,
            [Frozen] Mock<IZipArchiveHelper> zipHelper,
            NationalAchievementRatesOverallImportService importService)
        {
            //Arrange
            service.Setup(x => x.GetFileStream(newFilePath))
                .ReturnsAsync(new MemoryStream(Encoding.UTF8.GetBytes(content)));
            pageParser.Setup(x => x.GetCurrentDownloadFilePath()).ReturnsAsync(newFilePath);
            auditRepository.Setup(x => x.GetLastImportByType(ImportType.NationalAchievementRatesOverall))
                .ReturnsAsync((ImportAudit)null);
            importRepository.Setup(x => x.GetAllWithAchievementData()).ReturnsAsync(importData);
            
            //Act
            await importService.ImportData();
            
            //Assert
            repository.Verify(x=>x.DeleteAll(), Times.Once);
            repository.Verify(x =>
                x.InsertMany(It.Is<List<NationalAchievementRateOverall>>(c => c.Count.Equals(importData.Count))));
        }
        
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Import_Is_Audited(
            string filePath,
            string content,
            string newFilePath,
            List<NationalAchievementRateOverallImport> importData,
            [Frozen] Mock<INationalAchievementRatesPageParser> pageParser,
            [Frozen] Mock<IDataDownloadService> service,
            [Frozen] Mock<IImportAuditRepository> auditRepository,
            [Frozen] Mock<INationalAchievementRateOverallImportRepository> importRepository,
            [Frozen] Mock<INationalAchievementRateOverallRepository> repository,
            [Frozen] Mock<IZipArchiveHelper> zipHelper,
            NationalAchievementRatesOverallImportService importService)
        {
            //Arrange
            service.Setup(x => x.GetFileStream(newFilePath))
                .ReturnsAsync(new MemoryStream(Encoding.UTF8.GetBytes(content)));
            pageParser.Setup(x => x.GetCurrentDownloadFilePath()).ReturnsAsync(newFilePath);
            auditRepository.Setup(x => x.GetLastImportByType(ImportType.NationalAchievementRatesOverall))
                .ReturnsAsync((ImportAudit)null);
            importRepository.Setup(x => x.GetAllWithAchievementData()).ReturnsAsync(importData);
            
            //Act
            await importService.ImportData();
            
            //Assert
            auditRepository.Verify(x => x.Insert(It.Is<ImportAudit>(c =>
                c.FileName.Equals(newFilePath) 
                && c.ImportType.Equals(ImportType.NationalAchievementRatesOverall) 
                && c.RowsImported.Equals(importData.Count))));
        }
    }
}