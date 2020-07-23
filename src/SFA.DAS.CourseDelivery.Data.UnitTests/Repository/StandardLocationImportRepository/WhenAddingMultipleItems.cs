using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.StandardLocationImportRepository
{
    public class WhenAddingMultipleItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.StandardLocationImportRepository _standardLocationImportRepository;
        private List<StandardLocationImport> _standardLocationImports;

        [SetUp]
        public void Arrange()
        {
            _standardLocationImports = new List<StandardLocationImport>
            {
                new StandardLocationImport
                {
                    LocationId = 1,
                    Address1 = "test"
                },
                new StandardLocationImport
                {
                    LocationId = 2,
                    Address1 = "test"
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.StandardLocationImports).ReturnsDbSet(new List<StandardLocationImport>());
            _standardLocationImportRepository = new Data.Repository.StandardLocationImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_StandardLocationImport_Items_Are_Added()
        {
            //Act
            await _standardLocationImportRepository.InsertMany(_standardLocationImports);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.StandardLocationImports.AddRangeAsync(_standardLocationImports, It.IsAny<CancellationToken>()), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}