using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderImportRepository
{
    public class WhenAddingMultipleItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.Import.ProviderImportRepository _providerImportRepository;
        private List<ProviderImport> _providerImports;

        [SetUp]
        public void Arrange()
        {
            _providerImports = new List<ProviderImport>
            {
                new ProviderImport
                {
                    Id = 1,
                    Name ="Test"
                },
                new ProviderImport
                {
                    Id = 1,
                    Name= "Test 2"
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderImports).ReturnsDbSet(new List<ProviderImport>());
            _providerImportRepository = new Data.Repository.Import.ProviderImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_ProviderImport_Items_Are_Added()
        {
            //Act
            await _providerImportRepository.InsertMany(_providerImports);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.ProviderImports.AddRangeAsync(_providerImports, It.IsAny<CancellationToken>()), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}