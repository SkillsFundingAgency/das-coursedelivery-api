using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardRepository
{
    public class WhenDeletingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderStandardRepository _providerStandardRepository;
        private List<ProviderStandard> _providerStandards;

        [SetUp]
        public void Arrange()
        {
            _providerStandards = new List<ProviderStandard>
            {
                new ProviderStandard
                {
                    StandardId = 1,
                    Ukprn = 123
                },
                new ProviderStandard
                {
                    StandardId = 2,
                    Ukprn = 123
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandards).ReturnsDbSet(_providerStandards);
            _providerStandardRepository = new Data.Repository.ProviderStandardRepository(_courseDeliveryDataContext.Object, Mock.Of<ICourseDeliveryReadonlyDataContext>());
        }

        [Test]
        public void Then_The_ProviderStandards_Are_Removed()
        {
            //Act
            _providerStandardRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.ProviderStandards
                .RemoveRange(It.Is<List<ProviderStandard>>(c=>
                    c.ToList().Count.Equals(_providerStandards.Count))), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}