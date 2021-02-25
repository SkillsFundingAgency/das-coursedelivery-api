using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Services;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Services
{
    public class WhenGettingShortlistItemCount
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Number_Of_Items_Returned_For_User(
            Guid userId,
            int returnValue,
            [Frozen]Mock<IShortlistRepository> repository,
            ShortlistService service)
        {
            //Arrange
            repository.Setup(x => x.GetShortlistItemCountForUser(userId)).ReturnsAsync(returnValue);
            
            //Act
            var actual = await service.GetShortlistItemCountForUser(userId);
            
            //Assert
            actual.Should().Be(returnValue);
        }
    }
}