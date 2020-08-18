using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Infrastructure.HealthCheck;
using SFA.DAS.Testing.AutoFixture;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.CourseDelivery.Infrastructure.UnitTests.HealthChecks
{
    public class WhenGettingProviderRegistrationsHealthCheck
    {
        [Test, MoqAutoData]
        public async Task Then_The_ImportAudit_Provider_Registrations_Record_Is_Read_From_The_Repository(
            [Frozen] Mock<IImportAuditRepository> repository,
            HealthCheckContext healthCheckContext,
            ProviderRegistrationsHealthCheck healthCheck)
        {
            //Act
            await healthCheck.CheckHealthAsync(healthCheckContext);

            //Assert
            repository.Verify(x => x.GetLastImportByType(ImportType.ProviderRegistrations), Times.Once);
        }

        [Test, MoqAutoData]
        public async Task Then_If_No_Provider_Registrations_Rows_Are_Loaded_Then_Shows_As_Degraded(
            [Frozen] Mock<IImportAuditRepository> repository,
            HealthCheckContext healthCheckContext,
            ProviderRegistrationsHealthCheck healthCheck)
        {
            //Arrange
            repository.Setup(x => x.GetLastImportByType(ImportType.ProviderRegistrations))
                .ReturnsAsync(new ImportAudit(DateTime.Now, 0));

            //Act
            var actual = await healthCheck.CheckHealthAsync(healthCheckContext);

            //Assert
            actual.Status.Should().Be(HealthStatus.Degraded);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Provider_Registrations_Are_Loaded_And_More_Than_25_Hours_Old_Then_HealthCheck_Returns_Degraded(
            [Frozen] Mock<IImportAuditRepository> repository,
            HealthCheckContext healthCheckContext,
            ProviderRegistrationsHealthCheck healthCheck)
        {
            //Arrange
            repository.Setup(x => x.GetLastImportByType(ImportType.ProviderRegistrations))
                .ReturnsAsync(new ImportAudit(DateTime.UtcNow.AddHours(-25).AddMinutes(-1), 100));

            //Act
            var actual = await healthCheck.CheckHealthAsync(healthCheckContext);

            //Assert
            actual.Status.Should().Be(HealthStatus.Degraded);
        }

        [Test, MoqAutoData]
        public async Task Then_If_Provider_Registrations_Are_Loaded_And_Less_Than_25_Hours_Old_Then_HealthCheck_Returns_Healthy(
            [Frozen] Mock<IImportAuditRepository> repository,
            HealthCheckContext healthCheckContext,
            ProviderRegistrationsHealthCheck healthCheck)
        {
            //Arrange
            repository.Setup(x => x.GetLastImportByType(ImportType.ProviderRegistrations))
                .ReturnsAsync(new ImportAudit(DateTime.UtcNow.AddHours(-25).AddMinutes(1), 100));

            //Act
            var actual = await healthCheck.CheckHealthAsync(healthCheckContext);

            //Assert
            actual.Status.Should().Be(HealthStatus.Healthy);
        }
    }
}
