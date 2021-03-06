using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardLocationRepository
{
    public class WhenDeletingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderStandardLocationRepository _providerStandardLocationRepository;
        private List<ProviderStandardLocation> _providerStandardLocations;

        [SetUp]
        public void Arrange()
        {
            _providerStandardLocations = new List<ProviderStandardLocation>
            {
                new ProviderStandardLocation
                {
                    StandardId = 1,
                    Ukprn = 123
                },
                new ProviderStandardLocation
                {
                    StandardId = 2,
                    Ukprn = 123
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandardLocations).ReturnsDbSet(_providerStandardLocations);
            _providerStandardLocationRepository = new Data.Repository.ProviderStandardLocationRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public void Then_The_ProviderStandardLocations_Are_Removed()
        {
            //Act
            _providerStandardLocationRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.ProviderStandardLocations
                .RemoveRange(It.Is<List<ProviderStandardLocation>>(c=>
                    c.ToList().Count.Equals(_providerStandardLocations.Count))), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}