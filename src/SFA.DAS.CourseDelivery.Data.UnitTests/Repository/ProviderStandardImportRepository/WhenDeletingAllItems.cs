using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardImportRepository
{
    public class WhenDeletingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.Import.ProviderStandardImportRepository _providerStandardImportRepository;
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
            _courseDeliveryDataContext.Setup(x => x.ProviderStandardImports).ReturnsDbSet(_providerStandardImports);
            _providerStandardImportRepository = new Data.Repository.Import.ProviderStandardImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public void Then_The_ProviderStandardImports_Are_Removed()
        {
            //Act
            _providerStandardImportRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.ProviderStandardImports.RemoveRange(_courseDeliveryDataContext.Object.ProviderStandardImports), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}