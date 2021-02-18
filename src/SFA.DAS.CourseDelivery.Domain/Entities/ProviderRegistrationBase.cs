using System;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderRegistrationBase
    {
        public int Ukprn { get; set; }
        public DateTime StatusDate { get; set; }
        public int StatusId { get; set; }
        public int OrganisationTypeId { get; set; }
        public int ProviderTypeId { get; set; }
        public int FeedbackTotal { get ; set ; }
        public string Address1 { get ; set ; }
        public string Address2 { get ; set ; }
        public string Address3 { get ; set ; }
        public string Address4 { get ; set ; }
        public string Town { get ; set ; }
        public string Postcode { get ; set ; }
        public double Lat { get ; set ; }
        public double Long { get ; set ; }
        public string LegalName { get ; set ; }
    }
}