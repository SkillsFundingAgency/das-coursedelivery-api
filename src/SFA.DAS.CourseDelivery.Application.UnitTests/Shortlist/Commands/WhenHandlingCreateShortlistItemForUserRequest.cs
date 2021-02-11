using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.CreateShortlistItemForUser;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using ValidationResult = SFA.DAS.CourseDelivery.Domain.Validation.ValidationResult;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Commands
{
    public class WhenHandlingCreateShortlistItemForUserRequest
    {
        [Test, MoqAutoData]
        public void Then_The_Request_Is_Validated_And_If_Not_Valid_Exception_Thrown(
            CreateShortlistItemForUserRequest request,
            [Frozen]Mock<IValidator<CreateShortlistItemForUserRequest>> validator,
            CreateShortlistItemForUserRequestHandler handler)
        {
            //Arrange
            validator
                .Setup(x => x.ValidateAsync(request))
                .ReturnsAsync(new ValidationResult {ValidationDictionary = { {"Error", "Some error"}}});
            
            //Act / Assert
            Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
        }

        [Test, MoqAutoData]
        public async Task Then_If_The_Request_Is_Valid_The_Service_Is_Called(
            CreateShortlistItemForUserRequest request,
            [Frozen]Mock<IValidator<CreateShortlistItemForUserRequest>> validator,
            [Frozen]Mock<IShortlistService> service,
            CreateShortlistItemForUserRequestHandler handler)
        {
            //Arrange
            validator
                .Setup(x => x.ValidateAsync(request))
                .ReturnsAsync(new ValidationResult( ));

            //Act
            await handler.Handle(request, CancellationToken.None);

            //Assert
            service.Verify(x=>x.CreateShortlistItem(It.Is<Domain.Entities.Shortlist>( c=>
                c.StandardId.Equals(request.StandardId)
                && c.Lat.Equals(request.Lat)
                && c.Long.Equals(request.Lon)
                && c.CourseSector.Equals(request.SectorSubjectArea)
                && c.LocationDescription.Equals(request.LocationDescription)
                && c.Ukprn.Equals(request.Ukprn)
                && c.ShortlistUserId.Equals(request.ShortlistUserId)
                && c.Id != Guid.Empty
                )), Times.Once);
        }
    }
}