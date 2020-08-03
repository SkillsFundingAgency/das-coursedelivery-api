using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardImportRepository
{
    public class WhenAddingMultipleItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderStandardImportRepository _providerStandardImportRepository;
        private List<ProviderStandardImport> _providerStandardImports;

        [SetUp]
        public void Arrange()
        {
            _providerStandardImports = new List<ProviderStandardImport>
            {
                new ProviderStandardImport
                {
                    StandardId = 1,
                    Ukprn = 123
                },
                new ProviderStandardImport
                {
                    StandardId = 2,
                    Ukprn = 123
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandardImports).ReturnsDbSet(new List<ProviderStandardImport>());
            _providerStandardImportRepository = new Data.Repository.ProviderStandardImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_ProviderStandardImport_Items_Are_Added()
        {
            //Act
            await _providerStandardImportRepository.InsertMany(_providerStandardImports);
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.ProviderStandardImports.AddRangeAsync(_providerStandardImports, It.IsAny<CancellationToken>()), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}