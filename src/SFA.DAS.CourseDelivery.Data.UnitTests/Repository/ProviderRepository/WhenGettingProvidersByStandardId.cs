using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using Provider = SFA.DAS.CourseDelivery.Domain.Entities.Provider;
using ProviderRegistration = SFA.DAS.CourseDelivery.Domain.Entities.ProviderRegistration;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRepository
{
    public class WhenGettingProvidersByStandardId
    {
        private Mock<ICourseDeliveryReadonlyDataContext> _courseDeliveryDataContext;
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
                        Ukprn = 123,
                        ProviderRegistration = new ProviderRegistration
                        {
                            StatusId = RoatpTypeConstants.StatusOfActive, 
                            ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider
                        }
                    } 
                        
                },
                new ProviderStandard
                {
                    Ukprn = 12335,
                    StandardId = ExpectedStandardId,
                    Provider = new Provider
                    {
                        Ukprn = 12335,
                        Name="Second",
                        ProviderRegistration = new ProviderRegistration
                        {
                            StatusId = RoatpTypeConstants.StatusOfActive, 
                            ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider
                        }
                    } 
                }  ,
                new ProviderStandard
                {
                    Ukprn = 1233,
                    StandardId = ExpectedStandardId,
                    Provider = new Provider
                    {
                        Ukprn = 1233,
                        Name="First",
                        ProviderRegistration = new ProviderRegistration
                        {
                            StatusId = RoatpTypeConstants.StatusOfActive, 
                            ProviderTypeId = RoatpTypeConstants.ProviderTypeOfMainProvider
                        }
                    } 
                }  
            };
            
            
            _courseDeliveryDataContext = new Mock<ICourseDeliveryReadonlyDataContext>();
            _courseDeliveryDataContext.Setup(x => x.ProviderStandards).ReturnsDbSet(_providerStandards);
            
            _providerStandardImportRepository = new Data.Repository.ProviderRepository(Mock.Of<ICourseDeliveryDataContext>(), _courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_Providers_Items_Are_Returned_By_StandardId_Ordered_By_Name()
        {
            //Act
            var actual = await _providerStandardImportRepository.GetByStandardId(ExpectedStandardId);
            
            //Assert
            Assert.IsNotNull(actual);
            actual.Count().Should().Be(2);
            actual.Should().BeInAscendingOrder(c=>c.Name);
        }
    }
}