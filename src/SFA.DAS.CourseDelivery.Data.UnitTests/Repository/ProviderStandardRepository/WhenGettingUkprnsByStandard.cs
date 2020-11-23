using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using Provider = SFA.DAS.CourseDelivery.Domain.Entities.Provider;
using ProviderRegistration = SFA.DAS.CourseDelivery.Domain.Entities.ProviderRegistration;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardRepository
{
    public class WhenGettingUkprnsByStandard
    { 
        private Mock<ICourseDeliveryReadonlyDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderStandardRepository _providerStandardImportRepository;
        private List<ProviderStandard> _providerStandards;
        private List<int> _expectedCourses;
        private const int StandardId = 2;
        private const int Ukprn = 12335;

        [SetUp]
        public void Arrange()
        {
            _providerStandards = new List<ProviderStandard>
            {
                new ProviderStandard
                {
                    Ukprn = 123,
                    StandardId = 1,
                    Provider = new Provider
                    {
                        Ukprn = 123,
                        ProviderRegistration = new ProviderRegistration()
                    },
                    ProviderStandardLocation = new List<ProviderStandardLocation>
                    {
                        new ProviderStandardLocation
                        {
                            Ukprn = 1233
                        }
                    }

                },
                new ProviderStandard
                {
                    Ukprn = 12335,
                    StandardId = StandardId,
                    Provider = new Provider
                    {
                        Ukprn = Ukprn,
                        Name = "Second",
                        ProviderRegistration = new ProviderRegistration
                        {
                            ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider,
                            StatusId = RoatpTypeConstants.StatusOfActive,
                            Ukprn =  Ukprn
                        }
                    }
                },
                new ProviderStandard
                {
                    Ukprn = 1233,
                    StandardId = StandardId,
                    Provider = new Provider
                    {
                        Ukprn = 1233,
                        Name="First",
                        ProviderRegistration = new ProviderRegistration()
                    },
                    ProviderStandardLocation = new List<ProviderStandardLocation>
                    {
                        new ProviderStandardLocation
                        {
                            Ukprn = 1233
                        }
                    }
                }
            };

            _expectedCourses = new List<int> {StandardId};

            _courseDeliveryDataContext = new Mock<ICourseDeliveryReadonlyDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandards).ReturnsDbSet(_providerStandards);

            _providerStandardImportRepository = new Data.Repository.ProviderStandardRepository(Mock.Of<ICourseDeliveryDataContext>(), _courseDeliveryDataContext.Object);
        }
        
        [Test]
        public async Task Then_The_Distinct_Ukprns_Are_Returned_That_Have_A_Location_And_Are_Approved()
        {
            //Act
            var actual = await _providerStandardImportRepository.GetUkprnsByStandard(StandardId);

            //Assert
            actual.Should().BeEquivalentTo(_providerStandards.Where(c=>c.StandardId.Equals(StandardId) 
                                                                       && c.ProviderStandardLocation!=null 
                                                                       && c.ProviderStandardLocation.Any()
                                                                       && c.Provider.ProviderRegistration.StatusId == 1
                                                                       && c.Provider.ProviderRegistration.ProviderTypeId == 1)
                .Select(c => c.Ukprn).ToList());
        }
    }
}