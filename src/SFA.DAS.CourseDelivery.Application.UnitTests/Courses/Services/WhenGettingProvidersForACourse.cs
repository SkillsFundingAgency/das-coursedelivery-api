using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Provider.Services;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Courses.Services
{
    public class WhenGettingProvidersForACourse
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Gets_The_Providers_For_A_Course_From_The_Repository(
            int standardId,
            string sectorSubjectArea,
            short level,
            Guid shortlistUserId,
            [Frozen]Mock<IProviderRepository> repository,
            ProviderService service)
        {
            //Act
            await service.GetProvidersByStandardId(standardId, sectorSubjectArea, level, shortlistUserId);
            
            //Assert
            repository.Verify(x=>x.GetByStandardId(standardId, sectorSubjectArea, level, shortlistUserId), Times.Once);
        }
    }
}