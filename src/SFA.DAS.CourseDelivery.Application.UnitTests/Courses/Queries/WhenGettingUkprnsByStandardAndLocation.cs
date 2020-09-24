using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.CourseDelivery.Domain.Validation.ValidationResult;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Queries
{
    public class WhenGettingUkprnsByStandardAndLocation
    {
        [Test, MoqAutoData]
        public void Then_If_Not_Valid_An_Exception_Is_Thrown(
            List<int> ukprns,
            GetUkprnsQuery query,
            [Frozen] Mock<IValidator<GetUkprnsQuery>> validator,
            [Frozen] Mock<IProviderService> service,
            GetUkprnsQueryHandler handler)
        {
            //Arrange
            validator.Setup(x => x.ValidateAsync(query)).ReturnsAsync(new ValidationResult{ValidationDictionary = { {"",""}}});
            
            //Act
            Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Test, MoqAutoData]
        public async Task Then_If_Valid_Calls_Repository_And_Returns_Data(
            UkprnsForStandard ukprns,
            GetUkprnsQuery query,
            [Frozen] Mock<IValidator<GetUkprnsQuery>> validator,
            [Frozen] Mock<IProviderService> service,
            GetUkprnsQueryHandler handler)
        {
            //Arrange
            validator.Setup(x => x.ValidateAsync(query)).ReturnsAsync(new ValidationResult());
            service.Setup(x => x.GetUkprnsForStandardAndLocation(query.StandardId, query.Lat, query.Lon))
                .ReturnsAsync(ukprns);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            
            //Assert
            actual.UkprnsByStandardAndLocation.Should().BeEquivalentTo(ukprns.UkprnsFilteredByStandardAndLocation);
            actual.UkprnsByStandard.Should().BeEquivalentTo(ukprns.UkprnsFilteredByStandard);
        }
    }
}