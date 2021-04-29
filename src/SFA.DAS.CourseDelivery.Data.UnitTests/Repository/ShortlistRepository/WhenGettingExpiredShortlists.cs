using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ShortlistRepository
{
    public class WhenGettingExpiredShortlists
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Expired_ShortlistUserIds_Are_Returned(
            uint expiryPeriod,
            Guid firstUserId,
            Guid secondUserId,
            Guid thirdUserId,
            List<Shortlist> firstUser,
            List<Shortlist> secondUser,
            List<Shortlist> thirdUser,
            [Frozen] Mock<ICourseDeliveryReadonlyDataContext> mockContext,
            Data.Repository.ShortlistRepository repository)
        {
            //Arrange
            firstUser = firstUser.Select(c =>
            {
                c.ShortlistUserId = firstUserId;
                c.CreatedDate = DateTime.UtcNow;
                return c;
            }).ToList();
            secondUser = secondUser.Select(c =>
            {
                c.ShortlistUserId = secondUserId;
                c.CreatedDate = DateTime.UtcNow.AddDays(-expiryPeriod);
                return c;
            }).ToList();
            secondUser[0].CreatedDate = DateTime.UtcNow.AddDays(1);
            thirdUser = thirdUser.Select(c =>
            {
                c.ShortlistUserId = thirdUserId;
                c.CreatedDate = DateTime.UtcNow.AddDays(-expiryPeriod);
                return c;
            }).ToList();
            var records = new List<Shortlist>();
            records.AddRange(firstUser);
            records.AddRange(secondUser);
            records.AddRange(thirdUser);
            mockContext
                .Setup(context => context.Shortlists)
                .ReturnsDbSet(records);

            //Act
            var actual = (await repository.GetExpiredShortListUserIds(expiryPeriod-1)).ToList();

            //Assert
            actual.Count.Should().Be(1);
            actual.First().Should().Be(thirdUserId);
        }

        [Test, RecursiveMoqAutoData]
        public async Task And_If_No_Expired_Returns_Empty_List(
            uint expiryPeriod,
            Guid firstUserId,
            List<Shortlist> firstUser,
            [Frozen] Mock<ICourseDeliveryReadonlyDataContext> mockContext,
            Data.Repository.ShortlistRepository repository)
        {
            //Arrange
            firstUser = firstUser.Select(c =>
            {
                c.ShortlistUserId = firstUserId;
                c.CreatedDate = DateTime.UtcNow;
                return c;
            }).ToList();
            mockContext
                .Setup(context => context.Shortlists)
                .ReturnsDbSet(firstUser);
            
            //Act
            var actual = (await repository.GetExpiredShortListUserIds(expiryPeriod)).ToList();
            
            //Assert
            actual.Any().Should().BeFalse();
        }
    }
}