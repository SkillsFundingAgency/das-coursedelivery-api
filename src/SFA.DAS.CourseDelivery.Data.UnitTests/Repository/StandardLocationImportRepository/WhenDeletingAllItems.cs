using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.StandardLocationImportRepository
{
    public class WhenDeletingAllItems
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
            _courseDeliveryDataContext.Setup(x => x.StandardLocationImports).ReturnsDbSet(_standardLocationImports);
            _standardLocationImportRepository = new Data.Repository.StandardLocationImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public void Then_The_StandardLocationImports_Are_Removed()
        {
            //Act
            _standardLocationImportRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.StandardLocationImports.RemoveRange(_courseDeliveryDataContext.Object.StandardLocationImports), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}