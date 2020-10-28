using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderStandardRepository
{
    public class WhenGettingAProviderByStandardIdAndUkprn
    {
        private Mock<ICourseDeliveryReadonlyDataContext> _courseDeliveryDataContext;
        private Data.Repository.ProviderStandardRepository _providerStandardImportRepository;
        private List<ProviderStandard> _providerStandards;
        private ProviderStandard _expectedProviderStandard;
        private const int ExpectedStandardId = 2;
        private const int ExpectedUkprn = 12335;

        [SetUp]
        public void Arrange()
        {
            _expectedProviderStandard = new ProviderStandard
            {
                Ukprn = 12335,
                StandardId = ExpectedStandardId,
                Provider = new Provider
                {
                    Ukprn = ExpectedUkprn,
                    Name="Second"
                } 
            };
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
                _expectedProviderStandard  ,
                new ProviderStandard
                {
                    Ukprn = 1233,
                    StandardId = ExpectedStandardId,
                    Provider = new Provider
                    {
                        Ukprn = 1233,
                        Name="First"
                    } 
                }  
            };
            
            
            _courseDeliveryDataContext = new Mock<ICourseDeliveryReadonlyDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandards).ReturnsDbSet(_providerStandards);
            
            _providerStandardImportRepository = new Data.Repository.ProviderStandardRepository(Mock.Of<ICourseDeliveryDataContext>(), _courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_Providers_Items_Are_Returned_By_StandardId_Ordered_By_Name()
        {
            //Act
            var actual = await _providerStandardImportRepository.GetByUkprnAndStandard(ExpectedUkprn,ExpectedStandardId);
            //Assert
            Assert.IsNotNull(actual);
            
            actual.Provider.TradingName.Should().NotBeNullOrEmpty();
            actual.Should().BeEquivalentTo(_expectedProviderStandard);
        }
    }
}