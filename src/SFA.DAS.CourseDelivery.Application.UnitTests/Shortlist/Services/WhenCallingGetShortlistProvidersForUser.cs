using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Services;
using SFA.DAS.CourseDelivery.Domain.Extensions;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Services
{
    public class WhenCallingGetShortlistProvidersForUser
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Shortlist_From_The_Repository_With_Providers(
            Guid userId,
            List<Domain.Entities.ShortlistProviderWithStandardAndLocation> shortlistFromRepository,
            [Frozen]Mock<IShortlistRepository> mockRepository,
            ShortlistService service)
        {
            //Arrange
            mockRepository
                .Setup(repository => repository.GetShortListForUser(userId))
                .ReturnsAsync(shortlistFromRepository);

            //Act
            var actual = await service.GetAllForUserWithProviders(userId);
            
            //Assert
            var expected = shortlistFromRepository.BuildShortlistProviderLocation();
            actual.ToList().Should().BeEquivalentTo(expected);
        }
    }
}