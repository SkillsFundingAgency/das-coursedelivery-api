using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateRepository
{
    public class WhenAddingMultipleItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.NationalAchievementRateRepository _repository;
        private List<NationalAchievementRate> _items;

        [SetUp]
        public void Arrange()
        {
            _items = new List<NationalAchievementRate>
            {
                new NationalAchievementRate
                {
                    Id = 1
                },
                new NationalAchievementRate
                {
                    Id = 1
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRates).ReturnsDbSet(new List<NationalAchievementRate>());
            _repository = new Data.Repository.NationalAchievementRateRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_NationalAchievementRate_Items_Are_Added()
        {
            //Act
            await _repository.InsertMany(_items);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.NationalAchievementRates.AddRangeAsync(_items, It.IsAny<CancellationToken>()), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}