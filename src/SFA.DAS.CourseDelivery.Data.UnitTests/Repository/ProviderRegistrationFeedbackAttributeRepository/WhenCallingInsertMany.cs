using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationFeedbackAttributeRepository
{
    public class WhenCallingInsertMany
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Inserts_Records_Into_DbContext(
            List<ProviderRegistrationFeedbackAttribute> items,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ProviderRegistrationFeedbackAttributeRepository repository)
        {
            await repository.InsertMany(items);

            mockContext.Verify(context => context.ProviderRegistrationFeedbackAttributes.AddRangeAsync(
                items, 
                It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}