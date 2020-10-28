using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class ProviderRegistrationLookup
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("results")]
        public List<ProviderResult> Results { get; set; }
    }

    public class ProviderResult
    {
        [JsonProperty("ukprn")]
        public int Ukprn { get; set; }

        [JsonProperty("providerName")]
        public string ProviderName { get; set; }

        [JsonProperty("contactDetails")]
        public List<ContactDetail> ContactDetails { get; set; }

    }

    public class ContactDetail
    {
        [JsonProperty("contactType")]
        public string ContactType { get; set; }

        [JsonProperty("contactAddress")]
        public ContactAddress ContactAddress { get; set; }


        [JsonProperty("contactRole")]
        public string ContactRole { get; set; }

        [JsonProperty("contactTelephone1")]
        public string ContactTelephone1 { get; set; }

        [JsonProperty("contactTelephone2")]
        public string ContactTelephone2 { get; set; }

        [JsonProperty("contactWebsiteAddress")]
        public string ContactWebsiteAddress { get; set; }

        [JsonProperty("contactEmail")]
        public string ContactEmail { get; set; }

        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }
    }

    public class ContactAddress
    {
        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("address3")]
        public string Address3 { get; set; }

        [JsonProperty("address4")]
        public string Address4 { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("postCode")]
        public string PostCode { get; set; }
    }


}