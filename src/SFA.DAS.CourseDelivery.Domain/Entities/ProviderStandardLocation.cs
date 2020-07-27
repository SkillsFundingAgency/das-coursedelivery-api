namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderStandardLocation : ProviderStandardLocationBase
    {
        public virtual ProviderStandard ProviderStandard { get ; set ; }
        public virtual StandardLocation Location { get ; set ; }
    }
}