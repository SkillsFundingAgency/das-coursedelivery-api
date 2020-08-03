using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ImportAuditRepository
{
    public class WhenAddingImportAuditItem
    {
        private Mock<ICourseDeliveryDataContext> _dataContext;
        private ImportAudit _importAudit;
        private Data.Repository.ImportAuditRepository _importAuditRepository;
        private readonly DateTime _expectedDate = new DateTime(2020,10,30);
        private const int ExpectedRowsImported = 100;
        
        [SetUp]
        public void Arrange()
        {
            _importAudit = new ImportAudit(_expectedDate, 100);
            
            _dataContext = new Mock<ICourseDeliveryDataContext>();
            _dataContext.Setup(x => x.ImportAudit).ReturnsDbSet(new List<ImportAudit>());
            _importAuditRepository = new Data.Repository.ImportAuditRepository(_dataContext.Object);
        }

        [Test]
        public async Task Then_The_Import_Audit_Record_Is_Added()
        {
            //Act
            await _importAuditRepository.Insert(_importAudit);
            
            //Assert
            _dataContext.Verify(x=>
                    x.ImportAudit.AddAsync(
                        It.Is<ImportAudit>(c=>c.TimeStarted.Equals(_expectedDate)
                                              && c.RowsImported.Equals(ExpectedRowsImported))
                        ,It.IsAny<CancellationToken>())
                , Times.Once);
            _dataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}