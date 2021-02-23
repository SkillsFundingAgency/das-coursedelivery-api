using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationRepository
{
    public class WhenUpdatingAddresses
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderRegistrationRepository _providerRegistrationRepository;
        private List<ProviderRegistrationImport> _providerRegistrationImports;
        private ProviderRegistration _expectedProviderRegistration;
        private const double ExpectedLat = 2.1;
        private const double ExpectedLong = -2.1;
        private const string ExpectedPostcode = "TE 3T";

        [SetUp]
        public void Arrange()
        {
            _expectedProviderRegistration = new ProviderRegistration();
            _providerRegistrationImports = new List<ProviderRegistrationImport>
            {
                new ProviderRegistrationImport
                {
                    Ukprn = 123,
                    Lat = ExpectedLat,
                    Long = ExpectedLong,
                    Postcode = ExpectedPostcode
                },
                new ProviderRegistrationImport
                {
                    Ukprn = 1234,
                    Lat = ExpectedLat,
                    Long = ExpectedLong,
                    Postcode = ExpectedPostcode
                },
                new ProviderRegistrationImport
                {
                    Ukprn = 12345,
                    Lat = 0,
                    Long = 0,
                    Postcode = string.Empty
                }
            };
            
            
            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderRegistrationImports).ReturnsDbSet(_providerRegistrationImports);
            _courseDeliveryDataContext.Setup(x => x.ProviderRegistrations.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(_expectedProviderRegistration);
            
            _providerRegistrationRepository = new Data.Repository.ProviderRegistrationRepository(_courseDeliveryDataContext.Object, Mock.Of<ICourseDeliveryReadonlyDataContext>());
        }

        [Test]
        public async Task Then_The_Provider_Records_Are_Updated()
        {
            await _providerRegistrationRepository.UpdateAddressesFromImportTable();
            
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Exactly(1));
            _courseDeliveryDataContext.Verify(x=>x.ProviderRegistrations.FindAsync(It.IsAny<int>()), Times.Exactly(3));
        }
    }
}