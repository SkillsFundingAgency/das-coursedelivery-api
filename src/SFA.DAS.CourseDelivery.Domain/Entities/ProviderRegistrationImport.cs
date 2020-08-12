namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistrationImport : ProviderRegistrationBase
    {
        public static implicit operator ProviderRegistrationImport(ImportTypes.ProviderRegistration source)
        {
            return new ProviderRegistrationImport();
        }
    }
}
