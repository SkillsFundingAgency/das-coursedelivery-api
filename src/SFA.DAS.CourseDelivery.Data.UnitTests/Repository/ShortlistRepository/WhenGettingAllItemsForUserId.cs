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
    public class WhenGettingAllItemsForUserId
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_From_DbContext(
            Guid userId,
            List<Shortlist> recordsInDb,
            [Frozen] Mock<ICourseDeliveryReadonlyDataContext> mockContext,
            Data.Repository.ShortlistRepository repository)
        {
            recordsInDb[0].ShortlistUserId = userId;
            mockContext
                .Setup(context => context.Shortlists)
                .ReturnsDbSet(recordsInDb);

            var actual = await repository.GetAllForUser(userId);

            actual.Should()
                .BeEquivalentTo(recordsInDb.Where(shortlist =>
                    shortlist.ShortlistUserId == userId));
        }
    }
}