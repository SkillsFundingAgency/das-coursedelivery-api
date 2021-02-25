using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateOverallImportRepository
{
    public class WhenDeletingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.Import.NationalAchievementRateOverallImportRepository _importRepository;
        private List<NationalAchievementRateOverallImport> _importItems;

        [SetUp]
        public void Arrange()
        {
            _importItems = new List<NationalAchievementRateOverallImport>
            {
                new NationalAchievementRateOverallImport
                {
                    Id = 1
                },
                new NationalAchievementRateOverallImport
                {
                    Id = 1
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRateOverallImports).ReturnsDbSet(_importItems);
            _importRepository = new Data.Repository.Import.NationalAchievementRateOverallImportRepository(_courseDeliveryDataContext.Object);
        }
        
        [Test]
        public void Then_The_NationalAchievementRateOverallImports_Are_Removed()
        {
            //Act
            _importRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.NationalAchievementRateOverallImports.RemoveRange(_courseDeliveryDataContext.Object.NationalAchievementRateOverallImports), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}