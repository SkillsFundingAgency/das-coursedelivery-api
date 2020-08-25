using SFA.DAS.CourseDelivery.Data.Configuration;

namespace SFA.DAS.CourseDelivery.Data
{
    public static class SqlQueries
    {
        public static string InsertProviderRegistrationsFromImport = $"INSERT INTO dbo.{ProviderRegistration.TableName} SELECT * FROM dbo.{ProviderRegistrationImport.TableName}";
    }
}