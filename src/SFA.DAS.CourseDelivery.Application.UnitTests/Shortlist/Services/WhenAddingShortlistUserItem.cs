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
            //Act
            await service.CreateShortlistItem(item);
            
            //Assert
            repository.Verify(x=>x.Insert(item), Times.Once);
        }
    }
}