using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderImportRepository
{
    public class WhenGettingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderImportRepository _providerImportRepository;
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
            _courseDeliveryDataContext.Setup(x => x.ProviderImports).ReturnsDbSet(_providerImports);
            
            _providerImportRepository = new Data.Repository.ProviderImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_ProviderImport_Items_Are_Returned()
        {
            //Act
            var actual = await _providerImportRepository.GetAll();
            
            //Assert
            Assert.IsNotNull(actual);
            actual.Should().BeEquivalentTo(_providerImports);
        }
    }
}