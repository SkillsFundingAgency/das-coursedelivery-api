using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRepository
{
    public class WhenDeletingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderRepository _providerImportRepository;
        private List<Provider> _providerImports;

        [SetUp]
        public void Arrange()
        {
            _providerImports = new List<Provider>
            {
                new Provider
                {
                    Id = 1,
                    Name ="Test"
                },
                new Provider
                {
                    Id = 1,
                    Name= "Test 2"
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.Providers).ReturnsDbSet(_providerImports);
            _providerImportRepository = new Data.Repository.ProviderRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public void Then_The_Providers_Are_Removed()
        {
            //Act
            _providerImportRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.Providers.RemoveRange(_courseDeliveryDataContext.Object.Providers), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}