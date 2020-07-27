using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardLocationRepository
{
    public class WhenDeletingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderStandardLocationRepository _providerStandardLocationImportRepository;
        private List<ProviderStandardLocation> _providerStandardLocationImports;

        [SetUp]
        public void Arrange()
        {
            _providerStandardLocationImports = new List<ProviderStandardLocation>
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
            _courseDeliveryDataContext.Setup(x => x.ProviderStandardLocations).ReturnsDbSet(_providerStandardLocationImports);
            _providerStandardLocationImportRepository = new Data.Repository.ProviderStandardLocationRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public void Then_The_ProviderStandardLocations_Are_Removed()
        {
            //Act
            _providerStandardLocationImportRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.ProviderStandardLocations.RemoveRange(_courseDeliveryDataContext.Object.ProviderStandardLocations), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}