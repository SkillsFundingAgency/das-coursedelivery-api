using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateImportRepository
{
    public class WhenDeletingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.NationalAchievementRateImportRepository _importRepository;
        private List<NationalAchievementRateImport> _importItems;

        [SetUp]
        public void Arrange()
        {
            _importItems = new List<NationalAchievementRateImport>
            {
                new NationalAchievementRateImport
                {
                    Id = 1
                },
                new NationalAchievementRateImport
                {
                    Id = 1
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRateImports).ReturnsDbSet(_importItems);
            _importRepository = new Data.Repository.NationalAchievementRateImportRepository(_courseDeliveryDataContext.Object);
        }
        
        [Test]
        public void Then_The_NationalAchievementRateImports_Are_Removed()
        {
            //Act
            _importRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.NationalAchievementRateImports.RemoveRange(_courseDeliveryDataContext.Object.NationalAchievementRateImports), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}