using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ShortlistRepository
{
    public class WhenAddingAShortlistItem
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ShortlistRepository _providerImportRepository;
        private Shortlist _shortlistItem;

        [SetUp]
        public void Arrange()
        {
            _shortlistItem = 
                new Shortlist
                {
                    Id = Guid.NewGuid(),
                    ShortlistUserId = Guid.NewGuid()
                };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.Shortlists).ReturnsDbSet(new List<Shortlist>());
            _providerImportRepository = new Data.Repository.ShortlistRepository(_courseDeliveryDataContext.Object, Mock.Of<ICourseDeliveryReadonlyDataContext>());
        }

        [Test]
        public async Task Then_The_ShortList_Item_Is_Added()
        {
            //Act
            await _providerImportRepository.Insert(_shortlistItem);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.Shortlists.AddAsync(_shortlistItem, It.IsAny<CancellationToken>()), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}