using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ImportAuditRepository
{
    public class WhenGettingAnItemByImportType
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private List<ImportAudit> _importAudits;
        private Data.Repository.ImportAuditRepository _importAuditRepository;
        private  const string ExpectedFileName = "TestFile1";

        [SetUp]
        public void Arrange()
        {
            _importAudits = new List<ImportAudit>
            {
                new ImportAudit(new DateTime(2020,10,01), 100),
                new ImportAudit(new DateTime(2020,09,30), 100, ImportType.NationalAchievementRates, "TestFile"),
                new ImportAudit(new DateTime(2020,10,01), 101, ImportType.NationalAchievementRates, ExpectedFileName)
            };
            
            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ImportAudit).ReturnsDbSet(_importAudits);
            

            _importAuditRepository = new Data.Repository.ImportAuditRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_Latest_ImportAudit_Record_Is_Returned()
        {
            //Act
            var auditRecord = await _importAuditRepository.GetLastImportByType(ImportType.NationalAchievementRates);
            
            //Assert
            Assert.IsNotNull(auditRecord);
            auditRecord.FileName.Should().Be(ExpectedFileName);
        }

        [Test]
        public async Task Then_No_File_Returns_Null()
        {
            //Arrange
            _importAudits = new List<ImportAudit>
            {
                new ImportAudit(new DateTime(2020,10,01), 100)
            };
            
            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ImportAudit).ReturnsDbSet(_importAudits);
            _importAuditRepository = new Data.Repository.ImportAuditRepository(_courseDeliveryDataContext.Object);
            
            //Act
            var auditRecord = await _importAuditRepository.GetLastImportByType(ImportType.NationalAchievementRates);
            
            //Assert
            Assert.IsNull(auditRecord);
        }
    }
}