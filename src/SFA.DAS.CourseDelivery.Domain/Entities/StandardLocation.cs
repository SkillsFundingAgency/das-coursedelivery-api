namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class StandardLocation : StandardLocationBase
    {
        public virtual ProviderStandardLocation ProviderStandardLocation { get ; set ; }
    }
}