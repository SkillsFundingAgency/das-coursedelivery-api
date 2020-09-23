using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Queries
{
    public class WhenValidatingGetUkprnsQuery
    {
        [Test, AutoData]
        public async Task Then_The_Request_Is_Valid_If_All_Params_Supplied(
            int standardId, 
            double lat, 
            double lon,
            GetUkprnsQueryValidator queryValidator
            )
        {
            //Arrange
            var query = new GetUkprnsQuery
            {
                StandardId = standardId,
                Lat = lat,
                Lon = lon
            };
            
            //Act
            var actual = await queryValidator.ValidateAsync(query);
            
            //Assert
            actual.IsValid().Should().BeTrue();
        }

        [Test, AutoData]
        public async Task Then_Invalid_If_StandardId_Is_Zero(
            double lat, 
            double lon,
            GetUkprnsQueryValidator queryValidator)
        {
            //Arrange
            var query = new GetUkprnsQuery
            {
                StandardId = 0,
                Lat = lat,
                Lon = lon
            };
            
            //Act
            var actual = await queryValidator.ValidateAsync(query);
            
            //Assert
            actual.IsValid().Should().BeFalse();
        }
    }
}