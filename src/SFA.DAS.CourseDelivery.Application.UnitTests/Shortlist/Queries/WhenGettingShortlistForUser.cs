using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Queries.GetShortlistForUser;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Queries
{
    public class WhenGettingShortlistForUser
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Shortlist_From_The_Services(
            GetShortlistForUserQuery query,
            List<Domain.Models.Shortlist> shortlistFromService,
            ProviderLocation providerLocationFromService,
            [Frozen] Mock<IShortlistService> mockShortlistService,
            GetShortlistForUserQueryHandler handler)
        {
            //Arrange
            mockShortlistService
                .Setup(x => x.GetAllForUserWithProviders(query.UserId))
                .ReturnsAsync(shortlistFromService);
            
            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            actual.Shortlist.Should().BeEquivalentTo(shortlistFromService);
        }
    }
}