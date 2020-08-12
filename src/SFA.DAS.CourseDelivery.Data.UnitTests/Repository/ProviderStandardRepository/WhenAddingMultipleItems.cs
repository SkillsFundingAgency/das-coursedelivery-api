using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardRepository
{
    public class WhenAddingMultipleItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderStandardRepository _providerStandardRepository;
        private List<ProviderStandard> _providerStandards;

        [SetUp]
        public void Arrange()
        {
            _providerStandards = new List<ProviderStandard>
            {
                new ProviderStandard
                {
                    StandardId = 1,
                    Ukprn = 123
                },
                new ProviderStandard
                {
                    StandardId = 2,
                    Ukprn = 123
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandards).ReturnsDbSet(new List<ProviderStandard>());
            _providerStandardRepository = new Data.Repository.ProviderStandardRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_ProviderStandard_Items_Are_Added_From_The_Import_Table()
        {
            //Act
            await _providerStandardRepository.InsertFromImportTable();
            
            //Assert
            _courseDeliveryDataContext.Verify(x=>
                x.ExecuteRawSql(@"INSERT INTO dbo.ProviderStandard SELECT * FROM dbo.ProviderStandard_Import"), Times.Once);
        }
    }
}