using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardLocationImportRepository
{
    public class WhenDeletingAllItems
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
            _courseDeliveryDataContext.Setup(x => x.ProviderStandardLocationImports).ReturnsDbSet(_providerStandardLocationImports);
            _providerStandardLocationImportRepository = new Data.Repository.ProviderStandardLocationImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public void Then_The_ProviderStandardLocationImports_Are_Removed()
        {
            //Act
            _providerStandardLocationImportRepository.DeleteAll();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>x.ProviderStandardLocationImports.RemoveRange(_courseDeliveryDataContext.Object.ProviderStandardLocationImports), Times.Once);
            _courseDeliveryDataContext.Verify(x=>x.SaveChanges(), Times.Once);
        }
    }
}