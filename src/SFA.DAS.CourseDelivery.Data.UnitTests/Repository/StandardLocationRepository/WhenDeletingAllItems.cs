using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.StandardLocationRepository
{
    public class WhenDeletingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.StandardLocationRepository _standardLocationRepository;
        private List<StandardLocation> _standardLocations;

        [SetUp]
        public void Arrange()
        {
            _standardLocations = new List<StandardLocation>
            {
                new StandardLocation
                {
                    LocationId = 1,
                    Address1 = "test"
                },
                new StandardLocation
                {
                    LocationId = 2,
                    Address1 = "test"
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.StandardLocations).ReturnsDbSet(_standardLocations);
            _standardLocationRepository = new Data.Repository.StandardLocationRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public void Then_The_StandardLocations_Are_Removed()
        {
            //Act
            _standardLocationRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.StandardLocations.RemoveRange(_courseDeliveryDataContext.Object.StandardLocations), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}