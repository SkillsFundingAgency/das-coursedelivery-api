using System.Collections.Generic;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationFeedbackAttributeRepository
{
    public class WhenCallingDeleteAll
    {
        [Test, MoqAutoData]
        public void Then_Deletes_All_Records_In_Db(
            List<ProviderRegistrationFeedbackAttribute> itemsInDb,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ProviderRegistrationFeedbackAttributeRepository repository)
        {
            mockContext
                .Setup(context => context.ProviderRegistrationFeedbackAttributes)
                .ReturnsDbSet(itemsInDb);

            repository.DeleteAll();

            mockContext.Verify(context => context.ProviderRegistrationFeedbackAttributes.RemoveRange(itemsInDb), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}