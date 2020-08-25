using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Data.UnitTests.Repository.ProviderRegistrationRepository
{
    public class WhenCallingInsertFromImportTable
    {
        [Test, MoqAutoData]
        public async Task Then_Executes_Raw_Sql_To_Import(
            [Frozen] Mock<ICourseDeliveryDataContext> mockContext,
            Data.Repository.ProviderRegistrationRepository repository)
        {
            await repository.InsertFromImportTable();

            mockContext.Verify(context => context.ExecuteRawSql(SqlQueries.InsertProviderRegistrationsFromImport), Times.Once);
        }
    }
}