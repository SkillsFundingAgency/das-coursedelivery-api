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
    public class WhenAddingShortlistUserItem
    {
        [Test, MoqAutoData]
        public async Task Then_The_Item_Is_Added_To_The_Repository(
            Domain.Entities.Shortlist item,
            [Frozen] Mock<IShortlistRepository> repository,
            ShortlistService service)
        {
            //Arrange
            repository.Setup(x =>
                    x.GetShortlistUserItem(It.IsAny<Domain.Entities.Shortlist>()))
                .ReturnsAsync((Domain.Entities.Shortlist)null);
            //Act
            await service.CreateShortlistItem(item);
            
            //Assert
            repository.Verify(x=>x.Insert(item), Times.Once);
        }

        [Test, MoqAutoData]
        public async Task Then_If_There_Already_Is_A_Matching_Item_On_Course_And_Provider_And_Location_It_Is_Not_Added(
            Domain.Entities.Shortlist item,
            [Frozen] Mock<IShortlistRepository> repository,
            ShortlistService service)
        {
            //Arrange
            repository.Setup(x =>
                    x.GetShortlistUserItem(item))
                .ReturnsAsync(item);
                
            
            //Act
            await service.CreateShortlistItem(item);
            
            //Assert
            repository.Verify(x=>x.Insert(item), Times.Never);
        }
    }
}