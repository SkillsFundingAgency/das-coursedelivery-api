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
            int courseId,
            int providerUkprn,
            Guid shortlistUserId,
            float? lat,
            float? lon,
            List<Shortlist> recordsInDb,
            [Frozen] Mock<ICourseDeliveryReadonlyDataContext> mockContext,
            Data.Repository.ShortlistRepository repository)
        {
            //Arrange
            recordsInDb = recordsInDb.Select(c =>
            {
                c.ShortlistUserId = shortlistUserId;
                return c;
            }).ToList();
            recordsInDb[0].CourseId = courseId;
            recordsInDb[0].ProviderUkprn = providerUkprn;
            recordsInDb[0].Lat = lat;
            recordsInDb[0].Long = lon;
            mockContext
                .Setup(context => context.Shortlists)
                .ReturnsDbSet(recordsInDb);

            var actual = await repository.GetShortlistUserItem(shortlistUserId,courseId,providerUkprn,lat,lon);

            actual.Should()
                .BeEquivalentTo(recordsInDb.First(shortlist =>
                    shortlist.ShortlistUserId == shortlistUserId 
                    && shortlist.ProviderUkprn.Equals(providerUkprn)
                    && shortlist.CourseId.Equals(courseId)
                    && shortlist.Lat.Equals(lat)
                    && shortlist.Long.Equals(lon)
                    ));
        }
    }
}