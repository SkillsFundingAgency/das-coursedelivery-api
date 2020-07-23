using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.StandardLocationImportRepository
{
    public class WhenGettingAllItems
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.StandardLocationImportRepository _standardLocationImportRepository;
        private List<StandardLocationImport> _standardLocationImports;

        [SetUp]
        public void Arrange()
        {
            _standardLocationImports = new List<StandardLocationImport>
            {
                new StandardLocationImport
                {
                    LocationId = 1,
                    Address1 = "test"
                },
                new StandardLocationImport
                {
                    LocationId = 2,
                    Address1 = "test"
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.StandardLocationImports).ReturnsDbSet(_standardLocationImports);
            _standardLocationImportRepository = new Data.Repository.StandardLocationImportRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_StandardLocationImport_Items_Are_Returned()
        {
            //Act
            var actual = await _standardLocationImportRepository.GetAll();
            
            //Assert
            Assert.IsNotNull(actual);
            actual.Should().BeEquivalentTo(_standardLocationImports);
        }
    }
}