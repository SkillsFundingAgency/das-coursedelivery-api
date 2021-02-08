using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateImportRepository
{
    public class WhenAddingMultipleItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.Import.NationalAchievementRateImportRepository _importRepository;
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
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRateImports).ReturnsDbSet(new List<NationalAchievementRateImport>());
            _importRepository = new Data.Repository.Import.NationalAchievementRateImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_NationalAchievementRateImport_Items_Are_Added()
        {
            //Act
            await _importRepository.InsertMany(_importItems);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.NationalAchievementRateImports.AddRangeAsync(_importItems, It.IsAny<CancellationToken>()), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}