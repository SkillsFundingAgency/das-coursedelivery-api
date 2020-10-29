namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class ProviderHeadOfficeAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public double DistanceInMiles { get; set; }
    }
}