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
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
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
                        Ukprn = 123
                    }

                },
                new ProviderStandard
                {
                    Ukprn = 12335,
                    StandardId = StandardId,
                    Provider = new Provider
                    {
                        Ukprn = Ukprn,
                        Name = "Second"
                    }
                },
                new ProviderStandard
                {
                    Ukprn = 1233,
                    StandardId = StandardId,
                    Provider = new Provider
                    {
                        Ukprn = 1233,
                        Name="First"
                    }
                }
            };

            _expectedCourses = new List<int> {StandardId};

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandards).ReturnsDbSet(_providerStandards);

            _providerStandardImportRepository = new Data.Repository.ProviderStandardRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_Courses_Items_Are_Returned_By_StandardId()
        {
            //Act
            var actual = await _providerStandardImportRepository.GetCoursesByUkprn(Ukprn);

            //Assert
            Assert.IsNotNull(actual);

            actual.Should().BeEquivalentTo(_expectedCourses);
        }
    }
}