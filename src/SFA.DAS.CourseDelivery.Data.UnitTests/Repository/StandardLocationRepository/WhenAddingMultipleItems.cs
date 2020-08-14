using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.StandardLocationRepository
{
    public class WhenAddingMultipleItems
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
            _courseDeliveryDataContext.Setup(x => x.StandardLocations).ReturnsDbSet(new List<StandardLocation>());
            _standardLocationRepository = new Data.Repository.StandardLocationRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_StandardLocation_Items_Are_Added()
        {
            //Act
            await _standardLocationRepository.InsertFromImportTable();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>
                x.ExecuteRawSql(@"INSERT INTO dbo.StandardLocation SELECT * FROM dbo.StandardLocation_Import"), Times.Once);
        }
    }
}