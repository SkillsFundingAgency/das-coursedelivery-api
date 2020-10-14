using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateOverallRepository
{
    public class WhenDeletingAllItems
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
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRateOverall).ReturnsDbSet(_items);
            _repository = new Data.Repository.NationalAchievementRateOverallRepository(_courseDeliveryDataContext.Object, Mock.Of<ICourseDeliveryReadonlyDataContext>());
        }
        
        [Test]
        public void Then_The_NationalAchievementRateOverall_Items_Are_Removed()
        {
            //Act
            _repository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.NationalAchievementRateOverall.RemoveRange(_courseDeliveryDataContext.Object.NationalAchievementRateOverall), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}