using System.Collections.Generic;
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
        public async Task Then_The_NationalAchievementRateImport_Items_Are_Returned()
        {
            //Act
            var actual = await _importRepository.GetAll();
            
            //Assert
            Assert.IsNotNull(actual);
            actual.Should().BeEquivalentTo(_importItems);
        }
    }
}