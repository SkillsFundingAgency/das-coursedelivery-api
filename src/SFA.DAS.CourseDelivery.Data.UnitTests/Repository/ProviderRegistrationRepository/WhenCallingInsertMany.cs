using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationRepository
{
    public class WhenCallingInsertMany
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Inserts_Many_To_DbContext(
            List<ProviderRegistration> recordsToInsert,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ProviderRegistrationRepository repository)
        {
            await repository.InsertMany(recordsToInsert);

            mockContext.Verify(context => context.ProviderRegistrations.AddRangeAsync(
                recordsToInsert, 
                It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}