using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardLocationRepository
{
    public class WhenAddingMultipleItems
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
            _courseDeliveryDataContext.Setup(x => x.ProviderStandardLocations).ReturnsDbSet(new List<ProviderStandardLocation>());
            _providerStandardLocationRepository = new Data.Repository.ProviderStandardLocationRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_ProviderStandardLocation_Items_Are_Added_From_The_Import_Table()
        {
            //Act
            await _providerStandardLocationRepository.InsertFromImportTable();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>
                x.ExecuteRawSql(@"INSERT INTO dbo.ProviderStandardLocation SELECT * FROM dbo.ProviderStandardLocation_Import"), Times.Once);
        }
    }
}