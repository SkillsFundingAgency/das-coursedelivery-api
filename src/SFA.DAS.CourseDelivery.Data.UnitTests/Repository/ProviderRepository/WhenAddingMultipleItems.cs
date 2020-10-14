using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRepository
{
    public class WhenAddingMultipleItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderRepository _providerRepository;
        private List<Provider> _providers;

        [SetUp]
        public void Arrange()
        {
            _providers = new List<Provider>
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
            _courseDeliveryDataContext.Setup(x => x.Providers).ReturnsDbSet(new List<Provider>());
            _providerRepository = new Data.Repository.ProviderRepository(_courseDeliveryDataContext.Object, Mock.Of<ICourseDeliveryReadonlyDataContext>());
        }

        [Test]
        public async Task Then_The_Providers_Are_Added_From_The_Import_Table()
        {
            //Act
            await _providerRepository.InsertFromImportTable();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>
                x.ExecuteRawSql(@"INSERT INTO dbo.Provider SELECT * FROM dbo.Provider_Import"), Times.Once);
        }
    }
}