using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardRepository
{
    public class WhenGettingCoursesByStandardIdAndUkprn
    {
        private Mock<ICourseDeliveryReadonlyDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderStandardRepository _providerStandardImportRepository;
        private List<ProviderStandard> _providerStandards;
        private List<int> _expectedCourses;
        private const int StandardId = 20;
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
                        Ukprn = 123
                    }

                },
                new ProviderStandard
                {
                    Ukprn = Ukprn,
                    StandardId = StandardId,
                    Provider = new Provider
                    {
                        Ukprn = Ukprn,
                        Name = "Second"
                    },
                    ProviderStandardLocation = new List<ProviderStandardLocation>
                    {
                        new ProviderStandardLocation
                        {
                            Ukprn = Ukprn
                        }
                    }
                },
                new ProviderStandard
                {
                    Ukprn = Ukprn,
                    StandardId = 2,
                    Provider = new Provider
                    {
                        Ukprn = Ukprn,
                        Name = "Second"
                    },
                    ProviderStandardLocation = new List<ProviderStandardLocation>()
                },
                new ProviderStandard
                {
                    Ukprn = Ukprn,
                    StandardId = 3,
                    Provider = new Provider
                    {
                        Ukprn = 1233,
                        Name="First"
                    }
                }
            };

            _expectedCourses = new List<int> {StandardId};

            _courseDeliveryDataContext = new Mock<ICourseDeliveryReadonlyDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandards).ReturnsDbSet(_providerStandards);

            _providerStandardImportRepository = new Data.Repository.ProviderStandardRepository(Mock.Of<ICourseDeliveryDataContext>(), _courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_Courses_Items_Are_Returned_By_StandardId_That_Have_A_Delivery_Location()
        {
            //Act
            var actual = await _providerStandardImportRepository.GetCoursesByUkprn(Ukprn);

            //Assert
            Assert.IsNotNull(actual);

            actual.Should().BeEquivalentTo(_expectedCourses);
        }
    }
}