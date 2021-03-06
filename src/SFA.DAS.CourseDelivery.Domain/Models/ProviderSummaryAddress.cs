﻿using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Models
{
    public class ProviderSummaryAddress
    {
        public string Address1 { get ; set ; }
        public string Address2 { get ; set ; }
        public string Address3 { get ; set ; }
        public string Address4 { get ; set ; }
        public string Town { get ; set ; }
        public string Postcode { get ; set ; }

        public static implicit operator ProviderSummaryAddress(ProviderRegistration source)
        {
            if (source == null)
                return null;

            return new ProviderSummaryAddress
            {
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                Address4 = source.Address4,
                Town = source.Town,
                Postcode = source.Postcode
            };
        }
    }
}