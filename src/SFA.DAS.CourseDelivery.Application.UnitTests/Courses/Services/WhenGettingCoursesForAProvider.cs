using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Services;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Services
{
    public class WhenGettingCoursesForAProvider
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Courses_For_A_Provider_From_The_Repository(
            int ukprn,
            List<int> standardIds,
            [Frozen] Mock<IProviderStandardRepository> repository,
            ProviderService service)
        {
            //Arrange
            repository.Setup(x => x.GetCoursesByUkprn(ukprn)).ReturnsAsync(standardIds);

            //Act
            var actual = await service.GetStandardIdsByUkprn(ukprn);

            //Assert
            repository.Verify(x => x.GetCoursesByUkprn(ukprn), Times.Once);
            actual.Should().BeEquivalentTo(standardIds);
        }
    }
}