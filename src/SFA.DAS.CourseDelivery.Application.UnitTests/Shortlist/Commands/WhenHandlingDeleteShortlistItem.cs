using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.DeleteShortlistItemForUser;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Commands
{
    public class WhenHandlingDeleteShortlistItem
    {
        [Test, MoqAutoData]
        public async Task Then_If_The_Request_Is_Valid_The_Service_Is_Called(
            DeleteShortlistItemForUserRequest request,
            [Frozen]Mock<IShortlistService> service,
            DeleteShortlistItemForUserRequestHandler handler)
        {
            //Act
            await handler.Handle(request, CancellationToken.None);

            //Assert
            service.Verify(x=>x.DeleteShortlistUserItem(request.Id, request.ShortlistUserId), Times.Once);
        }
    }
}