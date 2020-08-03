namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderStandardLocation : ProviderStandardLocationBase
    {
        public virtual ProviderStandard ProviderStandard { get ; set ; }
        public virtual StandardLocation Location { get ; set ; }

        public static implicit operator ProviderStandardLocation(ProviderStandardLocationImport providerStandardLocationImport)
        {
            return new ProviderStandardLocation
            {
                Radius = providerStandardLocationImport.Radius,
                Ukprn = providerStandardLocationImport.Ukprn,
                DeliveryModes = providerStandardLocationImport.DeliveryModes,
                LocationId = providerStandardLocationImport.LocationId,
                StandardId = providerStandardLocationImport.StandardId
            };
        }
    }
}