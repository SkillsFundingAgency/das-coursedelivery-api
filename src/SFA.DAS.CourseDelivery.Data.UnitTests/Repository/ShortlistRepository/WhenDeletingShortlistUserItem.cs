using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ShortlistRepository
{
    public class WhenDeletingShortlistUserItem
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ShortlistRepository _providerImportRepository;
        private List<Shortlist> _providers;
        private Guid _shortlistUserId;
        private Guid _id;

        [SetUp]
        public void Arrange()
        {
            _shortlistUserId = Guid.NewGuid();
            _id = Guid.NewGuid();
            _providers = new List<Shortlist>
            {
                new Shortlist
                {
                    Id = _id,
                    ShortlistUserId = _shortlistUserId
                },
                new Shortlist
                {
                    Id = Guid.NewGuid(),
                    ShortlistUserId = _shortlistUserId
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.Shortlists).ReturnsDbSet(_providers);
            _providerImportRepository = new Data.Repository.ShortlistRepository(Mock.Of<ILogger<Data.Repository.ShortlistRepository>>(), _courseDeliveryDataContext.Object, Mock.Of<ICourseDeliveryReadonlyDataContext>());
        }

        [Test]
        public void Then_The_Shortlist_Item_Is_Removed()
        {
            //Act
            _providerImportRepository.Delete(_id, _shortlistUserId);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.Shortlists
                .Remove(It.Is<Shortlist>(c=>
                    c.Id.Equals(_id))), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }

        [Test]
        public void Then_Is_Not_Removed_If_ShortlistUserId_Does_Not_Match()
        {
            //Act
            _providerImportRepository.Delete(_id,Guid.NewGuid());
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.Shortlists
                .Remove(It.IsAny<Shortlist>()), Times.Never);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Never);
        }
    }
}