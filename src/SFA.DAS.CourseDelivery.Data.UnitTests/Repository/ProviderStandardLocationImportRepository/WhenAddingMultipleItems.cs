using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardLocationImportRepository
{
    public class WhenAddingMultipleItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderStandardLocationImportRepository _providerStandardLocationImportRepository;
        private List<ProviderStandardLocationImport> _providerStandardLocationImports;

        [SetUp]
        public void Arrange()
        {
            _providerStandardLocationImports = new List<ProviderStandardLocationImport>
            {
                new ProviderStandardLocationImport
                {
                    StandardId = 1,
                    Ukprn = 123
                },
                new ProviderStandardLocationImport
                {
                    StandardId = 2,
                    Ukprn = 123
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandardLocationImports).ReturnsDbSet(new List<ProviderStandardLocationImport>());
            _providerStandardLocationImportRepository = new Data.Repository.ProviderStandardLocationImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_ProviderStandardLocationImport_Items_Are_Added()
        {
            //Act
            await _providerStandardLocationImportRepository.InsertMany(_providerStandardLocationImports);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.ProviderStandardLocationImports.AddRangeAsync(_providerStandardLocationImports, It.IsAny<CancellationToken>()), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}