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
    public class WhenDeletingShortlistUserItem
    {
        [Test, RecursiveMoqAutoData]
        public void Then_Deletes_The_Shortlist_Item_From_The_Repository(
            Guid id,
            Guid userId,
            [Frozen]Mock<IShortlistRepository> mockRepository,
            ShortlistService service)
        {
            //Act
            service.DeleteShortlistUserItem(id,userId);
            
            //Assert
            mockRepository.Verify(x=>x.Delete(id,userId), Times.Once);
        }
    }
}