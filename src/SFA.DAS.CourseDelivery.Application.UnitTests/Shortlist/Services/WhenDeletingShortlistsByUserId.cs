using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Services;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Services
{
    public class WhenDeletingShortlistsByUserId
    {
        [Test, MoqAutoData]
        public async Task Then_The_Repository_Is_Called(
            Guid userId,
            [Frozen]Mock<IShortlistRepository> repository,
            ShortlistService service)
        {
            //Act
            await service.DeleteShortlist(userId);
            
            //Assert
            repository.Verify(x=>x.DeleteShortlistByUserId(userId), Times.Once);
        }
    }
}