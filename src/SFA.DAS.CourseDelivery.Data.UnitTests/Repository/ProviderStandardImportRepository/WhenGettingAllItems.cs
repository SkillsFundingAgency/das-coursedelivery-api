using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardImportRepository
{
    public class WhenGettingAllItems
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
        public async Task Then_The_ProviderStandardImport_Items_Are_Returned()
        {
            //Act
            var actual = await _providerStandardImportRepository.GetAll();
            
            //Assert
            Assert.IsNotNull(actual);
            actual.Should().BeEquivalentTo(_providerStandardImports);
        }
    }
}