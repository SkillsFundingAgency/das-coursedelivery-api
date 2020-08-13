using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateOverallRepository
{
    public class WhenAddingMultipleItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.NationalAchievementRateOverallRepository _repository;
        private List<NationalAchievementRateOverall> _items;

        [SetUp]
        public void Arrange()
        {
            _items = new List<NationalAchievementRateOverall>
            {
                new NationalAchievementRateOverall
                {
                    Id = 1
                },
                new NationalAchievementRateOverall
                {
                    Id = 1
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRateOverall).ReturnsDbSet(new List<NationalAchievementRateOverall>());
            _repository = new Data.Repository.NationalAchievementRateOverallRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_NationalAchievementRateOverall_Items_Are_Added()
        {
            //Act
            await _repository.InsertMany(_items);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.NationalAchievementRateOverall.AddRangeAsync(_items, It.IsAny<CancellationToken>()), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}