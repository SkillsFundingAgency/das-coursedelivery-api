using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.DeleteShortlistForUser;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Commands
{
    public class WhenHandlingDeleteShortlistForUser
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called_For_That_User_Id(
            DeleteShortlistForUserCommand command,
            [Frozen] Mock<IShortlistService> service,
            DeleteShortlistForUserCommandHandler handler)
        {
            //Act
            await handler.Handle(command, CancellationToken.None);
            
            //Assert
            service.Verify(x=>x.DeleteShortlist(command.ShortlistUserId), Times.Once);
        }
    }
}