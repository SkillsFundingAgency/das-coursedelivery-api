using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.NationalAchievementRateOverallRepository
{
    public class WhenGettingAllByDescription
    {
        private Mock<ICourseDeliveryDataContext> _courseDeliveryDataContext;
        private Data.Repository.NationalAchievementRateOverallRepository _repository;
        private List<NationalAchievementRateOverall> _items;
        private  const string ExpectedSectorSubjectArea = "TestSubjectArea";

        [SetUp]
        public void Arrange()
        {
            _items = new List<NationalAchievementRateOverall>
            {
                new NationalAchievementRateOverall
                {
                    Id = 1,
                    SectorSubjectArea = ExpectedSectorSubjectArea
                },
                new NationalAchievementRateOverall
                {
                    Id = 2,
                    SectorSubjectArea = ExpectedSectorSubjectArea
                },
                new NationalAchievementRateOverall
                {
                    Id = 3,
                    SectorSubjectArea = "Different"
                }
            };

            _courseDeliveryDataContext = new Mock<ICourseDeliveryDataContext>();
            _courseDeliveryDataContext.Setup(x => x.NationalAchievementRateOverall).ReturnsDbSet(_items);
            _repository = new Data.Repository.NationalAchievementRateOverallRepository(_courseDeliveryDataContext.Object);
        }

        [Test]
        public async Task Then_The_Items_Are_Returned_By_Description()
        {
            //Act
            var actual = await _repository.GetBySectorSubjectArea(ExpectedSectorSubjectArea);
            
            //Assert
            actual.ToList().TrueForAll(c => c.SectorSubjectArea.Equals(ExpectedSectorSubjectArea));
        }
    }
}