using System;
using System.Collections.Generic;
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
    public class WhenGettingExpiredShortlistUserIds
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called_And_Ids_Returned(
            uint expiryInDays,
            List<Guid> userIds,
            [Frozen]Mock<IShortlistRepository> repository,
            ShortlistService service)
        {
            //Arrange
            repository.Setup(x => x.GetExpiredShortlistUserIds(expiryInDays)).ReturnsAsync(userIds);
            
            //Act
            var actual = await service.GetExpiredShortlistUserIds(expiryInDays);
            
            //Assert
            actual.Should().BeEquivalentTo(userIds);
        }
    }
}