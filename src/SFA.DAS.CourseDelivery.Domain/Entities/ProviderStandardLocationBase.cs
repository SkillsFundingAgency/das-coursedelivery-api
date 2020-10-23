namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderStandardLocationBase
    {
        public int Ukprn { get; set; }
        public int StandardId { get; set; }
        public long LocationId { get; set; }
        public string DeliveryModes { get; set; }
        public decimal Radius { get; set; }
        public bool National { get; set; }
    }
}