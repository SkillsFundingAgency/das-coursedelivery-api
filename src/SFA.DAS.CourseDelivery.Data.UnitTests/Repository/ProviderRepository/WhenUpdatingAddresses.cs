using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRepository
{
    public class WhenUpdatingAddresses
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderRepository _providerRepository;
        private List<ProviderImport> _providerImports;
        private Provider _expectedProvider;
        private const double ExpectedLat = 2.1;
        private const double ExpectedLong = -2.1;
        private const string ExpectedPostcode = "TE 3T";

        [SetUp]
        public void Arrange()
        {
            _expectedProvider = new Provider();
            _providerImports = new List<ProviderImport>
            {
                new ProviderImport
                {
                    Id = 123,
                    Ukprn = 123,
                    Lat = ExpectedLat,
                    Long = ExpectedLong,
                    Postcode = ExpectedPostcode
                },
                new ProviderImport
                {
                    Id = 1234,
                    Ukprn = 1234,
                    Lat = ExpectedLat,
                    Long = ExpectedLong,
                    Postcode = ExpectedPostcode
                }
            };
            
            
            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderImports).ReturnsDbSet(_providerImports);
            _courseDeliveryDataContext.Setup(x => x.Providers.FindAsync(It.IsAny<long>()))
                .ReturnsAsync(_expectedProvider);
            
            _providerRepository = new Data.Repository.ProviderRepository(_courseDeliveryDataContext.Object, Mock.Of<ICourseDeliveryReadonlyDataContext>());
        }

        [Test]
        public async Task Then_The_Provider_Records_Are_Updated()
        {
            await _providerRepository.UpdateAddressesFromImportTable();
            
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Exactly(2));
        }
    }
}