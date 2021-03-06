﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationImportRepository
{
    public class WhenCallingInsertMany
    {
        [Test, MoqAutoData]
        public async Task Then_Inserts_Many_To_DbContext(
            List<ProviderRegistrationImport> recordsToInsert,
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.Import.ProviderRegistrationImportRepository repository)
        {
            await repository.InsertMany(recordsToInsert);

            mockContext.Verify(context => context.ProviderRegistrationImports.AddRangeAsync(
                recordsToInsert, 
                It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(context => context.SaveChanges(), Times.Once);
        }
    }
}