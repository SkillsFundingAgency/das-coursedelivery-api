using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateOverallImportRepository
{
    public class WhenAddingMultipleItems
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
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRateOverallImports).ReturnsDbSet(new List<NationalAchievementRateOverallImport>());
            _importRepository = new Data.Repository.Import.NationalAchievementRateOverallImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_NationalAchievementRateOverallImport_Items_Are_Added()
        {
            //Act
            await _importRepository.InsertMany(_importItems);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.NationalAchievementRateOverallImports.AddRangeAsync(_importItems, It.IsAny<CancellationToken>()), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}