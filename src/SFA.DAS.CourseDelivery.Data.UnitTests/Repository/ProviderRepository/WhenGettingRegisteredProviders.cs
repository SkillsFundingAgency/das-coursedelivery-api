using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using J2N.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using Provider = SFA.DAS.CourseDelivery.Domain.Entities.Provider;
using ProviderRegistration = SFA.DAS.CourseDelivery.Domain.Entities.ProviderRegistration;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRepository
{
    public class WhenGettingRegisteredProviders
    {
        private Mock<ICourseDeliveryReadonlyDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderRepository _providerRepository;
        private List<Provider> _providers;
        private Provider _expectedProvider;
        private const int ExpectedUkprn = 2;

        [SetUp]
        public void Arrange()
        {
            _expectedProvider = new Provider
            {
                Ukprn = ExpectedUkprn,
                ProviderRegistration = new ProviderRegistration
                {
                    StatusId = RoatpTypeConstants.StatusOfActive, 
                    ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider
                }
            };
            _providers = new List<Provider>
            {
                new Provider
                {
                    Ukprn = 123,
                    ProviderRegistration = new ProviderRegistration
                    {
                        StatusId = 0, 
                        ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider
                    }
                },
                new Provider
                {
                    Ukprn = 1234,
                    ProviderRegistration = new ProviderRegistration
                    {
                        StatusId = RoatpTypeConstants.StatusOfActive, 
                        ProviderTypeId = 0
                    }
                },
                _expectedProvider
            };
            
            
            _courseDeliveryDataContext = new Mock<ICourseDeliveryReadonlyDataContext>();
            _courseDeliveryDataContext.Setup(x => x.Providers).ReturnsDbSet(_providers);
            
            _providerRepository = new Data.Repository.ProviderRepository(Mock.Of<ICourseDeliveryDataContext>(), _courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_Only_Main_And_Active_Providers_Are_Returned()
        {
            var actual = await _providerRepository.GetAllRegistered();

            actual.Should().HaveCount(1);
            actual.First().Should().BeEquivalentTo(_expectedProvider);
        }
    }
}