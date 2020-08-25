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
    }
}