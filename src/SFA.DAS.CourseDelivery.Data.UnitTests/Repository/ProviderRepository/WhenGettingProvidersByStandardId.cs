using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRepository
{
    public class WhenGettingProvidersByStandardId
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderRepository _providerStandardImportRepository;
        private List<ProviderStandard> _providerStandards;
        private const int ExpectedStandardId = 2;

        [SetUp]
        public void Arrange()
        {
            _providerStandards = new List<ProviderStandard>
            {
                new ProviderStandard
                {
                    Ukprn = 123,
                    StandardId = 1,
                    Provider = new Provider
                    {
                        Ukprn = 123
                    } 
                        
                },
                new ProviderStandard
                {
                    Ukprn = 1233,
                    StandardId = ExpectedStandardId,
                    Provider = new Provider
                    {
                        Ukprn = 1233
                    } 
                }  
            };
            
            
            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandards).ReturnsDbSet(_providerStandards);
            
            _providerStandardImportRepository = new Data.Repository.ProviderRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_Providers_Items_Are_Returned_By_StandardId()
        {
            //Act
            var actual = await _providerStandardImportRepository.GetByStandardId(ExpectedStandardId);
            
            //Assert
            Assert.IsNotNull(actual);
            actual.Count().Should().Be(1);
        }
    }
}