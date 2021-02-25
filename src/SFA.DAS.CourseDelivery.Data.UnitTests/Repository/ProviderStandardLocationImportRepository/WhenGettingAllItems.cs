using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardLocationImportRepository
{
    public class WhenGettingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.Import.ProviderStandardLocationImportRepository _providerStandardLocationImportRepository;
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
            _providerStandardLocationImportRepository = new Data.Repository.Import.ProviderStandardLocationImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_ProviderStandardLocationImport_Items_Are_Returned()
        {
            //Act
            var actual = await _providerStandardLocationImportRepository.GetAll();
            
            //Assert
            Assert.IsNotNull(actual);
            actual.Should().BeEquivalentTo(_providerStandardLocationImports);
        }
    }
}