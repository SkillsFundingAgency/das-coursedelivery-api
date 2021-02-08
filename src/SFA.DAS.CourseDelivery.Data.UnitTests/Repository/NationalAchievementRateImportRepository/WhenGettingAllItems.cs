using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateImportRepository
{
    public class WhenGettingAllItems
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
                    Id = 1,
                    OverallAchievementRate = 1
                },
                new NationalAchievementRateImport
                {
                    Id = 2,
                    OverallCohort = 1
                },
                new NationalAchievementRateImport
                {
                    Id = 3,
                    OverallCohort = null,
                    OverallAchievementRate = null
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRateImports).ReturnsDbSet(_importItems);
            _importRepository = new Data.Repository.Import.NationalAchievementRateImportRepository(_courseDeliveryDataContext.Object);
        }
        
        [Test]
        public async Task Then_The_NationalAchievementRateImport_Items_Are_Returned_That_Have_Values()
        {
            //Act
            var actual = await _importRepository.GetAllWithAchievementData();
            
            //Assert
            Assert.IsNotNull(actual);
            actual.Count().Should().Be(2);
        }
    }
}