using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WhenCallingGetAllForUser
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Shortlist_From_The_Repository(
            Guid userId,
            List<Domain.Entities.Shortlist> shortlistFromRepository,
            [Frozen]Mock<IShortlistRepository> mockRepository,
            ShortlistService service)
        {
            //Arrange
            mockRepository
                .Setup(repository => repository.GetAllForUser(userId))
                .ReturnsAsync(shortlistFromRepository);

            //Act
            var actual = await service.GetAllForUser(userId);
            
            //Assert
            actual.Should().BeEquivalentTo(shortlistFromRepository.Select(shortlist => (Domain.Models.Shortlist)shortlist));
        }
    }
}