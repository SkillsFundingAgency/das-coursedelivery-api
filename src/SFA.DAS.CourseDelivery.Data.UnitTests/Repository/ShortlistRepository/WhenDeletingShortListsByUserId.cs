using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ShortlistRepository
{
    public class WhenDeletingShortListsByUserId
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_All_Records_Are_Deleted_Matching_User_Id(
            Guid firstUserId,
            List<Shortlist> firstUser,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ShortlistRepository repository)
        {
            //Arrange
            firstUser = firstUser.Select(c =>
            {
                c.ShortlistUserId = firstUserId;
                return c;
            }).ToList();
            mockContext
                .Setup(context => context.Shortlists)
                .ReturnsDbSet(firstUser);
            
            //Act
            await repository.DeleteShortlistByUserId(firstUserId);
            
            //Assert
            mockContext.Verify(x=>x.Shortlists
                .Remove(It.Is<Shortlist>(c=>
                    c.ShortlistUserId.Equals(firstUserId))), Times.Exactly(firstUser.Count));
            mockContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}