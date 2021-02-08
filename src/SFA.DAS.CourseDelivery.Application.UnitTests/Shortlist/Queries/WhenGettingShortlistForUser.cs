using System.Collections.Generic;
using System.Linq;
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
            [Frozen] Mock<IProviderService> mockProviderService,
            GetShortlistForUserQueryHandler handler)
        {
            //Arrange
            mockShortlistService
                .Setup(x => x.GetAllForUser(query.UserId))
                .ReturnsAsync(shortlistFromService);
            foreach (var shortlistItem in shortlistFromService)
            {
                mockProviderService
                    .Setup(service => service.GetProviderByUkprnAndStandard(
                        shortlistItem.ProviderUkprn, 
                        shortlistItem.CourseId, 
                        shortlistItem.Lat,
                        shortlistItem.Long,
                        shortlistItem.CourseSector))
                    .ReturnsAsync(providerLocationFromService);
            }

            //Act
            var actual = await handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            var actualItems = actual.ShortlistItems.ToList();
            for (var i = 0; i < actualItems.Count; i++)
            {
                actualItems[i].Id.Should().Be(shortlistFromService[i].Id);
                actualItems[i].CourseId.Should().Be(shortlistFromService[i].CourseId);
                actualItems[i].LocationDescription.Should().Be(shortlistFromService[i].LocationDescription);
                actualItems[i].ProviderLocation.Should().Be(providerLocationFromService);
            }
        }
    }
}