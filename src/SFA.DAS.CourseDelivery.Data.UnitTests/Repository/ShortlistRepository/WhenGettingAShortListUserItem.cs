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
    public class WhenGettingAShortListUserItem
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Item_Is_Retrieved_By_Unique_Key(
            Shortlist item,
            List<Shortlist> recordsInDb,
            [Frozen] Mock<ICourseDeliveryReadonlyDataContext> mockContext,
            Data.Repository.ShortlistRepository repository)
        {
            //Arrange
            recordsInDb = recordsInDb.Select(c =>
            {
                c.ShortlistUserId = item.ShortlistUserId;
                return c;
            }).ToList();
            recordsInDb[0].StandardId = item.StandardId;
            recordsInDb[0].Ukprn = item.Ukprn;
            recordsInDb[0].Lat = item.Lat;
            recordsInDb[0].Long = item.Long;
            mockContext
                .Setup(context => context.Shortlists)
                .ReturnsDbSet(recordsInDb);

            //Act
            var actual = await repository.GetShortlistUserItem(item);

            //Assert
            actual.Should()
                .BeEquivalentTo(recordsInDb.First(shortlist =>
                    shortlist.ShortlistUserId == item.ShortlistUserId 
                    && shortlist.Ukprn.Equals(item.Ukprn)
                    && shortlist.StandardId.Equals(item.StandardId)
                    && shortlist.Lat.Equals(item.Lat)
                    && shortlist.Long.Equals(item.Long)
                    ));
        }
    }
}