using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateRepository
{
    public class WhenDeletingAllItems
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
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRates).ReturnsDbSet(_items);
            _repository = new Data.Repository.NationalAchievementRateRepository(_courseDeliveryDataContext.Object);
        }
        
        [Test]
        public void Then_The_NationalAchievementRates_Are_Removed()
        {
            //Act
            _repository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.NationalAchievementRates.RemoveRange(_courseDeliveryDataContext.Object.NationalAchievementRates), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}