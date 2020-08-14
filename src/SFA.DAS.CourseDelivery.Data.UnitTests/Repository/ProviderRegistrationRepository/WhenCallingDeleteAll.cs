using System.Collections.Generic;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Courses.Data.UnitTests.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationRepository
{
    public class WhenCallingDeleteAll
    {
        [Test, MoqAutoData]
        public void Then_Deletes_All_Records_In_Db(
            List<ProviderRegistration> providerRegistrationsInDb,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ProviderRegistrationRepository repository)
        {
            mockContext
                .Setup(context => context.ProviderRegistrations)
                .ReturnsDbSet(providerRegistrationsInDb);

            repository.DeleteAll();

            mockContext.Verify(context => context.ProviderRegistrations.RemoveRange(providerRegistrationsInDb), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}